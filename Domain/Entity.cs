namespace Sattelite.Entities
{
    using System;

    //FullAuditedEntity<long>
    public abstract class Entity
    {
        public virtual int Id { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }

        public long? NumOfView { get; set; } = 0;

        //public virtual bool IsTransient()
        //{
        //    return this.Id == default(int);
        //}
    }

    public abstract class Entity<T>
    {
        public virtual T Id { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }

        public long? NumOfView { get; set; } = 0;
    }
}