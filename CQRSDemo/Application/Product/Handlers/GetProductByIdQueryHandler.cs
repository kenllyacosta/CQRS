using CQRSDemo.Application.Product.Queries;
using CQRSDemo.Interfaces;
using MediatR;

namespace CQRSDemo.Application.Product.Handlers
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Models.Product>
    {
        private readonly IProductContext Context;
        public GetProductByIdQueryHandler(IProductContext context)
        {
            this.Context = context;
        }

        public async Task<Models.Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(Context.Retrieve(request.Id).Result);
        }
    }
}
