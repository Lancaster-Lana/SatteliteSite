namespace Sattelite.EntityFramework.ViewModels.Admin.Project
{
    using System.Collections.Generic;
    using Sattelite.Base;
    using Sattelite.Entities;

    public class ProjectListViewModel : BaseListViewModel
    {
        /// <summary>
        /// current category to be filter projects
        /// </summary>
        public int CategoryId = -1;

        public List<Project> Projects { get; set; }// = new List<Project>();

        //private int pageNumber;

        //public ProjectListViewModel(int pageNumber)
        //{
        //    this.pageNumber = pageNumber;
        //}
    }
}
