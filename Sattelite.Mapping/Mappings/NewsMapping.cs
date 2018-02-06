namespace Sattelite.Mapping
{
    using Sattelite.Entities;

    public class NewsMapping : EntityMappingBase<News>
    {
        public NewsMapping()
        {
            this.Property(x => x.NewsContentId);

            this.HasRequired(n => n.Category)
                .WithMany(c => c.News).HasForeignKey(c => c.CategoryId);

            //this.HasRequired(x => x.NewsContent)
            //    .WithRequiredDependent().Map(key => key.MapKey("NewsContentId"));

            this.ToTable("News");
        }
    }
}