namespace Sattelite.Entities.ProjectAgg
{
    using System;

    public class CategoryFactory
    {
        public static Category GetCategory(int id, string name, string shortDesc, string createdBy)
        {
            return GetCategory(id, name, shortDesc, createdBy, DateTime.Now);
        }

        public static Category GetCategory(int id)
        {
            return GetCategory(id, null, null, null, DateTime.Now);
        }

        public static Category GetCategory(int id, string name, string shortDesc, string createdBy, DateTime createdDate)
        {
            return new Category
            {
                Id = id,
                Name = name,
                Description = shortDesc,
                CreatedBy = createdBy,
                CreatedDate = createdDate
            };
        }
    }
}