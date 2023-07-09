using MikartEnergy.Common.DTO.Abstract;
using MikartEnergy.Common.DTO.CallbackRequest;
using MikartEnergy.Common.Models.Result;
using MikartEnergy.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikartEnergy.BLL.Services.Abstract
{
    public abstract class BaseService
    {
        private protected readonly MikartContext _context;

        public BaseService(MikartContext context)
        {
            _context = context;
        }

        public async Task<ResultModel<T>> CreateBadRequestResultAsync<T>(T dto, IEnumerable<KeyValuePair<string, string>> messages) where T : class
        {
            return await Task.Run<ResultModel<T>>(() =>
            {
                var result = new ResultModel<T>(dto);
                result.AddErrorToDTO(messages);
                return result;
            });
        }
    }
}
