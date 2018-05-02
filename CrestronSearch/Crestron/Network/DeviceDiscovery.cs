using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CrestronSearch.Crestron.Network
{
    internal class DeviceDiscovery
    {
        private static readonly List<IPAddress> _addresses = new List<IPAddress>();
        
        public static async Task SearchAsync()
        {
            var sendBytes = Encoding.ASCII.GetBytes("\x14\x00\x00\x00\x01\x04\x00\x03\x00\x00");

            UdpSearch.Error += exception => Console.WriteLine("Broadcast UDP search exception: {0}", exception.Message);
            UdpSearch.Destroyed += () => Console.WriteLine("UDP listener socket was destroyed");
            UdpSearch.RawDataReceived += (bytes, ip) =>
            {
                var data = Encoding.ASCII.GetString(bytes).Split(new[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);

                if (_addresses.Contains(ip.Address))
                    return;

                _addresses.Add(ip.Address);

                if (data.Length >= 5)
                    Console.WriteLine($"[{ip.Address}] {data[3]} - {data[4]}");
            };

#pragma warning disable 4014
            UdpSearch.StartListenerAsync(Constants.CIP_PORT).ConfigureAwait(false);
#pragma warning restore 4014

            while (true)
            {
                await UdpSearch.SendAsync(Constants.CIP_PORT, sendBytes);
                await Task.Delay(TimeSpan.FromSeconds(3));
            }
        }
    }
}
