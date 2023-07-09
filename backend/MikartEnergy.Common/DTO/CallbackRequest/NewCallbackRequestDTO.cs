using MikartEnergy.Common.DTO.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikartEnergy.Common.DTO.CallbackRequest
{
    public class NewCallbackRequestDTO
    {
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public string AuthorEmail { get; set; }
        public string AuthorPhone { get; set; }
        public string Message { get; set; }
        public string IntrerestedIn { get; set; }
        public int Budget { get; set; }
    }
}
