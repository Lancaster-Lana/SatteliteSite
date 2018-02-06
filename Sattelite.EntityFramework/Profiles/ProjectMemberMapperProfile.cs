namespace Sattelite.EntityFramework.Profiles
{
    using AutoMapper;
    using Sattelite.Entities;
    using Sattelite.EntityFramework.ViewModels.Admin.Project;

    public class ProjectMemberMapperProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<ProjectMember, ProjectMemberViewModel>()
                .ForMember(x => x.ProjectId, o => o.MapFrom(m => m.Id))
                .ForMember(x => x.ProjectRole, o => o.MapFrom(m => m.ProjectRole));
            Mapper.CreateMap<ProjectMemberViewModel, ProjectMember>()
                .ForMember(x => x.Id, o => o.MapFrom(m => m.ProjectId))
                .ForMember(x => x.ProjectRole, o => o.MapFrom(m => m.ProjectRole));

            //Mapping ProjectRole
            Mapper.CreateMap<ProjectRole, ProjectMemberRoleViewModel>()
                .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
                .ForMember(x => x.Name, o => o.MapFrom(m => m.Name));
            Mapper.CreateMap<ProjectMemberRoleViewModel, ProjectRole>()
                .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
                .ForMember(x => x.Name, o => o.MapFrom(m => m.Name));
        }
    }
}