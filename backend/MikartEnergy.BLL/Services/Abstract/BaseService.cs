using MikartEnergy.Common.DTO.Abstract;
using MikartEnergy.Common.DTO.CallbackRequest;
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

        public async Task<T> CreateBadRequestResponseAsync<T>(T dto, IEnumerable<KeyValuePair<string, string>> messages) where T : IResponseStatusDTO
        {
            return await Task.Run<T>(() =>
            {
                dto.AddErrorToDTO(messages);
                return dto;
            });
        }
    }
}
