using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Concurrent;

using Sattelite.Utils;
using Sattelite.Entities;
using Sattelite.Entities.UserAgg;
using Sattelite.Framework.Extensions;
using Sattelite.EntityFramework;
using Sattelite.EntityFramework.ActionResults.Admin;
using Sattelite.EntityFramework.ViewModels.Admin.User;
using Sattelite.EntityFramework.ViewModels.Admin.Persistences;
using Sattelite.EntityFramework.Repository;

namespace Sattelite.Web.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ICategoryRepository _categoryRepository;

        //private readonly IUserCreatingPersistence _userCreatingPersistence;
        private readonly IUserEditingPersistence _userEditingPersistence;
        //private readonly IUserDeletingPersistence _userDeletingPersistence;

        public UserController(
            //IUserCreatingPersistence userCreatingPersistence,
            IUserEditingPersistence userEditingPersistence,
            IRoleRepository roleRepository,
            IUserRepository userRepository,
            ICategoryRepository categoryRepository)
        {
            //_userCreatingPersistence = userCreatingPersistence;
            _userEditingPersistence = userEditingPersistence;

            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _categoryRepository = categoryRepository;

            //Refresh cached collection from DB
            if (AppCach.AllUsers == null || !AppCach.AllUsers.Any())
                AppCach.AllUsers = new ConcurrentBag<User>(_userRepository.GetUsers());//ViewBag.AllNews = _newsRepository.GetNews().ToList();

            //Default lists can be saved to cach
            //HttpContext.Cache["AllRoles"] = _roleRepository.GetRoles();
        }

        public ActionResult Index(int page = 1)
        {
            var viewModel = new UserListViewModel(AppCach.AllUsers);
            return View(viewModel);
        }

        #region public methods

        public ActionResult Create()
        {
            return new UserCreatingViewModelActionResult<UserController>(x => x.Create());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(UserCreatingViewModel viewModel)
        {
            var user = PrepareUser(viewModel, true);

            if (_userRepository.SaveUser(user))
            //if (_userCreatingPersistence.CreateUser(user))
            {
                SetSucceedMessage("User created successfully");
                AppCach.AllUsers.Add(user); //save to global cach
            }
            else
                SetErrorMessage("Cannot create user");

            return RedirectToAction("Index", "User");
        }

        public ActionResult Edit(int id)
        {
            var viewModel = new UserEditingViewModelActionResult<UserController>(x => x.Edit(id), id);
            return viewModel;
        }

        [HttpPost]
        public ActionResult Edit(UserEditingViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                SetErrorMessage("Please, validate all fields");
                return View(userViewModel);
            }

            var user = PrepareUser(userViewModel, false); //userViewModel.MapTo<User>(); //

            //then update user subscriptions: add new, remove old
            var newUserSubscriptions = !string.IsNullOrWhiteSpace(userViewModel.SubscriptionsIds)
                //JsonConvert.DeserializeObject<List<int>>(userViewModel.SubscriptionsIds);
                ? userViewModel.SubscriptionsIds.Split(',').Select(e => Convert.ToInt32(e)).ToList()
                : null;

            //Update all user details
            if (_userEditingPersistence.SaveUser(user, newUserSubscriptions))
            {
                SetSucceedMessage("User saved successfully");
            }
            else
                SetErrorMessage("Cannot save user");

            return RedirectToAction("Index", "User");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var applicationUser = _userRepository.GetById(id);
            if (applicationUser == null)
            {
                SetErrorMessage("User has not been found by id = " + id); //ModelState.AddModelError()
                return View();
            }
            var model = applicationUser.MapTo<UserEditingViewModel>();
            return View(model);
        }

        [HttpPost]
        public ActionResult DeleteUserConfirm(int userId)
        {
            bool isSucceed = false;
            try
            {
                if (userId <= 0)
                    throw new ArgumentException("userId");

                isSucceed = _userRepository.DeleteUser(userId);
                //update cach : AppCach.AllUsers.Except(AppCach.AllUsers.Where(n => n.Id == userId));
                AppCach.AllUsers = new ConcurrentBag<User>(_userRepository.GetUsers());//ViewBag.AllNews = _newsRepository.GetNews().ToList();
            }
            catch (Exception ex)
            {
                isSucceed = false; //SetErrorMessage(string.Format("Cannot remove user. {0} ", ex.Message));
                string errMsg = string.Format("Cannot remove user. See {0} ", ex.Message);
                //throw new Exception(errMsg);  //Display error in modal window
                return Json(new
                {
                    Error = errMsg,
                    Success = isSucceed
                });
            }
            SetSucceedMessage("User removed successfully !"); //TODO: alert

            return RedirectToAction("Index");
        }

        public ActionResult AssignUserToRole(string userName, string roleName)
        {
            var user = _userRepository.GetUserByUserName(userName);
            if (user == null)
            {
                SetErrorMessage("User doesn't exist!");
            }

            var role = _roleRepository.GetRoleByName(roleName);
            if (role == null)
            {
                SetErrorMessage("Role doesn't exist!");
            }

            bool isRoleAssigned = RoleHelper.AddUserToRole(userName, roleName);

            if (isRoleAssigned)
            {
                SetSucceedMessage("User added to role successfully");
            }
            //var viewModel = user.MapTo<UserEditingViewModel>();
            //viewModel.Roles.Add(roleId);

            return View();
        }

        public ActionResult RemoveUserFromRole(string userName, string roleName)
        {
            var user = _userRepository.GetUserByUserName(userName);
            if (user == null)
            {
                SetErrorMessage("User doesn't exist!");
            }

            var role = _roleRepository.GetRoleByName(roleName);
            if (role == null)
            {
                SetErrorMessage("Role doesn't exist!");
            }

            bool isRoleAssigned = RoleHelper.AddUserToRole(userName, roleName);

            return View();
        }


        [HttpGet]
        public ActionResult CreateCategorySubscriptions(string userName)
        {
            var user = _userRepository.GetUserByUserName(userName);
            if (user == null)
            {
                SetErrorMessage("User doesn't exist!");
            }

            //Display these categories as list
            return View("SelectCategoriesForSubscription", "Category" /*, new {categoriesViewModel = categories }*/);
        }

        [HttpPost]
        public ActionResult CreateCategorySubscription(string userName, int categoryId)
        {
            return RedirectToAction("CreateSubscription", "Category", new { @categoryId = categoryId, @userName = userName });
        }

        #endregion

        #region private methods

        private User PrepareUser(UserViewModel user, bool isNew)
        {
            var newUser = UserFactory.Update(
                                        isNew ? 0 : user.UserId.Value,
                                        user.UserName,
                                        user.DisplayName,
                                        user.Password, // TODO: encrypted
                                        user.Email,
                                        user.RoleId,
                                        GetUserName());
            newUser.ModifiedDate = DateTime.Now;
            //newUser.Subsriptions = user.cat
            return newUser;
        }

        #endregion
    }
}