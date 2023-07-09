using FluentValidation;
using MikartEnergy.Common.DTO.Pagination;

namespace MikartEnergy.WebAPI.Validators
{
    public sealed class PaginationRequestDTOValidator: AbstractValidator<PaginationRequestDTO>
    {
        private readonly int _pageMinSize = 1;
        private readonly int _pageMaxSize = 50;

        public PaginationRequestDTOValidator() 
        {
            RuleFor(paginationRequest => paginationRequest.PageIndex)
                .GreaterThanOrEqualTo(1).WithMessage($"{nameof(PaginationRequestDTO.PageIndex)} must be greater or equal to 1.");

            RuleFor(paginationRequest => paginationRequest.PageSize)
                .GreaterThanOrEqualTo(_pageMinSize).WithMessage($"{nameof(PaginationRequestDTO.PageSize)} must be greater or equal to {_pageMinSize}.")
                .LessThanOrEqualTo(_pageMaxSize).WithMessage($"{nameof(PaginationRequestDTO.PageSize)} must be less or equal to {_pageMaxSize}.");
        }
    }
}
