using Microsoft.EntityFrameworkCore;
using MikartEnergy.BLL.Mapping;
using MikartEnergy.BLL.Services.Abstract;
using MikartEnergy.Common.DTO.CallbackRequest;
using MikartEnergy.Common.Enums;
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

        public async Task<CallbackRequestDTO> CreateCallbackRequestAsync(NewCallbackRequestDTO dto)
        {

            var callbackRequest = dto.ToCallbackRequest();

            _context.CallbackRequests.Add(callbackRequest);
            await _context.SaveChangesAsync();

            var createdRequest = await _context.CallbackRequests
                                        .FirstAsync(r => r.Id == callbackRequest.Id);

            CallbackRequestDTO responseDTO = createdRequest.ToCallbackRequestDTO();

            return responseDTO;

        }

        public async Task<IEnumerable<CallbackRequestDTO>> GetAllCallbackRequestsAsync(bool getDeleted)
        {
            var requests = getDeleted ? await _context.CallbackRequests.ToListAsync()
                                                : await _context.CallbackRequests.Where(r => !r.IsDeleted).ToListAsync();
            return requests.Select(e => e.ToCallbackRequestDTO());
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

        public async Task<CallbackRequestDTO> UpdateCallbackRequestAsync(CallbackRequestDTO dto)
        {
            if (await _context.CallbackRequests.AnyAsync(c => c.Id == dto.Id))
            {
                var entity = dto.ToCallbackRequest();
                entity.UpdatedAt = DateTime.Now;
                _context.Update(entity);
                await _context.SaveChangesAsync();
                return entity.ToCallbackRequestDTO();
            }

            dto.AddErrorToDTO(ResponseError.NotFound.ToString(), "Callback request was not found.");
            return dto;
        }

        // TODO: Move this method to base class and do it generic.

        public async Task<CallbackRequestDTO> CreateBadRequestResponseAsync(CallbackRequestDTO dto, IEnumerable<KeyValuePair<string, string>> messages)
        {
            return await Task.Run<CallbackRequestDTO>(() =>
            {
                dto.AddErrorToDTO(messages);
                return dto;
            });
        }
        
    }
}
