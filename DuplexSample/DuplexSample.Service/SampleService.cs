using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DuplexSample.Service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Multiple, Namespace = "http://www.acme.com/api")]
    public class SampleService : ISampleService
    {
        bool isAuthenticated;
        Dictionary<int, Product> products;

        IServiceEventsCallback serviceEventsCallback;

        public SampleService()
        {
            products = new Dictionary<int, Product>()
            {
                { 1, new Product(){Id=1, Name="Product 1" }},
                { 2, new Product(){Id=2, Name="Product 2" }},
                { 3, new Product(){Id=3, Name="Product 3" }},
                { 4, new Product(){Id=4, Name="Product 4" }}
            };
        }

        public OperationResult<bool> Login(string userName, string password)
        {
            if (userName == "admin" && password == "admin")
            {
                isAuthenticated = true;
                serviceEventsCallback = OperationContext.Current.GetCallbackChannel<IServiceEventsCallback>();

                return OperationResult.Success<bool>(true);
            }

            return OperationResult.Fail<bool>("Invalid user name or password");
        }

        public void Logout()
        {
            isAuthenticated = false;
            serviceEventsCallback = null;
        }

        public OperationResult<Product[]> GetAllProducts()
        {
            if (!isAuthenticated)
                return OperationResult.Fail<Product[]>("Not authenticated!");

            return OperationResult.Success<Product[]>(products.Values.ToArray());
        }

        public OperationResult<Product> GetProduct(int id)
        {
            if (!isAuthenticated)
                return OperationResult.Fail<Product>("Not authenticated!");

            if (!products.ContainsKey(id))
                OperationResult.Fail<Product>($"Product with Id {id} does not exist.");

            return OperationResult.Success<Product>(products[id]);
        }

        public OperationResult<Product> AddProduct(Product product)
        {
            if (!isAuthenticated)
                return OperationResult.Fail<Product>("Not authenticated!");

            if (products.ContainsKey(product.Id))
                OperationResult.Fail<Product>($"Product with Id {product.Id} already exist.");

            products.Add(product.Id, product);

            Task.Run(() => serviceEventsCallback.OnProductAdded(product.Id));

            return OperationResult.Success<Product>(product);
        }

        public OperationResult<Product> UpdateProduct(Product product)
        {
            if (!isAuthenticated)
                return OperationResult.Fail<Product>("Not authenticated!");

            if (!products.ContainsKey(product.Id))
                OperationResult.Fail<Product>($"Product with Id {product.Id} does not exist.");

            products[product.Id] = product;

            return OperationResult.Success<Product>(product);
        }

        public OperationResult<bool> RemoveProduct(int id)
        {
            if (!isAuthenticated)
                return OperationResult.Fail<bool>("Not authenticated!");

            if (!products.ContainsKey(id))
                OperationResult.Fail<bool>($"Product with Id {id} does not exist.");

            products.Remove(id);

            return OperationResult.Success<bool>(true);
        }
    }
}
