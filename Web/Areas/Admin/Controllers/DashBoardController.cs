namespace Sattelite.Web.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Concurrent;
    using System.Configuration;
    using System.Linq;
    using System.Web.Mvc;
    using Sattelite.Entities;
    using Sattelite.EntityFramework;
    using Sattelite.EntityFramework.Repository;
    using Sattelite.EntityFramework.ViewModels;
    using Sattelite.EntityFramework.ViewModels.Admin.DashBoard;

    [Authorize]
    public class DashBoardController : BaseController
    {
        private readonly string _titleSearchText;
        private readonly int _currentPage;
        private readonly int _numOfPage;

        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProjectRoleRepository _projectRoleRepository;
        private readonly INewsRepository _newsRepository;

        public DashBoardController(string titleSearchText = "", int currentPage = 0) : this(
          titleSearchText, currentPage,
          DependencyResolver.Current.GetService<IUserRepository>(),
          DependencyResolver.Current.GetService<IRoleRepository>(),
          DependencyResolver.Current.GetService<ICategoryRepository>(),
          DependencyResolver.Current.GetService<IProjectRoleRepository>(),
          DependencyResolver.Current.GetService<INewsRepository>())
        {
        }

        public DashBoardController(
                    string titleSearchText, int currentPage,
                    IUserRepository userRepository,
                    IRoleRepository roleRepository,
                    ICategoryRepository categoryRepository,
                    IProjectRoleRepository projectRoleRepository,
                    INewsRepository newsRepository) : base()
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _categoryRepository = categoryRepository;
            _projectRoleRepository = projectRoleRepository;
            _newsRepository = newsRepository;

            _titleSearchText = titleSearchText;
            _currentPage = currentPage;
            
            _numOfPage = ConfigurationManager.AppSettings["NumOfPage"] != null
                ? Int32.Parse(ConfigurationManager.AppSettings["NumOfPage"])
                : 10;
        }

        public ActionResult Index(int page = 1)
        {
            int numOfRecords;
            var viewModel = new DashBoardViewModel
            {
                //Init all collections for admin board
                AllUsers = _userRepository.GetUsers().ToList(),
                AllRoles = _roleRepository.GetRoles().ToList(),
                AllCategories = _categoryRepository.GetCategories().ToList(),
                AllProjectRoles = _projectRoleRepository.GetProjectRoles().ToList(),
                AllNews = _newsRepository.SeachByTitle(_titleSearchText, _currentPage, _numOfPage, out numOfRecords)?.ToList(),

                PagingData = new PagingViewModel(
                                                    _currentPage,
                                                    _numOfPage,
                                                    numOfRecords,
                                                    string.Format("{0}",
                                                        "/Admin/DashBoard/Index/{page}"),
                                                     null)
            };

            //Init common reuable collections on admin dashboard (for all views)
            //AppCach.Current.Application.Lock();
            AppCach.AllUsers = new ConcurrentBag<User>(viewModel.AllUsers);
            AppCach.AllRoles  = new ConcurrentBag<Role>(viewModel.AllRoles);
            AppCach.AllCategories = new ConcurrentBag<Category>(viewModel.AllCategories);
            AppCach.AllNews = new ConcurrentBag<News>(viewModel.AllNews);
            AppCach.AllProjectRoles = new ConcurrentBag<ProjectRole>(viewModel.AllProjectRoles);
            AppCach.AllProjects = new ConcurrentBag<Project>(viewModel.AllProjects);
            //AppCach.Current.Application.UnLock();
            return View(viewModel);
        }
    }
}