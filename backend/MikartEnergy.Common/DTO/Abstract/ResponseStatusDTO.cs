using MikartEnergy.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MikartEnergy.Common.DTO.Abstract
{
    public abstract class ResponseStatusDTO: IResponseStatusDTO
    {
        public bool Successful { get; private set; } = true;
        public List<KeyValuePair<string, string>> Errors { get; } =
            new List<KeyValuePair<string, string>>();

        public void AddErrorToDTO(string error, string message)
        {
            Successful = false;
            Errors.Add(new KeyValuePair<string, string>(error, message));
        }

        public void AddErrorToDTO(IEnumerable<KeyValuePair<string, string>> errors)
        {
            Successful = false;
            Errors.AddRange(errors);
        }
    }
}
