using CQRSDemo.Application.Product.Queries;
using CQRSDemo.Interfaces;
using MediatR;

namespace CQRSDemo.Application.Product.Handlers
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<Models.Product>>
    {
        private readonly IProductContext Context;
        public GetProductsQueryHandler(IProductContext context)
        {
            this.Context = context;
        }

        public async Task<List<Models.Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(Context.Retrieve().Result);
        }
    }
}
