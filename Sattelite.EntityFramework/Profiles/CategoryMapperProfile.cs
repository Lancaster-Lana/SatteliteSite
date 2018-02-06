namespace Sattelite.EntityFramework.Profiles
{
    using AutoMapper;
    using Sattelite.Entities;
    using Sattelite.EntityFramework.ViewModels.Admin.Category;

    public class CategoryMapperProfile : Profile 
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Category, CategoryEditingViewModel>()
                .ForMember(x => x.CategoryId, o => o.MapFrom(m => m.Id))
                .ForMember(x => x.Name, o => o.MapFrom(m => m.Name))
                .ForMember(x => x.Description, o => o.MapFrom(m => m.Description));
        }
    }
}