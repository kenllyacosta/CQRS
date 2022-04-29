using MediatR;

namespace CQRSDemo.Application.Product.Commands
{
    public class CreateProductCommand : IRequest<int>
    {
        public string? Name { get; set; }
        public decimal UnitPrice { get; set; }
    }
}