using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Servicio.Clases;

namespace Servicio.Interfaces
{

    [ServiceContract]
    internal interface ILogIn
    {
        [OperationContract]

        User TryLogIn(string user, string password);



    }
}
