namespace Sattelite.EntityFramework.ViewModels.Admin.News
{
    using System.Collections.Generic;
    using Sattelite.Base;
    using Sattelite.Entities;

    public class NewsListViewModel : BaseListViewModel
    {
        public List<News> News { get; set; } = new List<News>();

        private int pageNumber;
        public NewsListViewModel(int pageNumber)
        {
            this.pageNumber = pageNumber;
        }
    }
}
