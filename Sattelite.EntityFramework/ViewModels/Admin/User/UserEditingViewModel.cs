namespace Sattelite.EntityFramework.ViewModels.Admin.User
{
    using System.Collections.Generic;
    using Sattelite.Entities;

    public class UserEditingViewModel : UserViewModel
    {
        public List<CategorySubscription> Subscriptions { get; set; } = new List<CategorySubscription>();
    
        /// <summary>
        /// User active subscriptions
        /// </summary>
        public string SubscriptionsIds { get; set; } //comma separated Subscriptions Ids - from AJAX call  //public int[] NewSubscribedCategories { get; set; }
    }
}