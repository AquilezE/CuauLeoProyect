using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Servicio
{
    [ServiceContract (CallbackContract = typeof (IGetTextCallBack))]
    public interface IService1
    {
        [OperationContract (IsOneWay = true)]
        void GetText(string message);
    }
}
