using FluentValidation;
using MikartEnergy.Common.DTO.CallbackRequest;
using MikartEnergy.DAL.Entities;

namespace MikartEnergy.WebAPI.Validators
{
    public sealed class NewCallbackRequestDTOValidator : AbstractValidator<NewCallbackRequestDTO>
    {
        public NewCallbackRequestDTOValidator()
        {
            RuleFor(callbackRequest => callbackRequest.AuthorName)
                .NotNull().WithMessage($"{nameof(NewCallbackRequestDTO.AuthorName)} can't be NULL.")
                .NotEmpty().WithMessage($"{nameof(NewCallbackRequestDTO.AuthorName)} can't be empty.")
                .MaximumLength(100).WithMessage($"{nameof(NewCallbackRequestDTO.AuthorName)} can't be longer than 100 chars.");

            RuleFor(callbackRequest => callbackRequest.AuthorEmail)
                .NotNull().WithMessage($"{nameof(NewCallbackRequestDTO.AuthorEmail)} can't be NULL.")
                .EmailAddress().WithMessage($"{nameof(NewCallbackRequestDTO.AuthorEmail)} must have email format.")
                .MaximumLength(60).WithMessage($"{nameof(NewCallbackRequestDTO.AuthorEmail)} can't longer than 60 chars.");

            RuleFor(callbackRequest => callbackRequest.AuthorPhone)
                .NotNull().WithMessage($"{nameof(NewCallbackRequestDTO.AuthorPhone)} can't be NULL.")
                .NotEmpty().WithMessage($"{nameof(NewCallbackRequestDTO.AuthorPhone)} can't be empty.")
                .MinimumLength(10).WithMessage($"{nameof(NewCallbackRequestDTO.AuthorPhone)} can't be shorter than 10 chars.")
                .MaximumLength(26).WithMessage($"{nameof(NewCallbackRequestDTO.AuthorPhone)} can't be longer than 10 chars.");

            RuleFor(callbackRequest => callbackRequest.Message)
                .NotNull().WithMessage($"{nameof(NewCallbackRequestDTO.Message)} can't be NULL.")
                .NotEmpty().WithMessage($"{nameof(NewCallbackRequestDTO.Message)} can't be empty.")
                .MinimumLength(10).WithMessage($"{nameof(NewCallbackRequestDTO.Message)} can't be shorter than 10 chars.")
                .MaximumLength(255).WithMessage($"{nameof(NewCallbackRequestDTO.Message)} can't be longer than 255 chars.");       
            
            RuleFor(callbackRequest => callbackRequest.IntrerestedIn)
                .NotNull().WithMessage($"{nameof(NewCallbackRequestDTO.IntrerestedIn)} can't be NULL.")
                .NotEmpty().WithMessage($"{nameof(NewCallbackRequestDTO.IntrerestedIn)} can't be empty.")
                .MaximumLength(255).WithMessage($"{nameof(NewCallbackRequestDTO.IntrerestedIn)} can't be longer than 255 chars.");

            RuleFor(callbackRequest => callbackRequest.Budget)
                .GreaterThan(0).WithMessage($"{nameof(NewCallbackRequestDTO.Budget)} must be greater than 0.")
                .Must(value => value % 1000 == 0).WithMessage($"{nameof(NewCallbackRequestDTO.Budget)} must be equivalent to 1000.");
        }
    }
}
