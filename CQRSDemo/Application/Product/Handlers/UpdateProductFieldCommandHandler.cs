using CQRSDemo.Application.Product.Commands;
using CQRSDemo.Interfaces;
using MediatR;

namespace CQRSDemo.Application.Product.Handlers
{
    public class UpdateProductFieldCommandHandler : IRequestHandler<UpdateProductFieldCommand, bool>
    {
        private readonly IProductContext Context;
        public UpdateProductFieldCommandHandler(IProductContext context)
        {
            this.Context = context;
        }

        public async Task<bool> Handle(UpdateProductFieldCommand request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(Context.Update(request.Id, request.PropertyName, request.Value).Result);
        }
    }
}
