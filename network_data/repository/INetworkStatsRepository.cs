using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetworkData.Repositories
{
    public interface INetworkStatsRepository
    {
        Task<IEnumerable<(string Version, int Rsrp, int Snr)>> GetVersionStatsAsync5G(int is_nr_5g);
        Task<IEnumerable<(string Version, int Rsrp, int Snr)>> GetVersionStatsAsync4G(int is_lte);
    }
}
