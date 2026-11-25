using FluentValidation;
using MediatR;

namespace DonationHub.Application.Behaviors
{
    public sealed class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> _validators)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
        {
            if (!_validators.Any())
                return await next();
            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(_validators.Select(c => c.ValidateAsync(context, ct)));
            var failures = validationResults.SelectMany(e => e.Errors).Where(e => e is not null)
                .ToList();
            if (failures is null || failures.Count == 0)
                return await next();
            throw new ValidationException(failures);
        }
    }

}
