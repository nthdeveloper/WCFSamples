using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Web;

namespace WCFSample.WebAPI
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, IncludeExceptionDetailInFaults = true)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class SampleAPI : ISampleAPI
    {
        Dictionary<int, Product> products;

        public SampleAPI()
        {
            products = new Dictionary<int, Product>()
            {
                { 1, new Product(){Id=1, Name="Product 1" }},
                { 2, new Product(){Id=2, Name="Product 2" }},
                { 3, new Product(){Id=3, Name="Product 3" }},
                { 4, new Product(){Id=4, Name="Product 4" }}
            };
        }

        // To use HTTP GET, add [WebGet] attribute. (Default ResponseFormat is WebMessageFormat.Json)
        // To create an operation that returns XML,
        //     add [WebGet(ResponseFormat=WebMessageFormat.Xml)],
        //     and include the following line in the operation body:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
        public string Ping()
        {
            return "Hello!";
        }

        public Product[] GetAllProducts()
        {
            return products.Values.ToArray();
        }

        public Product GetProduct(int id)
        {
            if (!products.ContainsKey(id))
                throw new ArgumentException($"Product with Id {id} does not exist.");

            return products[id];
        }

        public Product AddProduct(Product product)
        {
            if (products.ContainsKey(product.Id))
                throw new ArgumentException($"Product with Id {product.Id} already exist.");

            products.Add(product.Id, product);

            return product;
        }

        public Product UpdateProduct(Product product)
        {
            if (!products.ContainsKey(product.Id))
                throw new ArgumentException($"Product with Id {product.Id} does not exist.");

            products[product.Id] = product;

            return product;
        }

        public bool RemoveProduct(int id)
        {
            if (!products.ContainsKey(id))
                throw new ArgumentException($"Product with Id {id} does not exist.");

            products.Remove(id);

            return true;
        }
    }
}