using FluentValidation;
using MikartEnergy.Common.DTO.CallbackRequest;

namespace MikartEnergy.WebAPI.Validators
{
    public sealed class CallbackRequestDTOValidator : AbstractValidator<CallbackRequestDTO>
    {
        public CallbackRequestDTOValidator()
        {
            RuleFor(callbackRequest => callbackRequest.Id)
                .NotNull().WithMessage($"{nameof(CallbackRequestDTO.Id)} can't be NULL.")
                .NotEqual(Guid.Empty);

            RuleFor(callbackRequest => callbackRequest.CreatedAt)
                .NotNull().WithMessage($"{nameof(CallbackRequestDTO.CreatedAt)} can't be NULL.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage($"{nameof(CallbackRequestDTO.CreatedAt)} can't be now or later.");

            RuleFor(callbackRequest => callbackRequest.UpdatedAt)
                .NotNull().WithMessage($"{nameof(CallbackRequestDTO.UpdatedAt)} can't be NULL.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage($"{nameof(CallbackRequestDTO.UpdatedAt)} can't be now or later.");

            RuleFor(callbackRequest => callbackRequest.AuthorName)
                .NotNull().WithMessage($"{nameof(CallbackRequestDTO.AuthorName)} can't be NULL.")
                .NotEmpty().WithMessage($"{nameof(CallbackRequestDTO.AuthorName)} can't be empty.")
                .MinimumLength(2).WithMessage($"{nameof(CallbackRequestDTO.AuthorName)} can't be shorter than 2 chars.")
                .MaximumLength(100).WithMessage($"{nameof(CallbackRequestDTO.AuthorName)} can't be longer than 100 chars.");

            RuleFor(callbackRequest => callbackRequest.AuthorEmail)
                .NotNull().WithMessage($"{nameof(CallbackRequestDTO.AuthorEmail)} can't be NULL.")
                .EmailAddress().WithMessage($"{nameof(CallbackRequestDTO.AuthorEmail)} must have email format.")
                .MaximumLength(60).WithMessage($"{nameof(CallbackRequestDTO.AuthorEmail)} can't longer than 60 chars.");

            RuleFor(callbackRequest => callbackRequest.AuthorPhone)
                .NotNull().WithMessage($"{nameof(CallbackRequestDTO.AuthorPhone)} can't be NULL.")
                .NotEmpty().WithMessage($"{nameof(CallbackRequestDTO.AuthorPhone)} can't be empty.")
                .MinimumLength(10).WithMessage($"{nameof(CallbackRequestDTO.AuthorPhone)} can't be shorter than 10 chars.")
                .MaximumLength(16).WithMessage($"{nameof(CallbackRequestDTO.AuthorPhone)} can't be longer than 16 chars.");

            RuleFor(callbackRequest => callbackRequest.Message)
                .NotNull().WithMessage($"{nameof(CallbackRequestDTO.Message)} can't be NULL.")
                .NotEmpty().WithMessage($"{nameof(CallbackRequestDTO.Message)} can't be empty.")
                .MinimumLength(10).WithMessage($"{nameof(CallbackRequestDTO.Message)} can't be shorter than 10 chars.")
                .MaximumLength(500).WithMessage($"{nameof(CallbackRequestDTO.Message)} can't be longer than 500 chars.");

            RuleFor(callbackRequest => callbackRequest.IntrerestedIn)
                .NotNull().WithMessage($"{nameof(CallbackRequestDTO.IntrerestedIn)} can't be NULL.")
                .NotEmpty().WithMessage($"{nameof(CallbackRequestDTO.IntrerestedIn)} can't be empty.")
                .MaximumLength(250).WithMessage($"{nameof(CallbackRequestDTO.IntrerestedIn)} can't be longer than 250 chars.");

            RuleFor(callbackRequest => callbackRequest.Budget)
                .GreaterThan(0).WithMessage($"{nameof(CallbackRequestDTO.Budget)} must be greater than 0.")
                .Must(value => value % 1000 == 0).WithMessage($"{nameof(CallbackRequestDTO.Budget)} must be equivalent to 1000.");
        }
    }
}
