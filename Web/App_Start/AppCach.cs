using Sattelite.Entities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Sattelite.Web
{
    /// <summary>
    /// Common thread-safe collections: 
    /// roles, categories, etc. to be reusable in different views in dropdowns 
    /// </summary>
    public static class AppCach
    {
        public static ConcurrentBag<News> AllNews { get; set; }
        public static ConcurrentBag<Project> AllProjects { get; set; }
        public static ConcurrentBag<Category> AllCategories { get; set; }

        public static ConcurrentBag<User> AllUsers { get; set; }
        public static ConcurrentBag<Role> AllRoles { get; set; }
        public static ConcurrentBag<ProjectRole> AllProjectRoles { get; set; }
    }
}