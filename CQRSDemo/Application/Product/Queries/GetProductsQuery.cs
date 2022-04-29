using MediatR;

namespace CQRSDemo.Application.Product.Queries
{
    public class GetProductsQuery : IRequest<List<Models.Product>>
    {
    }
}
