using CQRSDemo.Interfaces;
using CQRSDemo.Models;
using System.Data;
using System.Data.SqlClient;

namespace CQRSDemo.Services
{
    public class ProductContext : IProductContext
    {
        //Instalar System.Data.SqlClient
        private readonly string CnnString;        
        public ProductContext(string cnnString)
        {
            this.CnnString = cnnString;
        }

        public ValueTask<int> Create(Product product)
        {
            using (SqlConnection connection = new SqlConnection(CnnString))
            {
                connection.Open();
                SqlCommand command =
                    new($"Insert Into Product (Name, UnitPrice, Discontinued, Quantity) Values (@Name, @UnitPrice, @Discontinued, @Quantity); Select @@Identity", connection);
                command.Parameters.Add(new SqlParameter("@Name", product.Name));
                command.Parameters.Add(new SqlParameter("@UnitPrice", product.UnitPrice));
                command.Parameters.Add(new SqlParameter("@Discontinued", product.Discontinued));
                command.Parameters.Add(new SqlParameter("@Quantity", product.Quantity));

                var exec = command.ExecuteScalar();
                Result = exec == null ? 0 : Convert.ToInt32(exec);
            }
        }

        public ValueTask<List<Product>> Retrieve()
        {
            List<Product> Result = new List<Product>();
            using (SqlConnection connection = new SqlConnection(CnnString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("Select Id, Name, UnitPrice, Discontinued, Quantity From Product", connection);
                var datos = command.ExecuteReader();
                while (datos.Read())
                    Result.Add(new Product()
                    {
                        Id = datos.GetInt32("Id"),
                        Discontinued = datos.GetBoolean("Discontinued"),
                        Name = datos.GetString("Name"),
                        Quantity = datos.GetDecimal("Quantity"),
                        UnitPrice = datos.GetDecimal("UnitPrice")
                    });

                return ValueTask.FromResult(Result);
            }
        }

        public ValueTask<Product> Retrieve(int id)
        {
            Product? Result = default;
            using (SqlConnection connection = new SqlConnection(CnnString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("Select Id, Name, UnitPrice, Discontinued, Quantity From Product Where Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                var datos = command.ExecuteReader();
                while (datos.Read())
                    Result = new Product()
                    {
                        Id = datos.GetInt32("Id"),
                        Discontinued = datos.GetBoolean("Discontinued"),
                        Name = datos.GetString("Name"),
                        Quantity = datos.GetDecimal("Quantity"),
                        UnitPrice = datos.GetDecimal("UnitPrice")
                    };

                return ValueTask.FromResult(Result!);
            }
        }

        public ValueTask<bool> Update(Product product)
        {
            using (SqlConnection connection = new SqlConnection(CnnString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("Update Product Set Name = @Name, UnitPrice = @UnitPrice, Discontinued = @Discontinued, Quantity = @Quantity Where Id = @Id", connection);
                command.Parameters.Add(new SqlParameter("@Name", product.Name));
                command.Parameters.Add(new SqlParameter("@UnitPrice", product.UnitPrice));
                command.Parameters.Add(new SqlParameter("@Discontinued", product.Discontinued));
                command.Parameters.Add(new SqlParameter("@Quantity", product.Quantity));
                command.Parameters.Add(new SqlParameter("@Id", product.Id));

                return ValueTask.FromResult(command.ExecuteNonQuery() != 0);
            }
        }

        public ValueTask<bool> Update(int id, string propertyName, object value)
        {
            using (SqlConnection connection = new SqlConnection(CnnString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"Update Product Set {propertyName} = '{value}' Where Id = @Id", connection);
                command.Parameters.Add(new SqlParameter("@Id", id));

                return ValueTask.FromResult(command.ExecuteNonQuery() != 0);
            }
        }

        public ValueTask<bool> Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(CnnString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("Delete From Product Where Id = @Id", connection);
                command.Parameters.Add(new SqlParameter("@Id", id));

                return ValueTask.FromResult(command.ExecuteNonQuery() != 0);
            }
        }
    }
}