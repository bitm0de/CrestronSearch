using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace CrestronSearch.Crestron.Network
{
    internal abstract class UdpSearch
    {
        public static event RawDataReceivedEventHandler RawDataReceived;
        public delegate void RawDataReceivedEventHandler(byte[] receivedBytes, IPEndPoint ip);
        
        public static event ErrorEventHandler Error;
        public delegate void ErrorEventHandler(Exception ex);

        public static event DestroyedEventHandler Destroyed;
        public delegate void DestroyedEventHandler();

        public static async Task<int> SendAsync(int port, byte[] bytes)
        {
            using (var udpClient = new UdpClient(AddressFamily.InterNetwork))
            {
                udpClient.ExclusiveAddressUse = false;
                udpClient.AllowNatTraversal(true);
                return await udpClient.SendAsync(bytes, bytes.Length, new IPEndPoint(IPAddress.Broadcast, port));
            }
        }

        public static async Task StartListenerAsync(int port)
        {
            var localIP = Helpers.GetLocalIPAddress();
            UdpClient listener = null;
            try
            {
                listener = new UdpClient(new IPEndPoint(IPAddress.Any, port));
                while (true)
                {
                    var result = await listener.ReceiveAsync();
                    var bytes = result.Buffer;
                    if (!result.RemoteEndPoint.Address.Equals(localIP))
                    {
                        OnRawDataReceived(bytes, result.RemoteEndPoint);
                    }
                }

            }
            catch (Exception ex)
            {
                OnError(ex);
            }
            finally
            {
                listener?.Close();
                OnDestroyed();
            }
        }

        private static void OnRawDataReceived(byte[] receivedbytes, IPEndPoint ip)
        {
            RawDataReceived?.Invoke(receivedbytes, ip);
        }

        private static void OnError(Exception ex)
        {
            Error?.Invoke(ex);
        }

        private static void OnDestroyed()
        {
            Destroyed?.Invoke();
        }
    }
}