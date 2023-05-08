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
        List<KeyValuePair<ResponseError, string>> Errors { get; }

        void AddErrorToDTO(ResponseError error, string message);
        void AddErrorToDTO(params KeyValuePair<ResponseError, string>[] errors);
    }
}
