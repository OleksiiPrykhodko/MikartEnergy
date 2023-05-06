using Microsoft.EntityFrameworkCore;
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

        public async Task<CallbackRequestDTO> CreateCallbackRequest(NewCallbackRequestDTO request)
        {
            CallbackRequest callbackRequest = new CallbackRequest
            {
                AuthorName = request.AuthorName,
                AuthorEmail = request.AuthorEmail,
                AuthorPhone = request.AuthorPhone,
                IntrerestedIn = request.IntrerestedIn,
                Message = request.Message,
                Budget = request.Budget,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            _context.CallbackRequests.Add(callbackRequest);
            await _context.SaveChangesAsync();

            var createdRequest = await _context.CallbackRequests
                                        .FirstAsync(r => r.Id == callbackRequest.Id);

            CallbackRequestDTO responseDTO = CallbackRequestToDTO(createdRequest);

            return responseDTO;

        }

        public async Task<IEnumerable<CallbackRequestDTO>> GetAllCallbackRequests(bool getDeleted)
        {
            var requests = getDeleted ? await _context.CallbackRequests.ToListAsync()
                                                : await _context.CallbackRequests.Where(r => !r.IsDeleted).ToListAsync();
            return requests.Select(e => CallbackRequestToDTO(e));
        }

        public async Task<bool> DeleteCallbackRequests(Guid id)
        {
            var request = await _context.CallbackRequests.FirstOrDefaultAsync(r => r.Id == id);

            if (request is not null)
            {
                _context.CallbackRequests.Remove(request);
                return true;
            }

            return false;
        }

        private CallbackRequestDTO CallbackRequestToDTO(CallbackRequest entity)
        {
            if (entity is null)
            {
                throw new NullReferenceException($"Null argument of {nameof(CallbackRequest)}");
            }

            CallbackRequestDTO dto = new CallbackRequestDTO
            {
                Id = entity.Id,
                IsDeleted = entity.IsDeleted,
                InWork = entity.InWork,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                AuthorName = entity.AuthorName,
                AuthorEmail = entity.AuthorEmail,
                AuthorPhone = entity.AuthorPhone,
                IntrerestedIn = entity.IntrerestedIn,
                Message = entity.Message,
                Budget = entity.Budget
            };

            return dto;
        }
    }
}
