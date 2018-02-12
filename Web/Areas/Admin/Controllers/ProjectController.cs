namespace Sattelite.Web.Areas.Admin.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Collections.Concurrent;

    using Sattelite.Framework.Extensions;
    using Sattelite.Entities;
    using Sattelite.EntityFramework;
    using Sattelite.EntityFramework.ActionResults.Admin;
    using Sattelite.EntityFramework.ViewModels.Admin.Project;
    using Sattelite.EntityFramework.ViewModels.Admin.Persistences;
    using Sattelite.EntityFramework.Repository;

    [Authorize]
    public class ProjectController : BaseController
    {
        private readonly IProjectRepository _projectRepository;

        private readonly IProjectCreatingPersistence _projectCreatingPersistence;
        private readonly IProjectEditingPersistence _projectEditingPersistence;
        private readonly IProjectDeletingPersistence _projectDeletingPersistence;

        public ProjectController(
              IProjectRepository projectRepository,
              IProjectCreatingPersistence projectCreatingPersistence,
              IProjectEditingPersistence projectEditingPersistence,
              IProjectDeletingPersistence projectDeletingPersistence
            )
        {
            _projectRepository = projectRepository;

            _projectCreatingPersistence = projectCreatingPersistence;
            _projectEditingPersistence = projectEditingPersistence;
            _projectDeletingPersistence = projectDeletingPersistence;

            //Refresh AllNews collection from DB
            if (AppCach.AllProjects == null || !AppCach.AllProjects.Any())
                AppCach.AllProjects = new ConcurrentBag<Project>(_projectRepository.GetProjects());
        }

        public ActionResult Index(int page = 1)
        {
            var viewModel = new ProjectViewModel();
            TryUpdateModel(viewModel);

            return new ProjectsViewModelActionResult<ProjectController>(
                x => x.Index(page),
                viewModel.TitleSearchText ?? string.Empty, viewModel.CategoryId,
                page);
        }

        public ActionResult Create()
        {
            return new ProjectCreatingViewModelActionResult<ProjectController>(x => x.Create());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ProjectCreatingViewModel viewModel)
        {
            var project = PrepareProject(viewModel, true);

            if (_projectCreatingPersistence.CreateProject(project))
            {
                SetSucceedMessage("The project is saved  successfully");
                AppCach.AllProjects.Add(project); //save global cach
                return RedirectToAction("Index", "Project");
            }
            else
            {
                SetErrorMessage("Cannot create project");
                return View(viewModel);
            }
        }

        public ActionResult Edit(int id)
        {
            return new ProjectEditingViewModelActionResult<ProjectController>(x => x.Edit(id), id);
        }

        [HttpPost]
        //[ValidateInput(false)]
        public ActionResult Edit(ProjectEditingViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                SetErrorMessage("Please, validate all fields");
                return View(viewModel);
            }

            var project = PrepareProject(viewModel, false);

            if (_projectEditingPersistence.SaveProject(project))
            {
                SetSucceedMessage("Project saved successfully");
                return RedirectToAction("Index", "Project");
            }
            else
            {
                SetErrorMessage("Cannot save project data");
                return View(viewModel);
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var project = _projectRepository.GetById(id);
            if (project == null)
            {
                SetErrorMessage("Project hasn't been found by id = " + id);
                return View();
            }
            var model = project.MapTo<ProjectViewModel>();
            return View(model);

            //return new ProjectDeletingViewModelActionResult<ProjectController>(
            //    x => x.Delete(id), id);
        }

        [HttpPost]
        public ActionResult Delete(ProjectViewModel model)//int projectId)
        {
            bool isSucceed = false;

            try
            {
                isSucceed = _projectDeletingPersistence.DeleteProject(model.ProjectId.Value);

                if (isSucceed)
                {
                    SetSucceedMessage("Project removed successfully !");
                    //update cach : AppCach.AllRoles.Except(AppCach.AllRoles.Where(r => r.Id == roleId));
                    AppCach.AllProjects = new ConcurrentBag<Project>(_projectRepository.GetProjects());
                }
                else
                {
                    SetErrorMessage("Cannot delete project");
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                isSucceed = false;
                string errMsg = string.Format("Cannot remove project. See {0} ", ex.InnerException.Message);

                SetErrorMessage(errMsg); //TODO; log error
                return View("Delete", model);

                //return Json(new { Error = errMsg, Success = isSucceed});
            }

            return RedirectToAction("Index");
        }

        #region Project Membership

        /// <summary>
        /// Assign a new participant to the project role
        /// </summary>
        /// <param name="id">projectId</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddProjectMember(int id)
        {
            var model = new ProjectMemberViewModel
            {
                ProjectId = id,
                //ProjectRoleId = -1, UserId = -1
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult AddProjectMember(ProjectMemberViewModel model)
        {
            bool isMemberAdded = false;

            try
            {
                var newProjectMember = PrepareProjectMember(model, true);
                isMemberAdded = _projectEditingPersistence.AddProjectMember(newProjectMember);
                if (isMemberAdded)
                {
                    SetSucceedMessage(string.Format( "User '{0}' has been added to project role '{1}' !", newProjectMember.User.UserName, newProjectMember.ProjectRole.Name));
                }
            }
            catch (Exception ex)
            {
                SetErrorMessage("Project member hasn't been created !" + ex.Message);
                //return ShowAlertErrorMessage("Project member hasn't been created !" + ex.Message );
            }

            if (!isMemberAdded)
                return View(model);//AddProjectMember(model.ProjectId);

            //Stay on edit project if success
            return Edit(model.ProjectId);
        }

        /// <summary>
        /// Delete the user from some role in the project
        /// </summary>
        /// <param name="id">projectMemberId</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DeleteProjectMember(int id)
        {
            return new ProjectMemberDeletingViewModelActionResult<ProjectController>(
               x => x.DeleteProjectMember(id), id);

            //var viewModel = member?.MapTo<ProjectMemberViewModel>();
            //return View(viewModel);
        }

        [HttpPost]
        public ActionResult DeleteProjectMember(int projectId, int projectMemberId)
        {
            bool isSucceed = false;
            try
            {
                isSucceed = _projectRepository.DeleteProjectMember(projectMemberId);

                if (isSucceed)
                {
                    SetSucceedMessage("The project member removed successfully !"); //ShowAlertSuccessMessage("Project member removed successfully !");
                }
                else
                {
                    SetErrorMessage(string.Format("Cannot remove the project member with Id={0}", projectMemberId));
                }
            }
            catch (Exception ex)
            {
                SetErrorMessage(string.Format("Cannot remove the project member with Id={0}. See error : {1}", projectMemberId, ex.Message));
            }

            if(!isSucceed)
                return View(projectId); //Display the delete view with errors

            //return to the project edit page
            return Edit(projectId);
        }

        #endregion

        #region Private methods

        private Project PrepareProject(ProjectViewModel model, bool isNew)
        {
            //var project = model.MapTo<Project>(); //- not proper

            var project = new Project
            {
                Id = isNew ? 0 : (int)model.ProjectId,
                CategoryId = model.CategoryId,
                Category = AppCach.AllCategories.FirstOrDefault(c => c.Id == model.CategoryId),//model.AllCategories.FirstOrDefault(c => c.Id == model.CategoryId), //x
                CoordinatorId = model.CoordinatorId ?? model.CoordinatorId.Value,
                Coordinator = AppCach.AllUsers.FirstOrDefault(c => c.Id == model.CoordinatorId), //model.AllUsers.FirstOrDefault(c => c.Id == model.CoordinatorId), //x
                CreatedDate = isNew ? DateTime.Now : model.CreatedDate,
                CreatedBy = isNew ? GetUserName() : model.CreatedBy, //HttpContext.User?.Identity?.Name
                ModifiedDate = isNew ? null : model.ModifiedDate,

                //prepare content
                ProjectContent = new ProjectContent
                {
                    Name = model.Name,
                    Content = model.Content,
                    ShortDescription = model.ShortDescription,
                    CreatedDate = isNew ? DateTime.Now : model.CreatedDate,
                    CreatedBy = isNew ? GetUserName() : model.CreatedBy //HttpContext.User?.Identity?.Name
                }
                //ProjectMembers = isNew ? new List<ProjectMember>() : model.ProjectMembers
            };
            return project;
        }

        private ProjectMember PrepareProjectMember(ProjectMemberViewModel model, bool isNew)
        {
            var projectMember = new ProjectMember
            {
                Id = isNew ? 0 : model.Id.Value,
                UserId = model.UserId,
                ProjectId = model.ProjectId,
                ProjectRoleId = model.ProjectRoleId,
                CreatedDate = isNew ? DateTime.Now : model.CreatedDate,
                CreatedBy = isNew ? GetUserName() : model.CreatedBy,
                ModifiedDate = isNew ? null : model.ModifiedDate,
            };

            //var projectMember = model.MapTo<ProjectMember>()

            return projectMember;
        }

        #endregion
    }
}