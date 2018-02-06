using AutoMapper;
using Sattelite.EntityFramework.Profiles;

namespace Sattelite.Web.App_Start
{
    public class MappingConfig
    {
        public static void Configure()
        {
            Mapper.AddProfile(new RoleMapperProfile());
            Mapper.AddProfile(new UserMapperProfile());

            Mapper.AddProfile(new CategoryMapperProfile());
            Mapper.AddProfile(new SubscriptionMapperProfile());

            Mapper.AddProfile(new NewsMapperProfile());
            Mapper.AddProfile(new ProjectMapperProfile());
            Mapper.AddProfile(new ProjectMemberMapperProfile());
        }
    }
}