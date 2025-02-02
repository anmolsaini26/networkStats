using System.Threading.Tasks;

namespace NetworkData.Services
{
    public interface INetworkStatsService
    {
        Task<int> GetTotalCountByNr5gAsync(int nr_5g);
    }
}