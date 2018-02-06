namespace Sattelite.EntityFramework.Profiles
{
    using AutoMapper;
    using Sattelite.Entities;
    using Sattelite.EntityFramework.ViewModels.Admin.Project;

    public class ProjectMapperProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Project, ProjectViewModel>()
                .ForMember(x => x.ProjectId, o => o.MapFrom(m => m.Id))
                .ForMember(x => x.CategoryId, o => o.MapFrom(m => m.CategoryId))
                .ForMember(x => x.CoordinatorId, o => o.MapFrom(m => m.CoordinatorId))
                .ForMember(x => x.Name, o => o.MapFrom(m => m.ProjectContent.Name))
                .ForMember(x => x.ShortDescription, o => o.MapFrom(m => m.ProjectContent.ShortDescription))
                .ForMember(x => x.Content, o => o.MapFrom(m => m.ProjectContent.Content))
                .ForMember(x => x.ProjectMembers, o => o.MapFrom(m => m.ProjectMembers));

            Mapper.CreateMap<Project, ProjectEditingViewModel>()
                .ForMember(x => x.ProjectId, o => o.MapFrom(m => m.Id))
                .ForMember(x => x.CategoryId, o => o.MapFrom(m => m.CategoryId))
                .ForMember(x => x.CoordinatorId, o => o.MapFrom(m => m.CoordinatorId))
                .ForMember(x => x.Name, o => o.MapFrom(m => m.ProjectContent.Name))
                .ForMember(x => x.ShortDescription, o => o.MapFrom(m => m.ProjectContent.ShortDescription))
                .ForMember(x => x.Content, o => o.MapFrom(m => m.ProjectContent.Content))
                .ForMember(x => x.ProjectMembers, o => o.MapFrom(m => m.ProjectMembers));
            //Mapper.CreateMap<ProjectContentViewModel, ProjectContent>()
                //  .ForMember(x => x.Name, o => o.MapFrom(m => m.Name))
                //  .ForMember(x => x.ShortDescription, o => o.MapFrom(m => m.ShortDescription))
                //  .ForMember(x => x.Content, o => o.MapFrom(m => m.Content))
                //  //.TODO: other data, created by
            Mapper.CreateMap<ProjectEditingViewModel, Project>()
                .ForMember(x => x.Id, o => o.MapFrom(m => m.ProjectId))
                .ForMember(x => x.CategoryId, o => o.MapFrom(m => m.CategoryId))
                .ForMember(x => x.CoordinatorId, o => o.MapFrom(m => m.CoordinatorId))
                //.ForMember(x => x.ProjectContent.Name, o => o.MapFrom(m => m.Name))
                //.ForMember(x => x.ProjectContent.ShortDescription, o => o.MapFrom(m => m.ShortDescription))
                //.ForMember(x => x.ProjectContent.Content, o => o.MapFrom(m => m.Content))
                .ForMember(x => x.ProjectMembers, o => o.MapFrom(m => m.ProjectMembers));

            //Mapper.CreateMap<Project, ProjectCreatingViewModel>()
            //    .ForMember(x => x.CategoryId, o => o.MapFrom(m => m.CategoryId))
            //    .ForMember(x => x.CoordinatorId, o => o.MapFrom(m => m.CoordinatorId))

            //    .ForMember(x => x.Name, o => o.MapFrom(m => m.ProjectContent.Name))
            //    .ForMember(x => x.ShortDescription, o => o.MapFrom(m => m.ProjectContent.ShortDescription))
            //    .ForMember(x => x.Content, o => o.MapFrom(m => m.ProjectContent.Content))

            //    .ForMember(x => x.ProjectMembers, o => o.MapFrom(m => m.ProjectMembers));

            Mapper.CreateMap<ProjectCreatingViewModel, Project>()
                .ForMember(x => x.Id, o => o.MapFrom(m => m.ProjectId))
                .ForMember(x => x.CategoryId, o => o.MapFrom(m => m.CategoryId))
                .ForMember(x => x.CoordinatorId, o => o.MapFrom(m => m.CoordinatorId))
                //.ForMember(x => x.ProjectContent.Name, o => o.MapFrom(m => m.Name))
                //.ForMember(x => x.ProjectContent.ShortDescription, o => o.MapFrom(m => m.Content))
                //.ForMember(x => x.ProjectContent.Content, o => o.MapFrom(m => m.Content));
                .ForMember(x => x.ProjectMembers, o => o.MapFrom(m => m.ProjectMembers));
        }
    }
}