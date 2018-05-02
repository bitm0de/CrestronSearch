using System;
using System.Net;
using System.Net.Sockets;

namespace CrestronSearch.Crestron.Network
{
    internal class Helpers
    {
        public static IPAddress GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip;
                }
            }
            throw new Exception("Local IP address not found");
        }
    }
}
