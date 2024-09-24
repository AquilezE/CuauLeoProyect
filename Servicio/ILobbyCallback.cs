using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Servicio
{

    internal interface ILobbyCallback
    {
        [OperationContract(IsOneWay = true)]
        void GetMessage(Message message);

        [OperationContract]
        bool JoinLobby();
        
        [OperationContract]
        bool LeaveLobby();
    }
}
