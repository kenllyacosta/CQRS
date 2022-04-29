using CQRSDemo.Application.Product.Commands;
using CQRSDemo.Interfaces;
using MediatR;

namespace CQRSDemo.Application.Product.Handlers
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IProductContext Context;
        public DeleteProductCommandHandler(IProductContext context)
        {
            this.Context = context;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(Context.Delete(request.Id).Result);
        }
    }
}
