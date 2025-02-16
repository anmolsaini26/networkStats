using System.Collections.Generic;

namespace NetworkData.DTOs
{
    public class NetworkStatsDto
    {
        public Dictionary<string, VersionStats> IsNr5g1 { get; set; } = new Dictionary<string, VersionStats>();
    }

    public class VersionStats
    {
        public Dictionary<string, SignalQualityStats> NR_RSRP { get; set; } = new Dictionary<string, SignalQualityStats>();
        public int GrandTotal { get; set; }
    }

    public class SignalQualityStats
    {
        public Dictionary<string, int> NR_SNR { get; set; } = new Dictionary<string, int>();
                // public int Total => NR_SNR.Values.Sum();

    }
}
