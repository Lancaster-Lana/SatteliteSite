namespace Sattelite.EntityFramework.Profiles
{
    using AutoMapper;
    using Sattelite.Entities;
    using Sattelite.EntityFramework.ViewModels.Admin.Role;

    public class RoleMapperProfile : Profile 
    {
        protected override void Configure()
        {
           // Mapper.CreateMap<List<Role>, RoleViewModel>()
            //    .ForMember(x => x.Roles, o => o.MapFrom(m => m.Roles));

            Mapper.CreateMap<Role, RoleEditingViewModel>()
             .ForMember(x => x.RoleId, o => o.MapFrom(m => m.Id))
             .ForMember(x => x.Name, o => o.MapFrom(m => m.Name))
             .ForMember(x => x.Description, o => o.MapFrom(m => m.Description))
             .ForMember(x => x.Permissions, o => o.MapFrom(m => m.CategoryPermissions));
        }
    }
}