using System;

namespace GameServer
{
    class ServerHandle
    {
        public static void WelcomeReceived(int fromClient, Packet packet)
        {
            int clientId = packet.ReadInt();
            string username = packet.ReadString();

            Console.WriteLine($"{Server.clients[fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {fromClient}");
            if(fromClient != clientId)
            {
                Console.WriteLine($"Player \"{username}\" (ID: {fromClient}) has assumed the wrong client ID ({clientId})!");
            }

            Server.clients[fromClient].SendIntoGame(username);
        }
    }
}
