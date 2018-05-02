namespace CrestronSearch.Crestron.Types
{
    internal struct Device
    {
        public string Name { get; }
        public string Hostname { get; }
        public Firmware Firmware { get; }

        public Device(string name, string hostname, Firmware firmware)
        {
            Firmware = firmware;
            Hostname = hostname;
            Name = name;
        }

        public override string ToString()
        {
            return $"{Hostname}\t- ({Name})\t- [{Firmware}]";
        }
    }
}
