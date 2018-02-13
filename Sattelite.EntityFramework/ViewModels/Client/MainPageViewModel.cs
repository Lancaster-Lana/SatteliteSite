namespace Sattelite.EntityFramework.ViewModels.Client
{
    using System.Collections.Generic;
    using Sattelite.Entities;

    public class MainPageViewModel
    {
        public LeftColumnViewModelBase /*DetailsLeftColumnViewModel MainPageLeftColumnViewModel */ LeftColumn { get; set; }

        public MainPageRightColumnViewModel RightColumn { get; set; }
    }

    public abstract class LeftColumnViewModelBase
    {

    }

    public class MainPageLeftColumnViewModel : LeftColumnViewModelBase
    {
        public News FirstArticle { get; set; }

        public List<News> RemainNews { get; set; } = new List<News>();
    }

    public class DetailsLeftColumnViewModel : LeftColumnViewModelBase
    {
        public News CurrentArticle { get; set; }

        public Project CurrentProject { get; set; }
    }

    public class CategoryLeftColumnViewModel : LeftColumnViewModelBase
    {
        public List<News> News { get; set; } = new List<News>();

        public List<Project> Projects { get; set; } = new List<Project>();
    }

    public class MainPageRightColumnViewModel
    {
        public List<News> LatestNews { get; set; } = new List<News>();
        public List<News> MostViews { get; set; } = new List<News>();
        public List<Project> Projects { get; set; } = new List<Project>();
    }
}