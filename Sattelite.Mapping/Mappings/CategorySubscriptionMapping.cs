namespace Sattelite.Mapping
{
    using Sattelite.Entities;

    public class CategorySubscriptionMapping : EntityMappingBase<CategorySubscription>
    {
        public CategorySubscriptionMapping()
        {
            this.Property(x => x.CategoryId).IsRequired();
            this.Property(x => x.UserId).IsOptional(); //NOTE: if UserId = NULL, that mean "subscription is for all USERs"
            //this.Property(x => x.UserName).IsOptional();
            this.ToTable("CategorySubscription");
        }
    }
}