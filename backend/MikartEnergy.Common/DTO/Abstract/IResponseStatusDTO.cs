using MikartEnergy.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikartEnergy.Common.DTO.Abstract
{
    public interface IResponseStatusDTO
    {
        bool Successful { get; set; }
        KeyValuePair<ResponseErrors, string>[] Errors { get; set; }
    }
}
