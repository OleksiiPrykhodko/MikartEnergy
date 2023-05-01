using System;

namespace MikartEnergy.DAL.Entities.Abstract
{
    public abstract class BaseEntity
    {
        private DateTime _createdAt;

        public BaseEntity()
        {
            CreatedAt = UpdatedAt = DateTime.Now;
        }

        public Guid Id { get; set; }

        public DateTime CreatedAt
        {
            get { return _createdAt; }
            set { _createdAt = (value == DateTime.MinValue) ? DateTime.Now : value; }
        }

        public DateTime UpdatedAt { get; set; }

        public bool Deleted { get; set; }
    }
}
