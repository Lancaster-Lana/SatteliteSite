namespace Sattelite.EntityFramework.Profiles
{
    using AutoMapper;
    using Sattelite.Entities;
    using Sattelite.EntityFramework.ViewModels.Admin.Role;

    public class ProjectRoleMapperProfile : Profile 
    {
        protected override void Configure()
        {
            Mapper.CreateMap<ProjectRole, ProjectRoleViewModel>()
             .ForMember(x => x.RoleId, o => o.MapFrom(m => m.Id))
             .ForMember(x => x.Name, o => o.MapFrom(m => m.Name))
             .ForMember(x => x.ShortDescription, o => o.MapFrom(m => m.Description));
            //.ForMember(x => x.ProjectPermissions, o => o.MapFrom(m => m.ProjectPermissions));

            Mapper.CreateMap<ProjectRoleViewModel, ProjectRole>()
             .ForMember(x => x.Id, o => o.MapFrom(m => m.RoleId))
             .ForMember(x => x.Name, o => o.MapFrom(m => m.Name))
             .ForMember(x => x.Description, o => o.MapFrom(m => m.ShortDescription));
        }
    }
}