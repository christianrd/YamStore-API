using FluentValidation;

namespace API.Core.Application.Product.Commands.Create
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(150);
            RuleFor(x => x.Description).MinimumLength(10).MaximumLength(255);
            RuleFor(x => x.Price).NotEmpty().LessThanOrEqualTo(0);
            RuleFor(x => x.PictureUrl).NotEmpty().MinimumLength(10);
            RuleFor(x => x.Type).NotEmpty().MinimumLength(5);
            RuleFor(x => x.Brand).NotEmpty().MinimumLength(5);
            RuleFor(x => x.QuantityInStock).NotEmpty().LessThan(0);
        }
    }
}