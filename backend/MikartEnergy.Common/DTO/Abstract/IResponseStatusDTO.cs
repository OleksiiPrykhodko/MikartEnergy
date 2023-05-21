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
        bool Successful { get; }
        List<KeyValuePair<string, string>> Errors { get; }

        void AddErrorToDTO(string error, string message);
        void AddErrorToDTO(IEnumerable<KeyValuePair<string, string>> errors);
    }
}
