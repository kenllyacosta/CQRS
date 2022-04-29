using CQRSDemo.Application.Product.Commands;
using CQRSDemo.Interfaces;
using MediatR;

namespace CQRSDemo.Application.Product.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IProductContext Context;
        public CreateProductCommandHandler(IProductContext context)
        {
            this.Context = context;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(Context.Create(
                new Models.Product() 
                { 
                    Name = request.Name,
                    UnitPrice = request.UnitPrice,
                    Discontinued = false, 
                    Quantity = 0
                }).Result);
        }
    }
}