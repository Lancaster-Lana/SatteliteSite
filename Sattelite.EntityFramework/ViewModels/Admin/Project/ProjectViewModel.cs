namespace Sattelite.EntityFramework.ViewModels.Admin.Project
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Sattelite.Entities;
    using Sattelite.Base;

    public class ProjectViewModel : BaseViewModel
    {
        public int? ProjectId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public int? CoordinatorId { get; set; }

        [Required]
        public string Name { get; set; }

        /// <summary>
        /// TODO: Project additional details like links on sources, etc.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        ///Contains links, sources
        /// </summary>
        //public ProjectContentViewModel ProjectContent { get; set; }

        #region Existing collections

        /// <summary>
        ///  People working on the project
        /// </summary>
        public ICollection<ProjectMember> ProjectMembers { get; set; } = new List<ProjectMember>();

        #endregion
    }
}