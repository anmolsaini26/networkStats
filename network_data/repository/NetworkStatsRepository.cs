using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using NetworkData.DTOs;

namespace NetworkData.Repositories
{
    public class NetworkStatsRepository : INetworkStatsRepository
    {
        private readonly string _connectionString;

        public NetworkStatsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<(string Version, int NrRsrp, int NrSnr)>> GetVersionStatsAsync()
        {
            using var connection = new NpgsqlConnection(_connectionString);
            var query = @"
                SELECT version, nr_rsrp, nr_snr 
                FROM network_data 
                WHERE is_nr_5g = 1";

            return await connection.QueryAsync<(string Version, int NrRsrp, int NrSnr)>(query);
        }
    }
}
