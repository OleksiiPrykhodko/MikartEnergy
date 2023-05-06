using Microsoft.EntityFrameworkCore;
using MikartEnergy.BLL.Mapping;
using MikartEnergy.BLL.Services.Abstract;
using MikartEnergy.Common.DTO.CallbackRequest;
using MikartEnergy.DAL.Context;
using MikartEnergy.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MikartEnergy.BLL.Services
{
    public class CallbackRequestService : BaseService
    {
        public CallbackRequestService(MikartContext context) : base(context)
        {

        }

        public async Task<CallbackRequestDTO> CreateCallbackRequest(NewCallbackRequestDTO dto)
        {
            var callbackRequest = dto.ToCallbackRequest();

            _context.CallbackRequests.Add(callbackRequest);
            await _context.SaveChangesAsync();

            var createdRequest = await _context.CallbackRequests
                                        .FirstAsync(r => r.Id == callbackRequest.Id);

            CallbackRequestDTO responseDTO = createdRequest.ToCallbackRequestDTO();

            return responseDTO;

        }

        public async Task<IEnumerable<CallbackRequestDTO>> GetAllCallbackRequests(bool getDeleted)
        {
            var requests = getDeleted ? await _context.CallbackRequests.ToListAsync()
                                                : await _context.CallbackRequests.Where(r => !r.IsDeleted).ToListAsync();
            return requests.Select(e => e.ToCallbackRequestDTO());
        }

        public async Task<bool> DeleteCallbackRequest(Guid id)
        {
            var request = await _context.CallbackRequests.FirstOrDefaultAsync(r => r.Id == id);

            if (request is not null)
            {
                _context.CallbackRequests.Remove(request);
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateCallbackRequest(CallbackRequestDTO dto)
        {
            if(await _context.CallbackRequests.AnyAsync(c => c.Id == dto.Id))
            {
                var entity = dto.ToCallbackRequest();
                entity.UpdatedAt = DateTime.Now;
                _context.Update(entity);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;

        } 

    }
}
