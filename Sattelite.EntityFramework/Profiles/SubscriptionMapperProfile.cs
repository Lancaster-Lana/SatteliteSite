namespace Sattelite.EntityFramework.Profiles
{
    using AutoMapper;
    using Sattelite.Entities;
    using Sattelite.EntityFramework.ViewModels.Admin.Category;

    /// <summary>
    /// Category subscription for user
    /// </summary>
    public class SubscriptionMapperProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<CategorySubscription, CategorySubscriptionViewModel>()
                .ForMember(x => x.CategoryId, o => o.MapFrom(m => m.CategoryId))
                .ForMember(x => x.CategoryName, o => o.MapFrom(m => m.Category.Name))
                .ForMember(x => x.UserId, o => o.MapFrom(m => m.UserId))
                .ForMember(x => x.UserName, o => o.MapFrom(m => m.User.UserName));

            Mapper.CreateMap<CategorySubscriptionViewModel, CategorySubscription>()
                .ForMember(x => x.CategoryId, o => o.MapFrom(m => m.CategoryId))
                //.ForMember(x => x.Category.Name, o => o.MapFrom(m => m.CategoryName))
                .ForMember(x => x.UserId, o => o.MapFrom(m => m.UserId));
                //.ForMember(x => x.User.UserName, o => o.MapFrom(m => m.UserName));
        }
    }
}