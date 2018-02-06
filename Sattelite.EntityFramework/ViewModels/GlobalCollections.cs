using Sattelite.Entities;
using System;
using System.Collections.Generic;

namespace Sattelite.EntityFramework.ViewModels
{
    public static class GlobalCollections
    {
        /// <summary>
        /// All users (for coordinator assignment)
        /// </summary>
        public static List<User> AllUsers { get; set; } = new List<User>();

        public static List<Role> AllRoles { get; set; } = new List<Role>();

        public static List<Category> AllCategories { get; set; } = new List<Category>();

        public static List<Project> AllProjects { get; set; } = new List<Project>();

        public static List<ProjectRole> AllProjectMemberRoles { get; set; } = new List<ProjectRole>();
    }
}
