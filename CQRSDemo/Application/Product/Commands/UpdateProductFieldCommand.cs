using MediatR;

namespace CQRSDemo.Application.Product.Commands
{
    public class UpdateProductFieldCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string PropertyName { get; set; }
        public object Value { get; set; }
        public UpdateProductFieldCommand(int id, string propertyName, object value)
        {
            this.Id = id;
            this.PropertyName = propertyName;
            this.Value = value;
        }
    }
}
