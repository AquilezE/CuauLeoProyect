using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;


namespace Servicio
{

    public class Service1 : ILobby
    {


        private static ConcurrentDictionary<string,ConcurrentDictionary<string,ILobbyCallback>> lobbies = new ConcurrentDictionary<string, ConcurrentDictionary<string, ILobbyCallback>>();



        public bool Connect(string lobbyCode, string username)
        {
            
            // Check if the lobby exists
            if (lobbies.ContainsKey(lobbyCode))
            {
            
                // Get the lobby from the dictionary
                ConcurrentDictionary<string, ILobbyCallback> lobby = lobbies[lobbyCode];

                // Check if the user is already in the lobby
                if (lobby.ContainsKey(username))
                {
                    return false;
                }
                else
                {
                    // Add the user to the lobby
                    var callback = OperationContext.Current.GetCallbackChannel<ILobbyCallback>();
                    lobby.TryAdd(username, callback);
                    return true;
                }
            }
            else
            {
                // Create a new lobby and add the user to it
                var callback = OperationContext.Current.GetCallbackChannel<ILobbyCallback>();
                ConcurrentDictionary<string, ILobbyCallback> lobby = new ConcurrentDictionary<string, ILobbyCallback>();
                lobby.TryAdd(username, callback);
                lobbies.TryAdd(lobbyCode, lobby);
                return true;
            }
        }

        public bool Disconnect(string lobbyCode, string username)
        {
            // Check if the lobby exists
            if (lobbies.ContainsKey(lobbyCode))
            {
                // Get the lobby from the dictionary
                ConcurrentDictionary<string, ILobbyCallback> lobby = lobbies[lobbyCode];

                // Check if the user is in the lobby
                if (lobby.ContainsKey(username))
                {
                    // Remove the user from the lobby
                    ILobbyCallback callback;
                    lobby.TryRemove(username, out callback);

                    // Check if the lobby is empty
                    if (lobby.IsEmpty)
                    {
                        // Remove the lobby from the dictionary
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
            // Debugging: Print the incoming message details
            Console.WriteLine($"Received message from {message.UserName}: {message.Text}");

            // Get the callback channel for the current operation context
            var callback = OperationContext.Current.GetCallbackChannel<ILobbyCallback>();

            // Check if the lobby exists
            if (lobbies.ContainsKey(message.LobbyCode))
            {
                // Get the lobby from the dictionary
                ConcurrentDictionary<string, ILobbyCallback> lobby = lobbies[message.LobbyCode];

                //MIGHT BE REDUNDANT
                // Check if the user is already in the lobby
                
                if (!lobby.ContainsKey(message.UserName))
                {
                    // Try to add the user and their callback channel to the lobby
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
                    // Update the callback channel if it already exists
                    lobby[message.UserName] = callback;
                    Console.WriteLine($"User {message.UserName} callback updated in the lobby.");
                }

                // Print the current state of the lobby
                Console.WriteLine($"Current users in lobby {message.LobbyCode}:");
                foreach (var user in lobby)
                {
                    Console.WriteLine($"User: {user.Key}");
                }

                // Send the message to all users in the lobby
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