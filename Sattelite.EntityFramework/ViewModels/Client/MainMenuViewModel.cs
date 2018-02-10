namespace Sattelite.EntityFramework.ViewModels.Client
{
    using System.Collections.Generic;
    using Sattelite.Entities;

    public class MainMenuViewModel
    {
        public string SiteTitle { get; set; }

        public int CurrentCategoryId { get; set; }

        /// <summary>
        /// Categories on which user is subscribed for
        /// </summary>
        public List<Category> Categories { get; set; } = new List<Category>();
    }
}