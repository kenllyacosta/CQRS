using CQRSDemo.Models;
using CQRSDemo.Services;

namespace CQRSDemo.Interfaces
{
    public interface IProductContext
    {
        ValueTask<int> Create(Product product);
        ValueTask<List<Product>> Retrieve();
        ValueTask<Product> Retrieve(int id);
        ValueTask<bool> Update(Product product);
        ValueTask<bool> Update(int id, string propertyName, object value);
        ValueTask<bool> Delete(int id);
    }
}