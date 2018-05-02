using System;

namespace CrestronSearch.Crestron.Types
{
    public struct Firmware
    {
        public DateTime ReleaseDate { get; }
        public Version ReleaseVersion { get; }

        public Firmware(DateTime releaseDate, Version version)
        {
            ReleaseDate = releaseDate;
            ReleaseVersion = version;
        }
        public override string ToString()
        {
            return $"Firmware v{ReleaseVersion.Major}.{ReleaseVersion.Minor:D3}.{ReleaseVersion.Build:D4} ({ReleaseDate.ToString("MMMM dd, yyyy")})";
        }
    }
}
