namespace Sattelite.EntityFramework.ViewModels.Admin.Persistences
{
    using System.Web.Mvc;
    using Sattelite.Entities;
    using Sattelite.EntityFramework.Repository;

    public class UserCreatingPersistence : IUserCreatingPersistence
    {
        private readonly IUserRepository _userRepository;

        public UserCreatingPersistence()
            : this(DependencyResolver.Current.GetService<IUserRepository>())
        {
        }

        public UserCreatingPersistence(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        //public bool CreateUser(User user)
        //{
        //    int id = _userRepository.CreateUser(user);
        //    return id > 0;
        //}

        public bool CreateUser(User user)
        {
            return _userRepository.SaveUser(user);
        }
    }
}