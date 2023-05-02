using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MikartEnergy.DAL.Entities.Abstract
{
    public abstract class BaseEntity
    {
        private DateTime _createdAt;

        public BaseEntity()
        {
            CreatedAt = UpdatedAt = DateTime.Now;
        }

        [Key]
        // TODO: If Id well be generate in ctor, then change to [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        public DateTime CreatedAt
        {
            get { return _createdAt; }
            set { _createdAt = (value == DateTime.MinValue) ? DateTime.Now : value; }
        }

        public DateTime UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }
    }
}
