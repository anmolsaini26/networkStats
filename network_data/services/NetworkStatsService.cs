using System.Threading.Tasks;
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

        public async Task<int> GetTotalCountByNr5gAsync(int nr_5g)
        {
            return await _repository.GetTotalCountByNr5gAsync(nr_5g);
        }
    }
}
