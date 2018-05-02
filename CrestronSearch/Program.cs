using System;
using CrestronSearch.Crestron.Network;

namespace CrestronSearch
{
    internal class Program
    {
        private static void Main()
        {
            try
            {
#pragma warning disable 4014
                DeviceDiscovery.SearchAsync();
#pragma warning restore 4014
            }
            catch (Exception ex) // doesn't matter at this point just show the exception
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }

    }
}