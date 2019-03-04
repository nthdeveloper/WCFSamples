using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DuplexSample.Service
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IServiceEventsCallback), Namespace = "http://www.acme.com/api")]
    public interface ISampleService
    {
        [OperationContract(IsInitiating = true, IsTerminating = false)]
        OperationResult<bool> Login(string userName, string password);

        [OperationContract(IsInitiating = false, IsTerminating = true)]
        void Logout();

        [OperationContract]
        OperationResult<Product[]> GetAllProducts();

        [OperationContract]
        OperationResult<Product> GetProduct(int id);

        [OperationContract]
        OperationResult<Product> AddProduct(Product product);

        [OperationContract]
        OperationResult<Product> UpdateProduct(Product product);

        [OperationContract]
        OperationResult<bool> RemoveProduct(int id);
    }

    [DataContract]
    public class OperationResult<T>
    {
        [DataMember]
        public bool IsSuccess { get; set; }
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public T Value { get; set; }
    }

    [DataContract]
    public class Product
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }
    }

    public static class OperationResult
    {
        public static OperationResult<T> Success<T>(T resultValue)
        {
            return new OperationResult<T> { IsSuccess = true, Value = resultValue };
        }

        public static OperationResult<T> Fail<T>(string msg)
        {
            return new OperationResult<T> { IsSuccess = false, Message = msg, Value = default(T) };
        }
    }
}
