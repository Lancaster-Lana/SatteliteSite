namespace Sattelite.Web.Areas.Admin.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using System.Collections.Concurrent;

    using Sattelite.Framework.Extensions;
    using Sattelite.Entities;
    using Sattelite.EntityFramework;
    using Sattelite.EntityFramework.Repository;
    using Sattelite.EntityFramework.Extensions;
    using Sattelite.EntityFramework.MediaItem;
    using Sattelite.EntityFramework.ActionResults.Admin;
    using Sattelite.EntityFramework.ViewModels.Admin.News;
    using Sattelite.EntityFramework.ViewModels.Admin.Persistences;

    [Authorize]
    public class NewsController : BaseController
    {
        #region ctors & variables

        private readonly ICategoryRepository _categoryRepository;
        private readonly INewsRepository _newsRepository;

        private readonly INewsCreatingPersistence _newsCreatingPersistence;
        private readonly INewsEditingPersistence _newsEditingPersistence;
        private readonly INewsDeletingPersistence _newsDeletingPersistence;
        private readonly IMediaNewsStorage _newsStorage;

        public NewsController(
            ICategoryRepository categoryRepository,
            INewsRepository newsRepository,
            INewsCreatingPersistence newsCreatingPersistence,
            INewsEditingPersistence newsEditingPersistence,
            INewsDeletingPersistence newsDeletingPersistence,
            IMediaNewsStorage newsStorage)
        {
            _categoryRepository = categoryRepository;
            _newsRepository = newsRepository;

            _newsCreatingPersistence = newsCreatingPersistence;
            _newsDeletingPersistence = newsDeletingPersistence;
            _newsEditingPersistence = newsEditingPersistence;
            _newsStorage = newsStorage;

            //Refresh AllNews collection from DB
            if (AppCach.AllNews == null || !AppCach.AllNews.Any())
               AppCach.AllNews = new ConcurrentBag<News>(_newsRepository.GetNews());//ViewBag.AllNews = _newsRepository.GetNews().ToList();
        }

        #endregion

        #region Public methods

        public ActionResult Index(int page = 1)
        {
            var viewModel = new NewsListViewModel(page);
            //TryUpdateModel(viewModel);
            //return new NewsViewModelActionResult<NewsController>(x => x.Index(page), 1);
            return View(viewModel);
        }

        public ActionResult Create()
        {
            return new NewsCreatingViewModelActionResult<NewsController>(x => x.Create());
        }

        [HttpPost]
        [ValidateInput(true)]
        public ActionResult Create(NewsCreatingViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                SetErrorMessage("Please, fill the fields signed with *");
                return View(viewModel);
            }

            var article = PrepareArticle(viewModel, true);

            if (_newsCreatingPersistence.CreateNews(article))
            {
                SetSucceedMessage("Save news successfully");
                //Refresh AppCach.AllNews
                AppCach.AllNews.Add(article);
            }
            else
                SetErrorMessage("Cannot create article\news");

            return RedirectToAction("Index", "News");
        }

        public ActionResult Edit(int id)
        {
            return new NewsEditingViewModelActionResult<NewsController>(x => x.Edit(id), id);
        }

        [HttpPost]
        public ActionResult Edit(NewsEditingViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                SetErrorMessage("Please, validate all fields");
                return View(viewModel);
            }

            var item = PrepareArticle(viewModel, false);

            if (_newsEditingPersistence.SaveNews(item))
                SetSucceedMessage("News article saved successfully");
            else
                SetErrorMessage("Cannot update article");

            return RedirectToAction("Index", "News");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var article = _newsRepository.GetById(id);
            if (article == null)
            {
                SetErrorMessage("Article hasn't been found by id = " + id);
                return View();
            }
            var model = article.MapTo<NewsEditingViewModel>();
            return View(model);
        }

        [HttpPost]
        public ActionResult DeleteNewsConfirm(int articleId)
        {
            var isSucceed = _newsDeletingPersistence.DeleteNews(articleId);

            if (isSucceed)
            {
                SetSucceedMessage("Article removed successfully !");
                //update cach: AppCach.AllNews.Except(AppCach.AllNews.Where(n => n.Id == articleId));
                AppCach.AllNews = new ConcurrentBag<News>(_newsRepository.GetNews());//ViewBag.AllNews = _newsRepository.GetNews().ToList();
            }
            else
            {
                SetErrorMessage("Cannot delete this article !");
                return View();
            }
            return RedirectToAction("Index", "News");
        }

        #endregion

        #region Private methods

        private News PrepareArticle(NewsViewModel model, bool isNew)
        {
            var smallImagePath = model.SmallImage == null ? null
                : HttpPostedFileExtension.CreateImagePathFromStream(model.SmallImage, _newsStorage);//vm.SmallImage.CreateImagePathFromStream(_itemStorage);
            var mediumImagePath = model.MediumImage == null ? null
                : HttpPostedFileExtension.CreateImagePathFromStream(model.MediumImage, _newsStorage);
            var largeImagePath = model.BigImage == null ? null
                : HttpPostedFileExtension.CreateImagePathFromStream(model.BigImage, _newsStorage);

            var newsContent = new NewsContent
            {
                Title = model.Title,
                ShortDescription = model.ShortDescription,
                Content = model.Content,
                SmallImage = smallImagePath,
                MediumImage = mediumImagePath,
                BigImage = largeImagePath
            };

            News article = new News
            {
                Id = isNew ? -1 : model.NewsId.Value,
                NewsContent = newsContent,
                CategoryId = model.CategoryId,
                //category = model.AllCategories.FirstOrDefault(c => c.Id == model.CategoryId);
                CreatedBy = GetUserName()
            };

            return article;
        }

        #endregion
    }
}
