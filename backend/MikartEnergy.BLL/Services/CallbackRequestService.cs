using Microsoft.EntityFrameworkCore;
using MikartEnergy.BLL.Mapping;
using MikartEnergy.BLL.Services.Abstract;
using MikartEnergy.Common.DTO.CallbackRequest;
using MikartEnergy.Common.Enums;
using MikartEnergy.Common.Models.Result;
using MikartEnergy.DAL.Context;
using MikartEnergy.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MikartEnergy.BLL.Services
{
    public class CallbackRequestService : BaseService
    {
        public CallbackRequestService(MikartContext context) : base(context)
        {

        }

        public async Task<ResultModel<CallbackRequestDTO>> CreateCallbackRequestAsync(NewCallbackRequestDTO dto)
        {
            var callbackRequest = dto.ToCallbackRequest();

            _context.CallbackRequests.Add(callbackRequest);
            await _context.SaveChangesAsync();

            var createdRequest = await _context.CallbackRequests
                                        .FirstAsync(r => r.Id == callbackRequest.Id);
            var responseDTO = createdRequest.ToCallbackRequestDTO();
            return new ResultModel<CallbackRequestDTO>(responseDTO);
        }

        public async Task<ResultModel<IEnumerable<CallbackRequestDTO>>> GetAllCallbackRequestsAsync(bool getDeleted)
        {
            IEnumerable<CallbackRequestDTO> callbackRequests;

            if (getDeleted)
            {
                callbackRequests = await Task.Run(
                    () => _context.CallbackRequests.Select(e => e.ToCallbackRequestDTO()));
            }
            else
            {
                callbackRequests = await Task.Run(
                    () => _context.CallbackRequests.Where(r => !r.IsDeleted).Select(e => e.ToCallbackRequestDTO()));
            }

            return new ResultModel<IEnumerable<CallbackRequestDTO>>(callbackRequests);
        }

        public async Task<bool> DeleteCallbackRequestAsync(Guid id)
        {
            var entity = await _context.CallbackRequests.FirstOrDefaultAsync(r => r.Id == id);

            if (entity is not null && !entity.IsDeleted)
            {
                _context.CallbackRequests.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<ResultModel<CallbackRequestDTO>> UpdateCallbackRequestAsync(CallbackRequestDTO dto)
        {
            if (await _context.CallbackRequests.AnyAsync(c => c.Id == dto.Id))
            {
                var entity = dto.ToCallbackRequest();
                entity.UpdatedAt = DateTime.Now;
                _context.Update(entity);
                await _context.SaveChangesAsync();
                return new ResultModel<CallbackRequestDTO>(entity.ToCallbackRequestDTO());
            }

            var result = new ResultModel<CallbackRequestDTO>(dto);
            result.AddErrorToDTO(ResponseError.NotFound.ToString(), "Callback request was not found.");
            return result;
        }

    }
}
