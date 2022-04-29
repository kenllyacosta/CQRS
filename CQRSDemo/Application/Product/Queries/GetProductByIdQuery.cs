using MediatR;

namespace CQRSDemo.Application.Product.Queries
{
    public class GetProductByIdQuery : IRequest<Models.Product>
    {
        protected internal int Id { get; set; }
        public GetProductByIdQuery(int id)
        {
            this.Id = id;
        }
    }
}
