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
        public bool Successful { get; set; } = true;
        public List<KeyValuePair<ResponseError, string>> Errors { get; } =
            new List<KeyValuePair<ResponseError, string>>();

        public void AddErrorToDTO(ResponseError error, string message)
        {
            Successful = false;
            Errors.Add(new KeyValuePair<ResponseError, string>(error, message));
        }
        public void AddErrorToDTO(params KeyValuePair<ResponseError, string>[] errors)
        {
            Successful = false;
            Errors.AddRange(errors);
        }
        public void AddErrorToDTO(IEnumerable<KeyValuePair<ResponseError, string>> errors)
        {
            Successful = false;
            Errors.AddRange(errors);
        }
    }
}
