namespace Sattelite.EntityFramework.Profiles
{
    using AutoMapper;
    using Sattelite.Entities;
    using Sattelite.EntityFramework.ViewModels.Admin.News;

    public class NewsMapperProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<News, NewsEditingViewModel>()
                .ForMember(x => x.NewsId, o => o.MapFrom(m => m.Id))
                .ForMember(x => x.CategoryId, o => o.MapFrom(m => m.CategoryId))
                .ForMember(x => x.Title, o => o.MapFrom(m => m.NewsContent.Title))
                .ForMember(x => x.ShortDescription, o => o.MapFrom(m => m.NewsContent.ShortDescription))
                .ForMember(x => x.Content, o => o.MapFrom(m => m.NewsContent.Content))
                .ForMember(x => x.SmallImagePath, o => o.MapFrom(m => m.NewsContent.SmallImage))
                .ForMember(x => x.MediumImagePath, o => o.MapFrom(m => m.NewsContent.MediumImage))
                .ForMember(x => x.BigImagePath, o => o.MapFrom(m => m.NewsContent.BigImage));

        //    Mapper.CreateMap<NewsEditingViewModel, News>()
        //        .ForMember(x => x.Id, o => o.MapFrom(m => m.NewsId))
        //        .ForMember(x => x.CategoryId, o => o.MapFrom(m => m.CategoryId))
        //        .ForMember(x => x.NewsContent.Title, o => o.MapFrom(m => m.Title))
        //        .ForMember(x => x.NewsContent.ShortDescription, o => o.MapFrom(m => m.ShortDescription))
        //        .ForMember(x => x.NewsContent.Content, o => o.MapFrom(m => m.Content))
        //        .ForMember(x => x.NewsContent.SmallImage, o => o.MapFrom(m => m.SmallImagePath))
        //        .ForMember(x => x.NewsContent.MediumImage, o => o.MapFrom(m => m.MediumImagePath))
        //        .ForMember(x => x.NewsContent.BigImage, o => o.MapFrom(m => m.BigImagePath));
        }
    }
}