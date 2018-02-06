namespace Sattelite.EntityFramework.ViewModels.Admin.Persistences
{
    using System.Collections.Generic;
    using Sattelite.Entities;

    public interface IUserEditingPersistence
    {
        bool SaveUser(User user, List<int> newCategoriesToSubscribe = null);
        bool SaveUser(User user, List<CategorySubscription> newCategoriesToSubscribe = null); //bool UpdateUserSubscriptions(string userName, int id);
    }
}
