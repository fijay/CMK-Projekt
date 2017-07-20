using Newtonsoft.Json;
using Quobject.SocketIoClientDotNet.Client;
using System;

namespace SocketIO
{
    internal class Program
    {
        private static ServerInfoJson _serverInfoJson;
        private static bool _isConnected;
        private static Socket _socket;

        private static void Main(string[] args)
        {
            var url = "";//ToDo Server hier eintragen
            _isConnected = false;
            Connect(url);
        }
        /// <summary>
        /// Connected to the Server
        /// </summary>
        /// <param name="url"></param>
        private static void Connect(string url)
        {
            _socket = IO.Socket(url);
            _socket.On(Socket.EVENT_CONNECT, () =>
            { _isConnected = true; });
            _socket.On("update", SocketUpdate);
            var test = Console.ReadLine();
            if (test == "A")//Nur zum testen hier
            {
                SendToServer(null, null, "b");
            }
            test = Console.ReadLine();
            Disconnect();
        }

        /// <summary>
        /// Disconnectet from the Server
        /// </summary>
        private static void Disconnect()
        {
            _socket.Disconnect();
            _socket.On(Socket.EVENT_DISCONNECT, () =>
                { _isConnected = false; });
        }

        /// <summary>
        /// Create the json for the socket
        /// </summary>
        /// <returns></returns>
        private static string CreateJson(ServerInfoJson jsonType)
        {
            return JsonConvert.SerializeObject(jsonType);
        }

        /// <summary>
        /// Get the new Data from the Server
        /// </summary>
        /// <param name="data"></param>
        private static void SocketUpdate(object data)
        {
            Console.WriteLine(data);
            var jsonTempString = data.ToString();
            if (!string.IsNullOrEmpty(jsonTempString))
            {
                _serverInfoJson = JsonConvert.DeserializeObject<ServerInfoJson>(jsonTempString);
            }
        }
        /// <summary>
        /// Send the updated Values to the Server
        /// </summary>
        /// <param name="size"></param>
        /// <param name="rotation"></param>
        /// <param name="sound"></param>
        private static void SendToServer(string size = null,
            string rotation = null, string sound = null)
        {
            if (_isConnected)
            {
                var dataToServer = new ServerInfoJson
                {
                    rotation = rotation,
                    size = size,
                    sound = sound
                };
                var jsonString = CreateJson(dataToServer);
                _socket.Send(jsonString);
            }
        }
    }
}