using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;


namespace Servicio
{

    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class LobbyService : ILobby
    {


        private static ConcurrentDictionary<string,ConcurrentDictionary<string,ILobbyCallback>> lobbies = new ConcurrentDictionary<string, ConcurrentDictionary<string, ILobbyCallback>>();



        public bool Connect(string lobbyCode, string username)
        {
            
            if (lobbies.ContainsKey(lobbyCode))
            {
            
                ConcurrentDictionary<string, ILobbyCallback> lobby = lobbies[lobbyCode];

                if (lobby.ContainsKey(username))
                {
                    return false;
                }
                else
                {
                    var callback = OperationContext.Current.GetCallbackChannel<ILobbyCallback>();
                    lobby.TryAdd(username, callback);
                    return true;
                }
            }
            else
            {
                var callback = OperationContext.Current.GetCallbackChannel<ILobbyCallback>();
                ConcurrentDictionary<string, ILobbyCallback> lobby = new ConcurrentDictionary<string, ILobbyCallback>();
                lobby.TryAdd(username, callback);
                lobbies.TryAdd(lobbyCode, lobby);
                return true;
            }
        }

        public bool Disconnect(string lobbyCode, string username)
        {
            if (lobbies.ContainsKey(lobbyCode))
            {
                ConcurrentDictionary<string, ILobbyCallback> lobby = lobbies[lobbyCode];
                if (lobby.ContainsKey(username))
                {
                    ILobbyCallback callback;
                    lobby.TryRemove(username, out callback);

                    if (lobby.IsEmpty)
                    {
                        ConcurrentDictionary<string, ILobbyCallback> removed;
                        lobbies.TryRemove(lobbyCode, out removed);
                    }
                    
                    return true;
                }
            }

            return false;
        }

        public void SendMessage(Message message)
        {
            // Debugging, we dk how to log brah
            Console.WriteLine($"Received message from {message.UserName}: {message.Text}");

            var callback = OperationContext.Current.GetCallbackChannel<ILobbyCallback>();

            if (lobbies.ContainsKey(message.LobbyCode))
            {
                ConcurrentDictionary<string, ILobbyCallback> lobby = lobbies[message.LobbyCode];

                //MIGHT BE REDUNDANT ???                
                if (!lobby.ContainsKey(message.UserName))
                {

                    bool added = lobby.TryAdd(message.UserName, callback);
                    if (added)
                    {
                        Console.WriteLine($"User {message.UserName} added to the lobby.");
                    }
                    else
                    {
                        Console.WriteLine($"Failed to add user {message.UserName} to the lobby.");
                    }
                }
                else
                {
                    lobby[message.UserName] = callback;
                    Console.WriteLine($"User {message.UserName} callback updated in the lobby.");
                }

                //Debugging
                Console.WriteLine($"Current users in lobby {message.LobbyCode}:");
                foreach (var user in lobby)
                {
                    Console.WriteLine($"User: {user.Key}");
                }

                //Send msgs to all users
                foreach (var user in lobby)
                {
                    try
                    {
                        user.Value.GetMessage(message);
                        Console.WriteLine("Message sent to: " + user.Key);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to send message to {user.Key}: {ex.Message}");
                    }
                }
            }
            else
            {
                Console.WriteLine($"Lobby {message.LobbyCode} does not exist.");
            }
        }
    }
}