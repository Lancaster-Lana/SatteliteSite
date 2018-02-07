namespace Sattelite.EntityFramework.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;

    using Sattelite.Data;
    using Sattelite.Entities;
    using WebMatrix.WebData;

    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository()
            : this(DependencyResolver.Current.GetService<SatteliteDBContext>())
        {
        }

        public CategoryRepository(SatteliteDBContext context)
            : base(context)
        {
        }

        public Category GetById(int id)
        {
            return GetOne(x => x.Id == id,
                           x => x.Subscriptions); //with related entities
        }

        public Category GetByName(string name)
        {
            return GetOne(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Category> GetCategories()
        {
            return GetIncluding(x => x.CreatedDate.HasValue,
                             x => x.Subscriptions);
        }

        public bool SaveCategory(Category category)
        {
            //try catch
            Save(category);

            return true;
        }

        public bool DeleteCategory(Category category)
        {
            Delete(category);
            return true;
        }

        public bool IncreaseNumOfView(int id)
        {
            var category = GetById(id);

            if (category != null)
            {
                //TODO:
                //category.NumOfView = category.NumOfView + 1;
                return SaveCategory(category);
            }

            return false;
        }

        #region Methods for user category subscriptions

        /// <summary>
        /// Get categories that user is subscribed to (to be visible as menu items) 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public IEnumerable<Category> GetUserCategories(string userName)
        {
            var subscriptions = GetQuery<CategorySubscription>();
            var userSubscribedCats = GetUserSubscriptions(userName).Select(s => s.CategoryId).ToList();
            return Get(cat => userSubscribedCats.Contains(cat.Id));
        }

        public IEnumerable<Category> GetUserCategories(int userId)
        {
            var subscriptions = GetQuery<CategorySubscription>();
            var userSubscribedCats = GetUserSubscriptions(userId).Select(s => s.CategoryId).ToList();
            return Get(cat => userSubscribedCats.Contains(cat.Id));
        }

        public IEnumerable<CategorySubscription> GetUserSubscriptions(string userName)
        {
            var subscriptions = GetQuery<CategorySubscription>();
            return subscriptions.Where(s => userName.Equals(s.User.UserName, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<CategorySubscription> GetUserSubscriptions(int userId)
        {
            var subscriptions = GetQuery<CategorySubscription>();
            return subscriptions.Where(s => userId == s.Id);
        }

        /// <summary>
        /// Subscribe user to a single category (news, projects)
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool AddUserSubscription(string userName, int categoryId)
        {
            var subsriptions = GetUserCategories(userName);

            if (!subsriptions.Any(c => c.Id == categoryId))
            {
                SubscribeToCategory(userName, categoryId);
                UnitOfWork.SaveChanges();
            }
            return true;
        }

        public bool AddUserSubscription(int userId, int categoryId)
        {
            var subsriptions = GetUserCategories(userId);

            if (!subsriptions.Any(c => c.Id == categoryId))
            {
                SubscribeToCategory(userId, categoryId);
                UnitOfWork.SaveChanges();
            }
            return true;
        }

        /// <summary>
        /// Subscribe user to categories (that absent in his\her current subscriptions)
        /// </summary>
        /// <param name="categoryIds"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool AddUserSubscriptions(string userName, List<int> newCategoriesForSubscriptionIds, bool removeOldSubscriptions)
        {
            //UnitOfWork.BeginTransaction();

            var userSubscriptions = GetUserSubscriptions(userName);
            var userSubscribedCategories = userSubscriptions.Select(c => c.CategoryId).ToList();

            //var userSubscribedCategories = GetUserSubscriptions(userName).Select(s => s.CategoryId);
            var newSubscriptions = newCategoriesForSubscriptionIds.Except(userSubscribedCategories);

            //Remove non-actual subscriptions
            if (removeOldSubscriptions)
            {
                var oldSubscriptions = userSubscriptions.Where(s => !newCategoriesForSubscriptionIds.Contains(s.CategoryId)).ToList();
                UnsubscribeUserFrom(oldSubscriptions);
            }

            foreach (var categoryId in newSubscriptions)
            {
                SubscribeToCategory(userName, categoryId);
            }

            //UnitOfWork.SaveChanges();

            return true;
        }

        public bool AddUserSubscriptions(User user, List<int> newCategoriesForSubscriptionIds, bool removeOldSubscriptions)
        {
            var userSubscriptions = user.Subscriptions;
            var userSubscribedCategories = userSubscriptions.Select(c => c.CategoryId).ToList();

            //var userSubscribedCategories = GetUserSubscriptions(userName).Select(s => s.CategoryId);
            var newSubscriptions = newCategoriesForSubscriptionIds.Except(userSubscribedCategories);

            //Remove non-actual subscriptions
            if (removeOldSubscriptions)
            {
                var oldSubscriptions = userSubscriptions.Where(s => !newCategoriesForSubscriptionIds.Contains(s.CategoryId)).ToList();
                UnsubscribeUserFrom(oldSubscriptions);
            }

            //UnitOfWork.BeginTransaction();
            foreach (var categoryId in newSubscriptions)
            {
                SubscribeToCategory(user.Id, categoryId);
            }

            //UnitOfWork.SaveChanges();

            return true;
        }

        private void SubscribeToCategory(int userId, int categoryId)
        {
            var subscription = new CategorySubscription
            {
                CategoryId = categoryId,
                UserId = userId,
                CreatedDate = DateTime.Now,
                CreatedBy = HttpContext.Current.User?.Identity?.Name
            };

            DbContext.Set<CategorySubscription>().Add(subscription);
        }

        private void SubscribeToCategory(string userName, int categoryId)
        {
            WebSecurity.InitializeDatabaseConnection(CONSTS.DefaultConnectionString, "User", "Id", "UserName", autoCreateTables: true);
            var userId = (int)Membership.GetUser(userName)?.ProviderUserKey;//HttpContext.Current.User.Identity.UserName;
            SubscribeToCategory(userId, categoryId);
        }

        /// <summary>
        /// Remove sinle user category subscription
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public bool RemoveUserSubscription(string userName, int categoryId)
        {
            var categorySubscription = GetUserSubscriptions(userName).FirstOrDefault(s => s.CategoryId == categoryId);
            if (categorySubscription != null)
                DbContext.Set<CategorySubscription>().Remove(categorySubscription);

            return true;
        }

        public bool RemoveUserSubscription(User user, int categoryId)
        {
            var categorySubscription = user.Subscriptions.FirstOrDefault(s => s.CategoryId == categoryId);

            if (categorySubscription != null)
                DbContext.Set<CategorySubscription>().Remove(categorySubscription);

            return true;
        }

        public bool RemoveUserSubscriptions(string userName, List<int> categoryIds)
        {
            var userSubscriptions = GetUserSubscriptions(userName)?.Where(s => categoryIds.Contains(s.CategoryId)).ToList();
            return UnsubscribeUserFrom(userSubscriptions);
        }

        public bool RemoveUserSubscriptions(User user, List<int> categoryIds)
        {
            var userSubscriptions = user.Subscriptions?.Where(s => categoryIds.Contains(s.CategoryId)).ToList();
            return UnsubscribeUserFrom(userSubscriptions);
        }

        public bool RemoveAllUserSubscriptions(string userName)
        {
            var userSubscriptions = GetUserSubscriptions(userName);
            foreach (var sub in userSubscriptions)
                DbContext.Set<CategorySubscription>().Remove(sub);

            //save all subscriptions
            UnitOfWork.SaveChanges();

            return true;
        }

        private bool UnsubscribeUserFrom(List<CategorySubscription> userSubscriptions)
        {
            foreach (var sub in userSubscriptions)
                DbContext.Set<CategorySubscription>().Remove(sub);
            return true;
        }

        #endregion
    }
}