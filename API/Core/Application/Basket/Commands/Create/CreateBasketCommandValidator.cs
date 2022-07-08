using FluentValidation;

namespace API.Core.Application.Basket.Commands.Create
{
    public class CreateBasketCommandValidator : AbstractValidator<CreateBasketCommand>
    {
        public CreateBasketCommandValidator()
        {
            RuleFor(x => x.BuyerId).NotEmpty();
        }
    }
}