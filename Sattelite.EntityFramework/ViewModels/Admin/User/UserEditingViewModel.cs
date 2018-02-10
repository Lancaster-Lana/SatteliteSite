namespace Sattelite.EntityFramework.ViewModels.Admin.User
{
    using System.Collections.Generic;
    using Sattelite.EntityFramework.ViewModels.Admin.Category;

    public class UserEditingViewModel : UserViewModel
    {
        public List<CategorySubscriptionViewModel> Subscriptions { get; set; } = new List<CategorySubscriptionViewModel>();
    }
}