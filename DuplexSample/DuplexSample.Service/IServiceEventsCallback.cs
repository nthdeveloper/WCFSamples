using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DuplexSample.Service
{
    public interface IServiceEventsCallback
    {
        [OperationContract(IsOneWay = true)]
        void OnProductAdded(int id);

        [OperationContract(IsOneWay = true)]
        void OnProductUpdated(int id);

        [OperationContract(IsOneWay = true)]
        void OnProductDeleted(int id);
    }
}
