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
            if (string.IsNullOrWhiteSpace(dto.AuthorName))
            {
                var request = new CallbackRequestDTO();
                request.AddErrorToDTO(ResponseError.InvalidDtoFieldValue, "Author name can't be empty.");
                return request;
            }
            if (string.IsNullOrWhiteSpace(dto.AuthorEmail))
            {
                var request = new CallbackRequestDTO();
                request.AddErrorToDTO(ResponseError.InvalidDtoFieldValue, "Author email can't be empty.");
                return request;
            }
            if (string.IsNullOrWhiteSpace(dto.Message))
            {
                var request = new CallbackRequestDTO();
                request.AddErrorToDTO(ResponseError.InvalidDtoFieldValue, "Message can't be empty.");
                return request;
            }

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

            dto.AddErrorToDTO(ResponseError.NotFound, "Callback was not found.");
            return dto;
        }

        public async Task<CallbackRequestDTO> CreateBadRequestResponseAsync(IEnumerable<string> messages)
        {
            return await Task.Run<CallbackRequestDTO>(() =>
            {
                var dto = new CallbackRequestDTO();
                dto.AddErrorToDTO(messages.ToList()
                    .Select(m => new KeyValuePair<ResponseError, string>(ResponseError.InvalidModelState, m)));
                return dto;
            });
        }

        public async Task<CallbackRequestDTO> CreateBadRequestResponseAsync()
        {
            return await CreateBadRequestResponseAsync(new string[] { ResponseError.InvalidModelState.ToString() });
        }




    }
}
