using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetworkData.Repositories
{
    public interface INetworkStatsRepository
    {
        Task<IEnumerable<(string Version, int NrRsrp, int NrSnr)>> GetVersionStatsAsync(int is_nr_5g);
    }
}
