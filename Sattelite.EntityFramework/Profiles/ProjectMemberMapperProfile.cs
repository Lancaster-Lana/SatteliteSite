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
                .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
                .ForMember(x => x.ProjectId, o => o.MapFrom(m => m.ProjectId))
                .ForMember(x => x.UserId, o => o.MapFrom(m => m.UserId))
                .ForMember(x => x.ProjectUser, o => o.MapFrom(m => m.User))
                .ForMember(x => x.ProjectRoleId, o => o.MapFrom(m => m.ProjectRoleId))
                .ForMember(x => x.ProjectRole, o => o.MapFrom(m => m.ProjectRole))
                .ForMember(x => x.CreatedDate, o => o.MapFrom(m => m.CreatedDate))
                .ForMember(x => x.CreatedBy, o => o.MapFrom(m => m.CreatedBy))
                .ForMember(x => x.ModifiedDate, o => o.MapFrom(m => m.ModifiedDate))
                //.ForMember(x => x.ModifiedBy, o => o.MapFrom(m => m.ModifiedBy))
                ;
            Mapper.CreateMap<ProjectMemberViewModel, ProjectMember>()
                .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
                .ForMember(x => x.ProjectId, o => o.MapFrom(m => m.ProjectId))
                .ForMember(x => x.UserId, o => o.MapFrom(m => m.UserId))
                .ForMember(x => x.User, o => o.MapFrom(m => m.ProjectUser))
                .ForMember(x => x.ProjectRoleId, o => o.MapFrom(m => m.ProjectRoleId))
                .ForMember(x => x.ProjectRole, o => o.MapFrom(m => m.ProjectRole))
                .ForMember(x => x.CreatedDate, o => o.MapFrom(m => m.CreatedDate))
                .ForMember(x => x.CreatedBy, o => o.MapFrom(m => m.CreatedBy))
                .ForMember(x => x.ModifiedDate, o => o.MapFrom(m => m.ModifiedDate));
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