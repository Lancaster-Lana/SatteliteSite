namespace Sattelite.EntityFramework.Repository
{
    using Sattelite.Entities;
    using System.Collections.Generic;

    public interface ICategoryRepository
    {
        Category GetById(int id);

        Category GetByName(string name);

        IEnumerable<Category> GetCategories();

        bool SaveCategory(Category category);

        bool DeleteCategory(Category category);

        IEnumerable<Category> GetUserCategories(string userName);

        IEnumerable<CategorySubscription> GetUserSubscriptions(string userName);

        bool AddUserSubscription(string userName, int categoryId);

        bool AddUserSubscription(int userId, int categoryId);

        bool AddUserSubscriptions(User user, List<int> newCategoriesForSubscriptionIds, bool removeOldSubscriptions);

        bool AddUserSubscriptions(string userName, List<int> newCategoriesForSubscriptionIds, bool removeOldSubsriptions);

        bool RemoveUserSubscription(string userName, int categoryId);
        bool RemoveUserSubscription(User user, int categoryId);

        bool RemoveUserSubscriptions(string userName, List<int> categoryIds);

        bool RemoveUserSubscriptions(User user, List<int> categoryIds);

        bool RemoveAllUserSubscriptions(string userName);

        bool IncreaseNumOfView(int id);
    }
}