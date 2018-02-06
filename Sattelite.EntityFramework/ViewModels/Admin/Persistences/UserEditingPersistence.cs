namespace Sattelite.EntityFramework.ViewModels.Admin.Persistences
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Data;
    using Sattelite.Entities;
    using Sattelite.Entities.UserAgg;
    using Sattelite.EntityFramework.Repository;
    using Sattelite.Framework.Extensions;

    public class UserEditingPersistence : IUserEditingPersistence
    {
        private readonly IUserRepository _userRepository;
        private readonly ICategoryRepository _categoryRepository;

        public UserEditingPersistence(IUserRepository repository, ICategoryRepository categoryRepository)
        {
            _userRepository = repository;
            _categoryRepository = categoryRepository;
        }

        public bool SaveUser(User user, List<int> newCategoriesToSubscribe = null)
        {
            var oldUser = _userRepository.GetById(user.Id);

            //TODO: _userRepository.ValidateUser() - check if there are duplicate users names

            if (oldUser == null && oldUser.UserName == null)
                throw new NoNullAllowedException(string.Format("User with id={0}", user.Id).ToNotNullErrorMessage());

            //Update user properties
            if (!oldUser.UserName.Equals(user.UserName, StringComparison.InvariantCulture))
                oldUser.UserName = user.UserName;
            if (!oldUser.DisplayName.Equals(user.DisplayName, StringComparison.InvariantCulture))
                oldUser.DisplayName = user.DisplayName;
            if (!oldUser.Email.Equals(user.Email, StringComparison.InvariantCulture))
                oldUser.Email = user.Email;
            if (!oldUser.Password.Equals(user.Password, StringComparison.InvariantCulture) && !string.IsNullOrWhiteSpace(user.Password))
                oldUser.Password = user.Password;

            oldUser.RoleId = user.RoleId;
            oldUser.ModifiedDate = DateTime.Now;

            //refresh related collections
            //Re-create subscriptions for user
            if (newCategoriesToSubscribe != null)
                _categoryRepository.AddUserSubscriptions(oldUser, newCategoriesToSubscribe, true);
            //else
            //    _categoryRepository.RemoveAllUserSubscriptions(user.UserName); // delete old subscriptions

            //OR just refresh
            //oldUser.Subscriptions = newUserSubscriptions;
            //var oldIRolePermissions = this._roleRepository.GetByPermissionsByRoleId(role.Id);

            return _userRepository.SaveUser(oldUser);
        }

        public bool SaveUser(User user, List<CategorySubscription> newCategoriesToSubscribe = null)
        {
            return SaveUser(user, newCategoriesToSubscribe.Select(s => s.CategoryId).ToList());
        }
    }
}