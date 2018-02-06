using System.Collections.Concurrent;
using Sattelite.Base;
using Sattelite.Entities;

namespace Sattelite.EntityFramework.ViewModels.Admin.User
{
    public class UserListViewModel : BaseListViewModel
    {
        public ConcurrentBag<Entities.User> AllUsers;

        public UserListViewModel(ConcurrentBag<Entities.User> allUsers)
        {
            this.AllUsers = allUsers;
        }
    }
}
