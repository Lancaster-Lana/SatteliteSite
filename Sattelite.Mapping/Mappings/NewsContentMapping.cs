namespace Sattelite.Mapping
{
    using Sattelite.Entities;

    public class NewsContentMapping : EntityMappingBase<NewsContent>
    {
        public NewsContentMapping()
        {
            this.Property(x => x.Title).IsRequired();
            this.Property(x => x.ShortDescription).IsRequired();
            this.Property(x => x.Content); //Not required (can be added later), just Title
            this.Property(x => x.SmallImage);
            this.Property(x => x.MediumImage);
            this.Property(x => x.BigImage);
            this.Property(x => x.NumOfView);

            //this.HasMany(x => x.RelatedNews).WithRequired(y => y.NewsContent);

            this.ToTable("NewsContent");
        }
    }
}