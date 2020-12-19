using FluentValidation;

namespace StockAPI.Models.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator() {
            RuleFor(product => product.Amount).GreaterThanOrEqualTo(0).WithMessage("A quantidade do produto não pode ser menor que ZERO.");
            RuleFor(product => product.Price).GreaterThanOrEqualTo(0).WithMessage("O preço do produto não pode ser menor que ZERO.");
        }
    }
}
