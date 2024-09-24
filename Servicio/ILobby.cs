using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Servicio
{
    [ServiceContract(CallbackContract = typeof(ILobbyCallback))]
    internal interface ILobby
    {
        [OperationContract(IsOneWay = true)]
        void SendMessage(Message mensaje);

        [OperationContract]
        bool Connect(string lobbyCode, string username);

        [OperationContract]
        bool Disconnect(string lobbyCode, string username);
    }



}
