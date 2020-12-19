using FluentValidation;

namespace SalesAPI.Models.Validators
{
    public class SaleValidator : AbstractValidator<Sale>
    {
        public SaleValidator() {
            RuleFor(sale => sale.Total).GreaterThan(0).WithMessage("O total da compra precisa ser maior que ZERO.");
            RuleForEach(sale => sale.Products).SetValidator(new ProductValidator());
        }
    }

    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(product => product.Amount).GreaterThan(0).WithMessage("A quantidade de produto precisa ser maior que ZERO");
        }
    }
}
