using System.Collections.Generic;

namespace NetworkData.DTOs
{
    public class NetworkStatsDto
    {
        public Dictionary<string, VersionStats> IsRAT { get; set; } = new Dictionary<string, VersionStats>();
    }

    public class VersionStats
    {
        public Dictionary<string, SignalQualityStats> RSRP { get; set; } = new Dictionary<string, SignalQualityStats>();
        public int GrandTotal { get; set; }
    }

    public class SignalQualityStats
    {
        public Dictionary<string, int> SNR { get; set; } = new Dictionary<string, int>();
                // public int Total => SNR.Values.Sum();

    }
}
