using System;
using CrestronSearch.Crestron;
using CrestronSearch.Crestron.Network;

namespace CrestronSearch
{
    internal class Program
    {
        private static void Main()
        {
            try
            {
                DeviceDiscovery.SearchAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
            Console.ReadKey();
        }

    }
}