namespace Sattelite.Entities
{
    public class NewsContent : Entity
    {
        public string Title { get; set; } = string.Empty;

        //[Required(ErrorMessage = "Short description is required")]
        public string ShortDescription { get; set; } = string.Empty;

        public string Content { get; set; }

        public string SmallImage { get; set; }

        public string MediumImage { get; set; }

        public string BigImage { get; set; }

        //public virtual ICollection<News> News { get; set; } = new List<News>();   
    }
}