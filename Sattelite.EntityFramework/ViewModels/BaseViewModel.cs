
using Sattelite.Entities;
using Sattelite.EntityFramework.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sattelite.Base
{
    public class BaseViewModel
    {
        [Required(ErrorMessage = "Короткий опис обов'язковий")]
        public string ShortDescription { get; set; }

        public string TitleSearchText { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        //public string ModifiedBy { get; set; }

        public PagingViewModel PagingData { get; set; } = new PagingViewModel();

        #region Global collections

        /// <summary>
        /// All users (for coordinator assignment)
        /// </summary>
        //public List<User> AllUsers { get; set; } = new List<User>();

        //public List<Role> AllRoles { get; set; } = new List<Role>();

        //public List<Category> AllCategories { get; set; } = new List<Category>();

        //public List<Project> AllProjects { get; set; } = new List<Project>();

        //public List<ProjectRole> AllProjectMemberRoles { get; set; } = new List<ProjectRole>();

        #endregion
    }
}