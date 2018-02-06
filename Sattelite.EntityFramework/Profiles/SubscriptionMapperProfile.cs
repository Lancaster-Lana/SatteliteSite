namespace Sattelite.EntityFramework.Profiles
{
    using AutoMapper;
    using Sattelite.Entities;
    using Sattelite.EntityFramework.ViewModels.Admin.Category;

    public class SubscriptionMapperProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<CategorySubscription, SubscriptionViewModel>()
                .ForMember(x => x.CategoryId, o => o.MapFrom(m => m.CategoryId))
                .ForMember(x => x.CategoryName, o => o.MapFrom(m => m.Category.Name))
                .ForMember(x => x.UserId, o => o.MapFrom(m => m.UserId))
                .ForMember(x => x.UserName, o => o.MapFrom(m => m.User.UserName));
        }
    }
}