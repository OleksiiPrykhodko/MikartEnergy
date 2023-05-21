using MikartEnergy.DAL.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikartEnergy.DAL.Entities
{
    public class CallbackRequest : BaseEntity
    {
        [MaxLength(60)]
        public string AuthorFirstName { get; set; }
        [MaxLength(60)]
        public string AuthorLastName { get; set; }
        [MaxLength(60)]
        public string AuthorEmail { get; set; }
        [MaxLength(16)]
        public string AuthorPhone { get; set; }
        [MaxLength(512)]
        public string Message { get; set; }
        [MaxLength(255)]
        public string IntrerestedIn { get; set; }
        public int Budget { get; set; }
        public bool InWork { get; set; }
    }
}
