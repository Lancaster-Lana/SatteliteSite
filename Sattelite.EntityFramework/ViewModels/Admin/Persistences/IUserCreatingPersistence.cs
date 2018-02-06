
namespace Sattelite.EntityFramework.ViewModels.Admin.Persistences
{
    using Sattelite.Entities;

    public interface IUserCreatingPersistence
    {
        bool CreateUser(User user);
    }
}
