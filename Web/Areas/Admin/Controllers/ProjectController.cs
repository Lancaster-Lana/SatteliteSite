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
    using Sattelite.EntityFramework.ViewModels.Admin.User;
    using Sattelite.EntityFramework.Repository;

    /// <summary>
    /// 
    /// </summary>
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

        /// <summary>
        /// Begin assigning a new particioant of the project 
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public ActionResult AddProjectMember(int projectId)
        {
            //return new ProjectMemberCreatingViewModelActionResult<ProjectController>(x => x.AddProjectMember());

            //var project = _projectRepository.GetById(projectId);
            //project.ProjectMembers

            var model = new ProjectMemberViewModel
            {
                ProjectId = projectId,
                ProjectUser = new UserViewModel { UserName = "Please, select a user" },
                ProjectRole = new ProjectMemberRoleViewModel() { Id = 0, Name = "Please, select a member role" }
            };

            return View(model);
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
            }
            else
                SetErrorMessage("Cannot create project");

            return RedirectToAction("Index", "Project");
        }

        public ActionResult Edit(int id)
        {
            return new ProjectEditingViewModelActionResult<ProjectController>(x => x.Edit(id), id);
        }

        [HttpPost]
        [ValidateInput(true)]
        public ActionResult Edit(ProjectEditingViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                SetErrorMessage("Please, validate all fields");
                return View(viewModel);
            }

            var project = PrepareProject(viewModel, false);

            if (_projectEditingPersistence.SaveProject(project))
                SetSucceedMessage("Project saved successfully");
            else
                SetErrorMessage("Cannot save project data");
            return RedirectToAction("Index", "Project");//return View();
        }

        //public ActionResult EditProjectMember(int projectId)
        //{
        //    return new ProjectMemberEditingViewModelActionResult<ProjectController>(x => x.Edit(id), id);
        //}

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
        public ActionResult DeleteProjectConfirm(ProjectViewModel model)//int projectId)
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

        [HttpGet]
        public ActionResult DeleteProjectMember(int projectId, int projectMemberId)
        {
            return new ProjectMemberDeletingViewModelActionResult<ProjectController>(
               x => x.DeleteProjectMember(projectId, projectMemberId), projectId, projectMemberId);

            //var project = _projectRepository.GetById(projectId);
            //if (project == null)
            //{
            //    SetErrorMessage("Project hasn't been found by id = " + projectId);
            //    return View();
            //}
            //var member = project.ProjectMembers.FirstOrDefault(m => m.Id == projectMemberId);
            //var viewModel = member?.MapTo<ProjectMemberViewModel>();
        }

        [HttpDelete]
        public ActionResult DeleteProjectMemberConfirm(int projectId, int projectMemberId)
        {
            //var isSucceed = _projectRepository.DeleteProjectMember(projectId, projectMemberId);

            if (_projectDeletingPersistence.DeleteProjectMember(projectId, projectMemberId))
                SetSucceedMessage("Project removed successfully !");
            else
                SetErrorMessage(string.Format("Cannot remove the project member with Id={0}", projectMemberId));

            return RedirectToAction("Index");
        }

        #region private methods

        private Project PrepareProject(ProjectViewModel model, bool isNew)
        {
            var project = model.MapTo<Project>(); //- not proper

            project = new Project
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

            //if (isNew)
            //{
            //    //if new project, add coordinator as the fisrt project member
            //    project.ProjectMembers.Add(new ProjectMember
            //    {
            //        UserId = project.Coordinator.Id,
            //        //User = project.Coordinator
            //        ProjectRoleId = (int)DefaultProjectMemberRoles.Coordinator,
            //        //ProjectRole = model.AllProjectMemberRoles.FirstOrDefault(r => r.Id == (int)DefaultProjectMemberRoles.Coordinator),
            //        CreatedDate = DateTime.Now,
            //        CreatedBy = GetUserName(),
            //    });
            //}
            return project;
        }

        #endregion
    }
}