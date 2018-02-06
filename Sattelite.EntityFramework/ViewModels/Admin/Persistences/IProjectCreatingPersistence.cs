
namespace Sattelite.EntityFramework.ViewModels.Admin.Persistences
{
    using Sattelite.Entities;

    public interface IProjectCreatingPersistence
    {
        bool CreateProject(Project project);
    }
}
