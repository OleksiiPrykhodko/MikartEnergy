using MikartEnergy.Common.DTO.Abstract;
using MikartEnergy.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikartEnergy.Common.DTO.CallbackRequest
{
    public class CallbackRequestDTO : IResponseStatusDTO
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool InWork { get; set; }
        public bool IsDeleted { get; set; }

        public bool Successful { get; set; } = true;
        public KeyValuePair<ResponseError, string>[] Errors { get; set; }

        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public string AuthorPhone { get; set; }
        public string Message { get; set; }
        public string IntrerestedIn { get; set; }
        public int Budget { get; set; }
    }
}
