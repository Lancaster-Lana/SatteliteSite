namespace Sattelite.EntityFramework.ViewModels.Client
{
    using System.Collections.Generic;

    using Sattelite.Entities;
    using Sattelite.Entities.ProjectAgg;

    public class MainPageViewModel
    {
        public LeftColumnViewModelBase LeftColumn { get; set; }

        public MainPageRightColumnViewModel RightColumn { get; set; }
    }

    public abstract class LeftColumnViewModelBase
    {

    }

    public class MainPageLeftColumnViewModel : LeftColumnViewModelBase
    {
        public MainPageLeftColumnViewModel()
        {
            this.RemainItems = new List<News>();
        }

        public News FirstItem { get; set; }

        public List<News> RemainItems { get; set; }
    }

    public class DetailsLeftColumnViewModel : LeftColumnViewModelBase
    {
        public News CurrentItem { get; set; }
    }

    public class CategoryLeftColumnViewModel : LeftColumnViewModelBase
    {
        public List<News> News { get; set; }= new List<News>();
    }

    public class MainPageRightColumnViewModel
    {
        public List<News> LatestNews { get; set; }
        public List<News> MostViews { get; set; }

        public List<Project> Projects { get; set; }

        public MainPageRightColumnViewModel()
        {
            this.LatestNews = new List<News>();
            this.MostViews = new List<News>();
            this.Projects = new List<Project>();
        }
    }
}