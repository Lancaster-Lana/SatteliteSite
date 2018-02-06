namespace Sattelite.Web.Areas.Admin.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Sattelite.Entities;
    using Sattelite.EntityFramework;
    using Sattelite.EntityFramework.ActionResults.Admin;
    using Sattelite.EntityFramework.ActionResults.Client;
    using Sattelite.EntityFramework.ViewModels.Admin.Persistences;
    using Sattelite.EntityFramework.ViewModels.Admin.Role;
    using Sattelite.Framework.Extensions;
    using Sattelite.EntityFramework.Repository;
    using System.Collections.Concurrent;

    [Authorize]
    public class RoleController : BaseController
    {
        private readonly IRoleRepository _roleRepository;

        private readonly IRoleCreatingPersistence _roleCreatingPersistence;
        private readonly IRoleEditingPersistence _roleEditingPersistence;
        private readonly IRoleDeletingPersistence _roleDeletingPersistence;

        public RoleController(
            IRoleRepository roleRepository,
            IRoleCreatingPersistence roleCreatingPersistence,
            IRoleEditingPersistence roleEditingPersistence,
            IRoleDeletingPersistence roleDeletingPersistence)
        {
            _roleRepository = roleRepository;
            _roleCreatingPersistence = roleCreatingPersistence;
            _roleDeletingPersistence = roleDeletingPersistence;
            _roleEditingPersistence = roleEditingPersistence;

            //Refresh cached collection from DB
            if (AppCach.AllRoles == null || !AppCach.AllRoles.Any())
                AppCach.AllRoles = new ConcurrentBag<Role>(_roleRepository.GetRoles());
        }

        public ActionResult Index(int page = 1)
        {
            var viewModel = new RoleListViewModel(); //{ Roles = _roleRepository.GetRoles().ToList(); }
            TryUpdateModel(viewModel);
            return new RolesViewModelActionResult<RoleController>(x => x.Index(page),
                viewModel.TitleSearchText ?? string.Empty,
                page);
        }

        #region public methods

        public ActionResult Create()
        {
            return new RoleCreatingViewModelActionResult<RoleController>(x => x.Create());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(RoleCreatingViewModel viewModel)
        {
            var role = PrepareRole(viewModel, true);

            if (_roleCreatingPersistence.CreateRole(role))
            {
                SetSucceedMessage("Role created successfully");
                AppCach.AllRoles.Add(role); //save to global cach
            }
            else
                SetErrorMessage("Cannot create role");

            return RedirectToAction("Index", "Role");
        }

        public ActionResult Edit(int id)
        {
            return new RoleEditingViewModelActionResult<RoleController>(x => x.Edit(id), id);
        }

        [HttpPost]
        public ActionResult Edit(RoleEditingViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                SetErrorMessage("Please, validate all fields");
                return View(viewModel);
            }

            var role = PrepareRole(viewModel, false);

            if (_roleEditingPersistence.SaveRole(role))
                SetSucceedMessage("Save role successfully");
            else
                SetErrorMessage("Cannot update role");

            return RedirectToAction("Index", "Role");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var role = _roleRepository.GetById(id);
            if (role == null)
            {
                SetErrorMessage("Role has not been found by id = " + id); //ModelState.AddModelError()
                return View();
            }

            var model = role.MapTo<RoleEditingViewModel>();
            return View(model);
        }

        [HttpPost]
        public ActionResult DeleteRoleConfirm(int roleId)
        {
            var isSucceed = _roleDeletingPersistence.RemoveRole(roleId);

            if (isSucceed)
            {
                SetSucceedMessage("Role removed successfully !");
                //update cach : AppCach.AllRoles.Except(AppCach.AllRoles.Where(r => r.Id == roleId));
                AppCach.AllRoles = new ConcurrentBag<Role>(_roleRepository.GetRoles());
            }
            else
            {
                SetErrorMessage("Cannot delete role");
                return View();
            }
            return RedirectToAction("Index", "Role");
        }

        private Role PrepareRole(RoleViewModel role, bool isNew)
        {
            var newrole = new Role
            {
                Id = isNew ? 0 : role.RoleId.Value,
                Name = role.Name,
                Description = role.Description,
                CreatedDate = DateTime.Now,
                CreatedBy = GetUserName()
            };
            return newrole;
        }

        #endregion
    }
}
