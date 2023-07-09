using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikartEnergy.Common.Models.Result
{
    public class ResultModel<T> where T : class
    {
        public ResultModel(T item)
        {
            DTO = item;
        }
        public T DTO { get; }
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
