﻿
namespace Sattelite.EntityFramework.ViewModels.Admin.Persistences
{
    public interface IProjectDeletingPersistence
    {
        bool DeleteProject(int id);
        bool DeleteProjectMember(int projectId, int projectMemberId);
    }
}
