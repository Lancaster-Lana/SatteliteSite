namespace Sattelite.EntityFramework.ViewModels.Client
{
    using System.Collections.Generic;

    using Sattelite.Entities;
    using Sattelite.Entities.ProjectAgg;

    public class HeaderViewModel
    {
        public HeaderViewModel()
        {
            this.Categories = new List<Category>();
        }

        public string SiteTitle { get; set; }

        public int CurrentCategoryId { get; set; }

        public List<Category> Categories { get; set; }
    }
}