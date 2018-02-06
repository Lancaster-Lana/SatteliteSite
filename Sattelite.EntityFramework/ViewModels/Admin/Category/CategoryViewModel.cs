using Sattelite.Base;
using System.ComponentModel.DataAnnotations;

namespace Sattelite.EntityFramework.ViewModels.Admin.Category
{
    public class CategoryViewModel : BaseViewModel
    {
        public int CategoryId { get; set; }

        [Required (ErrorMessage = "Назва категорії обов'язкова")]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}