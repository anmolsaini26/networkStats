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

        public async Task<NetworkStatsDto> GetVersionStatsAsync()
        {
            var result = await _repository.GetVersionStatsAsync();
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
