using FluentValidation;
using MikartEnergy.Common.DTO.Pagination;
using MikartEnergy.Common.QueryParams.Pagination;

namespace MikartEnergy.WebAPI.Validators
{
    public sealed class PaginationRequestQueryParamsValidator: AbstractValidator<PaginationQueryParams>
    {
        private readonly int _pageMinSize = 1;
        private readonly int _pageMaxSize = 50;

        public PaginationRequestQueryParamsValidator() 
        {
            RuleFor(paginationRequest => paginationRequest.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage($"{nameof(PaginationQueryParams.PageNumber)} must be greater or equal to 1.");

            RuleFor(paginationRequest => paginationRequest.PageSize)
                .GreaterThanOrEqualTo(_pageMinSize).WithMessage($"{nameof(PaginationQueryParams.PageSize)} must be greater or equal to {_pageMinSize}.")
                .LessThanOrEqualTo(_pageMaxSize).WithMessage($"{nameof(PaginationQueryParams.PageSize)} must be less or equal to {_pageMaxSize}.");
        }
    }
}
