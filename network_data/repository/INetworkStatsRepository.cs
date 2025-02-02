using System.Threading.Tasks;

namespace NetworkData.Repositories
{
    public interface INetworkStatsRepository
    {
        Task<int> GetTotalCountByNr5gAsync(int nr_5g);
    }
}
