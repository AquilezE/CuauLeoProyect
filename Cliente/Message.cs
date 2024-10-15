﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente
{
    internal class Message
    {



        public Message(string username, string messageText, int lobbyCode)
        {
            UserName = username;
            MessageText = messageText;
            LobbyCode = lobbyCode;
        }

        public string UserName { get; set; }
        public string MessageText { get; set; }
        public int LobbyCode { get; set; }
    }
}
