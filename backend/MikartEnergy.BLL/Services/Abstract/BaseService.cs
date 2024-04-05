using MikartEnergy.Common.DTO.Pagination;
using MikartEnergy.Common.Models.Result;
using MikartEnergy.DAL.Context;

namespace MikartEnergy.BLL.Services.Abstract
{
    public abstract class BaseService
    {
        public async Task<ResultModel<T>> CreateBadRequestResultAsync<T>(T dto, IEnumerable<KeyValuePair<string, string>> messages) where T : class
        {
            return await Task.Run<ResultModel<T>>(() =>
            {
                var result = new ResultModel<T>(dto);
                result.AddErrorToDTO(messages);
                return result;
            });
        }

        public int GetSkipAmount(PaginationRequestDTO request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request), "PaginationRequestDTO can't be null.");
            }

            return (request.PageNumber - 1) * request.PageSize;
        }
    }
}
