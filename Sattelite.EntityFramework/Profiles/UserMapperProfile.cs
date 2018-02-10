namespace Sattelite.EntityFramework.Profiles
{
    using AutoMapper;

    using Sattelite.Entities;
    using Sattelite.EntityFramework.Security;
    using Sattelite.EntityFramework.ViewModels.Admin.User;

    public class UserMapperProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<User, UserViewModel>()
                .ForMember(x => x.UserId, o => o.MapFrom(m => m.Id))
                .ForMember(x => x.UserName, o => o.MapFrom(m => m.UserName))
                .ForMember(x => x.Email, o => o.MapFrom(m => m.Email));

            Mapper.CreateMap<UserViewModel, User>()
                .ForMember(x => x.Id, o => o.MapFrom(m => m.UserId))
                .ForMember(x => x.UserName, o => o.MapFrom(m => m.UserName))
                .ForMember(x => x.Email, o => o.MapFrom(m => m.Email));

            Mapper.CreateMap<User, UserEditingViewModel>()
                 .ForMember(x => x.UserId, o => o.MapFrom(m => m.Id))
                 .ForMember(x => x.UserName, o => o.MapFrom(m => m.UserName))
                 .ForMember(x => x.Password, o => o.MapFrom(m => m.Password))
                 //.ForMember(x => x.ConfirmPassword, o => o.MapFrom(m => m.ConfirmPassword))
                 .ForMember(x => x.DisplayName, o => o.MapFrom(m => m.DisplayName))
                 .ForMember(x => x.Email, o => o.MapFrom(m => m.Email))
                 .ForMember(x => x.RoleId, o => o.MapFrom(m => m.RoleId))
                 // .ForMember(x => x.Role, o => o.MapFrom(m => m.Role)) //TODO:
                 .ForMember(x => x.Subscriptions, o => o.MapFrom(m => m.Subscriptions));

            Mapper.CreateMap<UserEditingViewModel, User>()
                 .ForMember(x => x.Id, o => o.MapFrom(m => m.UserId))
                 .ForMember(x => x.UserName, o => o.MapFrom(m => m.UserName))
                 .ForMember(x => x.DisplayName, o => o.MapFrom(m => m.DisplayName))
                 .ForMember(x => x.Password, o => o.MapFrom(m => m.Password))
                 .ForMember(x => x.Email, o => o.MapFrom(m => m.Email))
                 .ForMember(x => x.RoleId, o => o.MapFrom(m => m.RoleId))
                 .ForMember(x => x.Subscriptions, o => o.MapFrom(m => m.Subscriptions));
        }
    }
}