using CQRSDemo.Application.Product.Commands;
using CQRSDemo.Interfaces;
using MediatR;

namespace CQRSDemo.Application.Product.Handlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IProductContext Context;
        public UpdateProductCommandHandler(IProductContext context)
        {
            this.Context = context;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(Context.Update(request.Product).Result);
        }
    }
}
