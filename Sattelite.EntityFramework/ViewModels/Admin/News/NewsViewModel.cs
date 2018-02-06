namespace Sattelite.EntityFramework.ViewModels.Admin.News
{
    using System.Web;
    using System.ComponentModel.DataAnnotations;

    using Sattelite.Base;

    public class NewsViewModel : BaseViewModel
    {
        /// <summary>
        /// Article\News Id
        /// </summary>
        public int? NewsId { get; set; }

        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Назва обов'язкова")]
        public string Title { get; set; }

        //[Required(ErrorMessage = "Короткий опис статті обов'язковий")]
        //public string ShortDescription { get; set; } //= Title;

        public string Content { get; set; }

        [FileExtensions(Extensions = "jpg", ErrorMessage = "Specify a jpg file. (Comma-separated values)")]
        public HttpPostedFileBase SmallImage { get; set; } = null;
        public string SmallImagePath { get; set; }

        [FileExtensions(Extensions = "jpg", ErrorMessage = "Specify a jpg file. (Comma-separated values)")]
        public HttpPostedFileBase MediumImage { get; set; } = null;
        public string MediumImagePath { get; set; }

        [FileExtensions(Extensions = "jpg", ErrorMessage = "Specify a jpg file. (Comma-separated values)")]
        public HttpPostedFileBase BigImage { get; set; } = null;

        public string BigImagePath { get; set; }
    }
}