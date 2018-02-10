namespace Sattelite.Web.Areas.Admin.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using Sattelite.Entities;
    using Sattelite.EntityFramework;
    using Sattelite.EntityFramework.Repository;
    using Sattelite.EntityFramework.ActionResults.Admin;
    using Sattelite.EntityFramework.ViewModels.Admin.Persistences;
    using Sattelite.EntityFramework.ViewModels.Admin.Category;
    using System.Collections.Concurrent;
    using Sattelite.Framework.Extensions;
    using System.Web.Security;

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

        [HttpGet]
        public ActionResult Create()
        {
            return new CategoryCreatingViewModelActionResult<CategoryController>(x => x.Create());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(CategoryCreatingViewModel viewModel)
        {
            var category = PrepareCategory(viewModel, true);

            if (!_categoryCreatingPersistence.Validate(category))//check name, etc/
                SetErrorMessage("Cannot create category with the same name");

            if (_categoryCreatingPersistence.CreateCategory(category))
            {
                SetSucceedMessage("Save category successfully");
                //Refresh cach AllCategories
                AppCach.AllCategories.Add(category);
            }
            else
                SetErrorMessage("Cannot create category");

            return RedirectToAction("Index", "Category");
        }

        public ActionResult Edit(int id)
        {
            return new CategoryEditingViewModelActionResult<CategoryController>(x => x.Edit(id), id);
        }

        [HttpPost]
        public ActionResult Edit(CategoryEditingViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                SetErrorMessage("Please, validate all fields");
                return View(viewModel);
            }
            var category = PrepareCategory(viewModel, false);

            if (_categoryEditingPersistence.SaveCategory(category))
                SetSucceedMessage("Category saved  successfully");
            else
                SetErrorMessage("Cannot update category. See validation errors.");

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
        [HandleError(View = "ErrorPage")]
        public ActionResult RSSCategoryFeed(int id)
        {
            if (id < 1)
            {
                ModelState.AddModelError("", "Error to subscribe to category with id < 1");
                throw new ArgumentNullException("CategoryId");
            }

            if (User == null || User.Identity == null)
            {
                ModelState.AddModelError("", "User is not authenticated (or no login)");
                throw new ArgumentNullException("User");
            }

            //Check if user subscribed to category already
            string currentUserName = User.Identity.Name;
            var userSubcriptions = _categoryRepository.GetUserSubscriptions(currentUserName);
            bool isUserSubscribedToCategory = userSubcriptions.Any(s => s.CategoryId == id);

            var subscription = new CategorySubscriptionViewModel
            {
                CategoryId = id,
                CategoryName = _categoryRepository.GetById(id)?.Name,
                UserId = (int)Membership.GetUser(User.Identity.Name)?.ProviderUserKey,
                UserName = currentUserName
            };

            if (!isUserSubscribedToCategory)
                return View("SubscribeToCategory", subscription);

            //else suggest to unsubscribe fom category
            return View("UnsubscribeFromCategory", subscription);
        }

        public JsonResult CreateSubscription(string userName, int categoryId)
        {
            bool isSubscribed = true;
            try
            {
                isSubscribed = _categoryRepository.AddUserSubscription(userName, categoryId);
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, responseText = ex.Message });
            }

            return Json(new { Success = isSubscribed }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Remove category Subscription for user
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public JsonResult RemoveCategorySubscriptionConfirm(string userName, int categoryId)
        {
            bool isSubscriptionRemoved = true;
            try
            {
                isSubscriptionRemoved = _categoryRepository.RemoveUserSubscription(userName, categoryId);
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, responseText = ex.Message });
            }

            return Json(new { Success = isSubscriptionRemoved }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion

        #region private methods
        private Category PrepareCategory(CategoryViewModel model, bool isNew)
        {
            return new Category
            {
                Id = isNew ? 0 : model.CategoryId,
                Name = model.Name,
                Description = model.ShortDescription,
                CreatedBy = isNew ? GetUserName() : model.CreatedBy,
                CreatedDate = isNew ? DateTime.Now : model.CreatedDate,
            };
        }

        #endregion
    }
}