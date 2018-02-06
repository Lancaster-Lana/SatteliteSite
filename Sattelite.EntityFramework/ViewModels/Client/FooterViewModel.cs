namespace Sattelite.EntityFramework.ViewModels.Client
{
    using System.Collections.Generic;

    using Sattelite.Entities;
    using Sattelite.Entities.ProjectAgg;

    public class FooterViewModel
    {
        public FooterViewModel()
        {
            this.Categories = new List<Category>();
        }

        public List<Category> Categories { get; set; }
    }
}