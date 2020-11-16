using System.Linq;
using FluentValidation;
using Store.App.Controllers.Api.Models.Requests;
using Store.DAL;

namespace Store.App.Controllers.Api.Validators
{
    public class AddUpdateCartItemsRequestValidator : AbstractValidator<AddUpdateCartItemsRequest>
    {
        public AddUpdateCartItemsRequestValidator(StoreContext context)
        {
            RuleForEach(r => r.Items).Must(i => i.Amount >= 0)
                .WithMessage("Amount should be positive.");
            RuleForEach(r => r.Items).Must((request, item, ctx) =>
                {
                    ctx.MessageFormatter.AppendArgument("ProductId", item.ProductId);
                    return context.Products.Any(p => p.Id == item.ProductId);
                })
                .WithMessage("Product with id: {ProductId} does not exist.");
        }
    }
}
