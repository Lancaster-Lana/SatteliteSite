namespace Sattelite.Web.Areas.Admin.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Collections.Concurrent;

    using Sattelite.Entities;
    using Sattelite.EntityFramework;
    using Sattelite.EntityFramework.ActionResults.Admin;
    using Sattelite.EntityFramework.ViewModels.Admin.Role;
    using Sattelite.Framework.Extensions;
    using Sattelite.EntityFramework.Repository;

    [Authorize]
    public class ProjectRoleController : BaseController
    {
        private readonly IProjectRoleRepository _projectRoleRepository;

        //private readonly IProjectRoleCreatingPersistence _roleCreatingPersistence;
        //private readonly IProjectRoleEditingPersistence _roleEditingPersistence;
        //private readonly IProjectRoleDeletingPersistence _roleDeletingPersistence;

        public ProjectRoleController(
            IProjectRoleRepository projectRoleRepository
            //IProjectRoleCreatingPersistence roleCreatingPersistence,
            //IProjectRoleEditingPersistence roleEditingPersistence,
            //IProjectRoleDeletingPersistence roleDeletingPersistence
            )
        {
            _projectRoleRepository = projectRoleRepository;
            //_roleCreatingPersistence = roleCreatingPersistence;
            //_roleDeletingPersistence = roleDeletingPersistence;
            //_roleEditingPersistence = roleEditingPersistence;

            //Refresh cached collection from DB
            if (AppCach.AllProjectRoles == null || !AppCach.AllProjectRoles.Any())
                AppCach.AllProjectRoles = new ConcurrentBag<ProjectRole>(_projectRoleRepository.GetProjectRoles());
        }

        public ActionResult Index(int page = 1)
        {
            return View();
        }

        public ActionResult List(int page = 1)
        {
            var viewModel = new ProjectRoleListViewModel
            {
                ProjectRoles = AppCach.AllProjectRoles?.ToList()
            };
            return PartialView(viewModel);
        }

        #region public methods

        public ActionResult Create()
        {
            return new ProjectRoleCreatingViewModelActionResult<ProjectRoleController>(x => x.Create());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ProjectRoleViewModel viewModel)
        {
            var role = PrepareRole(viewModel, true);

            if (_projectRoleRepository.SaveProjectRole(role))
            {
                SetSucceedMessage("Project role created successfully !");
                AppCach.AllProjectRoles.Add(role); //save to global cach
            }
            else
                SetErrorMessage("Cannot create role");

            return RedirectToAction("Index", "ProjectRole");
        }

        /*
                public ActionResult Edit(int id)
                {
                    return new ProjectRoleEditingViewModelActionResult<RoleController>(x => x.Edit(id), id);
                }

                [HttpPost]
                public ActionResult Edit(ProjectRoleEditingViewModel viewModel)
                {
                    if (!ModelState.IsValid)
                    {
                        SetErrorMessage("Please, validate all fields");
                        return View(viewModel);
                    }

                    var role = PrepareRole(viewModel, false);

                    if (_projectRoleEditingPersistence.SaveRole(role))
                        SetSucceedMessage("Project role updated successfully !");
                    else
                        SetErrorMessage("Cannot update role");

                    return RedirectToAction("Index", "ProjectRole");
                }
                */

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var role = _projectRoleRepository.GetById(id);
            if (role == null)
            {
                SetErrorMessage("Role has not been found by id = " + id); //ModelState.AddModelError()
                return View();
            }

            var model = role.MapTo<ProjectRoleViewModel>();
            return View(model);
        }

        [HttpPost]
        public ActionResult DeleteProjectRoleConfirm(int roleId)
        {
            var isSucceed = _projectRoleRepository.DeleteProjectRole(roleId);

            if (isSucceed)
            {
                SetSucceedMessage("Role removed successfully !");
                //update cach : AppCach.AllRoles.Except(AppCach.AllRoles.Where(r => r.Id == roleId));
                AppCach.AllProjectRoles = new ConcurrentBag<ProjectRole>(_projectRoleRepository.GetProjectRoles());
            }
            else
            {
                SetErrorMessage("Cannot delete role");
                return View();
            }
            return RedirectToAction("Index", "ProjectRole");
        }

        private ProjectRole PrepareRole(ProjectRoleViewModel role, bool isNew)
        {
            var newrole = new ProjectRole
            {
                Id = isNew ? 0 : role.RoleId.Value,
                Name = role.Name,
                Description = role.ShortDescription,
                CreatedDate = DateTime.Now,
                CreatedBy = isNew ? GetUserName() : role.CreatedBy
            };
            return newrole;
        }

        #endregion
    }
}
