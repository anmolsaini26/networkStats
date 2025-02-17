using System.Threading.Tasks;
using NetworkData.DTOs;

namespace NetworkData.Services
{
    public interface INetworkStatsService
    {
        Task<NetworkStatsDto> GetVersionStatsAsync(int? is_nr_5g, int? is_lte);
    }
}
