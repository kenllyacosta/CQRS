using MediatR;

namespace CQRSDemo.Application.Product.Commands
{
    public class UpdateProductCommand : IRequest<bool>
    {
        public Models.Product Product { get; set; }
        public UpdateProductCommand(Models.Product product)
            => Product = product;
    }
}
