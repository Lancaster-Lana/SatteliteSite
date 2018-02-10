using System.Collections.Concurrent;
using System.Collections.Generic;
using Sattelite.Base;

namespace Sattelite.EntityFramework.ViewModels.Admin.User
{
    public class UserListViewModel : BaseListViewModel
    {
        public List<Entities.User> Users { get; set; }// = new List<Entities.User>();
    }
}
