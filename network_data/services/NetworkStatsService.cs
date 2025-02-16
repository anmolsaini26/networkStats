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

        public async Task<NetworkStatsDto> GetVersionStatsAsync(int is_nr_5g)
        {
            var result = await _repository.GetVersionStatsAsync(is_nr_5g);
            var response = new NetworkStatsDto();

            foreach (var row in result)
            {
                string rsrpCategory = CategorizeRsrp(row.NrRsrp);
                string snrCategory = CategorizeSnr(row.NrSnr);

                if (!response.IsNr5g1.ContainsKey(row.Version))
                {
                    response.IsNr5g1[row.Version] = new VersionStats();
                }

                if (!response.IsNr5g1[row.Version].NR_RSRP.ContainsKey(rsrpCategory))
                {
                    response.IsNr5g1[row.Version].NR_RSRP[rsrpCategory] = new SignalQualityStats
                    {
                        NR_SNR = new Dictionary<string, int>
                        {
                            { "Poor NR SNR", 0 },
                            { "Good NR SNR", 0 },
                            { "Very Good NR SNR", 0 }
                        }
                    };
                }

                response.IsNr5g1[row.Version].NR_RSRP[rsrpCategory].NR_SNR[snrCategory]++;
            }
            foreach (var versionEntry in response.IsNr5g1)
            {
                foreach (var rsrpEntry in versionEntry.Value.NR_RSRP)
                {
                    // Calculate the total from the SNR counts
                    int total = rsrpEntry.Value.NR_SNR.Values.Sum();
                    // Add or update the "total" key inside the same dictionary
                    rsrpEntry.Value.NR_SNR["total"] = total;
                }
            }

            // Compute a grand total per version (summing all RSRP category totals)
            foreach (var versionEntry in response.IsNr5g1)
            {
                int grandTotal = versionEntry.Value.NR_RSRP.Values.Sum(sqs => sqs.NR_SNR["total"]);
                versionEntry.Value.GrandTotal = grandTotal;
            }

            return response;
        }

        private string CategorizeRsrp(int rsrp)
        {
            if (rsrp == -74)
                return "Very Good NR RSRP";
            else if (rsrp > -100 && rsrp <= -75)
                return "Good NR RSRP";
            else if (rsrp >= -140 && rsrp <= -100)
                return "Poor NR RSRP";
            else
                return "NuLL";
        }

        private string CategorizeSnr(int snr)
        {
            if (snr <= 0)
                return "Poor NR SNR";
            else if (snr > 0 && snr <= 6)
                return "Good NR SNR";
            else if (snr > 6)
                return "Very Good NR SNR";
            else return "----";
        }
    }
}
