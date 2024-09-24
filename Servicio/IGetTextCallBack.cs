using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Servicio
{
    internal interface IGetTextCallBack
    {
        [OperationContract (IsOneWay = true)]
        void GetAnswerMessage(string message);
    }
}
