namespace Sattelite.Web.Areas.Admin.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Security;

    using Sattelite.Entities;
    using Sattelite.EntityFramework;
    using Sattelite.EntityFramework.Repository;
    using Sattelite.EntityFramework.ActionResults.Admin;
    using Sattelite.EntityFramework.ViewModels.Admin.Persistences;
    using Sattelite.EntityFramework.ViewModels.Admin.Category;
    using System.Collections.Concurrent;
    using Sattelite.Framework.Extensions;

    [Authorize]
    public class CategoryController : BaseController
    {
        private readonly ICategoryRepository _categoryRepository;

        private readonly ICategoryCreatingPersistence _categoryCreatingPersistence;
        private readonly ICategoryEditingPersistence _categoryEditingPersistence;
        private readonly ICategoryDeletingPersistence _categoryDeletingPersistence;

        public CategoryController(
            ICategoryRepository categoryRepository,
            ICategoryCreatingPersistence categoryCreatingPersistence,
            ICategoryEditingPersistence categoryEditingPersistence,
            ICategoryDeletingPersistence categoryDeletingPersistence
            )
        {
            _categoryRepository = categoryRepository;
            _categoryCreatingPersistence = categoryCreatingPersistence;
            _categoryDeletingPersistence = categoryDeletingPersistence;
            _categoryEditingPersistence = categoryEditingPersistence;

            if (AppCach.AllCategories == null || !AppCach.AllCategories.Any())
                AppCach.AllCategories = new ConcurrentBag<Category>(_categoryRepository.GetCategories().ToList());
        }

        public ActionResult Index(int page = 1)
        {
            var viewModel = new CategoryListViewModel();

            /*TryUpdateModel<CategoryListViewModel>(viewModel);
            return new CategoryViewModelActionResult<CategoryController>(x => x.Index(page), 1);
            */
            return View(viewModel);
        }

        #region public methods

        public ActionResult Create()
        {
            return new CategoryCreatingViewModelActionResult<CategoryController>(x => x.Create());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(CategoryCreatingViewModel viewModel)
        {
            var category = PrepareCategory(viewModel, true);

            if (_categoryCreatingPersistence.CreateCategory(category))
                SetSucceedMessage("Save category successfully");
            else
                SetErrorMessage("Cannot create category");

            return RedirectToAction("Index", "Category");
        }

        public ActionResult Edit(int id)
        {
            return new CategoryEditingViewModelActionResult<CategoryController>(x => x.Edit(id), id);
        }

        [HttpPost]
        //[ValidateInput(false)]
        public ActionResult Edit(CategoryEditingViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                SetErrorMessage("Please, validate all fields");
                return View(viewModel);
            }

            var category = PrepareCategory(viewModel, false);

            if (_categoryEditingPersistence.SaveCategory(category))
                SetSucceedMessage("Save category successfully");
            else
                SetErrorMessage("Cannot edit category");

            return RedirectToAction("Index", "Category");
        }

        //public ActionResult Delete_old(int id)
        //{
        //    var isSucceed = _categoryDeletingPersistence.DeleteCategory(id);

        //    if (isSucceed)
        //        SetSucceedMessage("Category deleted successfully !");
        //    else
        //        SetErrorMessage("Cannot delete category");

        //    return RedirectToAction("Index", "Category");
        //}

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var category = _categoryRepository.GetById(id);
            if (category == null)
            {
                SetErrorMessage("Category hasn't been found by id = " + id);
                return View();
            }
            var model = category.MapTo<CategoryEditingViewModel>();
            return View(model);
        }

        [HttpPost]
        public ActionResult DeleteCategoryConfirm(int categoryId)
        {
            var isSucceed = _categoryDeletingPersistence.DeleteCategory(categoryId);

            if (isSucceed)
            {
                SetSucceedMessage("Category removed successfully !");
                //update cach : AppCach.AllCategories.Except(AppCach.AllCategories.Where(n => n.Id == categoryId));
                AppCach.AllCategories = new ConcurrentBag<Category>(_categoryRepository.GetCategories().ToList());
            }
            else
            {
                SetErrorMessage("Cannot delete category");
                return View();
            }
            return RedirectToAction("Index", "Category");
        }

        #region Subscriptions

        /// <summary>
        /// Subscribe to category posts, projects
        /// NOTE: user should be logged-in before getting possibility to subscribe category
        /// </summary>
        /// <param name="id">category id</param>
        /// <returns></returns>
        [HandleError(ExceptionType = typeof(ArgumentNullException), View = "ErrorPage")]
        public ActionResult RSSCategoryFeed(int id)
        {
            if (id < 1)
            {
                ModelState.AddModelError("Error", "Error to subscribe to category with id < 1");
                throw new ArgumentNullException("CategoryId");
            }

            if (User == null || User.Identity == null)
            {
                ModelState.AddModelError("Error", "User is not authenticated (or no login)");
                throw new ArgumentNullException("User");
            }

            var newSubscription = new SubscriptionViewModel
            {
                CategoryId = id,
                CategoryName = _categoryRepository.GetById(id)?.Name,
                UserId = (int)Membership.GetUser(User.Identity.Name)?.ProviderUserKey,
                UserName = User?.Identity?.Name
            };

            return View("RSSCategoryFeed", newSubscription);
        }

        public ActionResult RSSCategoryFeedSubsriptionSuccess()
        {
            return View();
        }

        public ActionResult CreateSubscription(int userId, int categoryId)
        {
            var isSubscribed = _categoryRepository.AddUserSubscription(userId, categoryId);

            if (isSubscribed)
                return View("RSSCategoryFeedSubsriptionSuccess");

            return View("RSSCategoryFeedSubsriptionError");
        }

        /// <summary>
        /// Remove category Subscription for user
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public ActionResult RemoveSubscription(string userName, int categoryId)
        {
            bool removed = _categoryRepository.RemoveUserSubscription(userName, categoryId);
            if (removed)
                return View("RSSCategoryFeedRemovedSuccess");

            return View("RSSCategoryFeedSubsriptionError");
        }

        #endregion

        #endregion

        #region private methods
        private Category PrepareCategory(CategoryViewModel model, bool isNew)
        {

            //return CategoryFactory.GetCategory(
            //                            isNew ? 0 : model.CategoryId,
            //                            model.Name,
            //                            model.SortDescription,
            //                            GetUserName());

            return new Category
            {
                Id = isNew ? 0 : model.CategoryId,
                Name = model.Name,
                Description = model.Description,
                CreatedBy = isNew ? GetUserName() : model.CreatedBy,
                CreatedDate = isNew ? DateTime.Now : model.CreatedDate,
            };
        }

        #endregion
    }
}
