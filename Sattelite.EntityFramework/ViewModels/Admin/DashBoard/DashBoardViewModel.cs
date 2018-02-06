namespace Sattelite.EntityFramework.ViewModels.Admin.DashBoard
{
    using System.Collections.Generic;

    using Sattelite.Entities;

    /// <summary>
    /// Contains information about all collections
    /// </summary>
    public class DashBoardViewModel
    {
        public List<News> AllNews { get; set; } = new List<News>();
        public List<Project> AllProjects { get; set; } = new List<Project>();
        public List<Category> AllCategories { get; set; } = new List<Category>();

        public List<User> AllUsers { get; set; } = new List<User>();
        public List<Role> AllRoles { get; set; } = new List<Role>();
        public List<ProjectRole> AllProjectRoles { get; set; } = new List<ProjectRole>();

        public string TitleSearchText { get; set; }
        public PagingViewModel PagingData { get; set; } = new PagingViewModel();
    }
}