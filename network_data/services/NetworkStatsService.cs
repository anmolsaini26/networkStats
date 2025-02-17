using System.Threading.Tasks;
using NetworkData.DTOs;
using System.Collections.Generic;
using NetworkData.Repositories;

namespace NetworkData.Services
{
    public class NetworkStatsService : INetworkStatsService
    {
        private readonly INetworkStatsRepository _repository;

        public NetworkStatsService(INetworkStatsRepository repository)
        {
            _repository = repository;
        }

        public async Task<NetworkStatsDto> GetVersionStatsAsync(int? is_nr_5g, int? is_lte)
        {
            IEnumerable<(string Version, int Rsrp, int Snr)> result = null;
            if(is_nr_5g.HasValue)
            {
                result = await _repository.GetVersionStatsAsync5G(is_nr_5g.Value);
            }
            else if(is_lte.HasValue)
            {
                result = await _repository.GetVersionStatsAsync4G(is_lte.Value);
            }
            
            var response = new NetworkStatsDto();

            foreach (var row in result)
            {
                string rsrpCategory = CategorizeRsrp(row.Rsrp);
                string snrCategory = CategorizeSnr(row.Snr);

                if (!response.IsRAT.ContainsKey(row.Version))
                {
                    response.IsRAT[row.Version] = new VersionStats();
                }

                if (!response.IsRAT[row.Version].RSRP.ContainsKey(rsrpCategory))
                {
                    response.IsRAT[row.Version].RSRP[rsrpCategory] = new SignalQualityStats
                    {
                        SNR = new Dictionary<string, int>
                        {
                            { "Poor SNR", 0 },
                            { "Good SNR", 0 },
                            { "Very Good SNR", 0 }
                        }
                    };
                }

                response.IsRAT[row.Version].RSRP[rsrpCategory].SNR[snrCategory]++;
            }
            foreach (var versionEntry in response.IsRAT)
            {
                foreach (var rsrpEntry in versionEntry.Value.RSRP)
                {
                    // Calculate the total from the SNR counts
                    int total = rsrpEntry.Value.SNR.Values.Sum();
                    // Add or update the "total" key inside the same dictionary
                    rsrpEntry.Value.SNR["total"] = total;
                }
            }

            // Compute a grand total per version (summing all RSRP category totals)
            foreach (var versionEntry in response.IsRAT)
            {
                int grandTotal = versionEntry.Value.RSRP.Values.Sum(sqs => sqs.SNR["total"]);
                versionEntry.Value.GrandTotal = grandTotal;
            }

            return response;
        }

        private string CategorizeRsrp(int rsrp)
        {
            if (rsrp == -74)
                return "Very Good RSRP";
            else if (rsrp > -100 && rsrp <= -75)
                return "Good RSRP";
            else if (rsrp >= -140 && rsrp <= -100)
                return "Poor RSRP";
            else
                return "NuLL";
        }

        private string CategorizeSnr(int snr)
        {
            if (snr <= 0)
                return "Poor SNR";
            else if (snr > 0 && snr <= 6)
                return "Good SNR";
            else if (snr > 6)
                return "Very Good SNR";
            else return "----";
        }
    }
}
