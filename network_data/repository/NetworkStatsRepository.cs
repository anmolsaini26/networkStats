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

        public async Task<IEnumerable<(string Version, int NrRsrp, int NrSnr)>> GetVersionStatsAsync(int is_nr_5g)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            Console.Write(is_nr_5g);
            var query = @"
                SELECT version, nr_rsrp, nr_snr 
                FROM network_data 
                WHERE is_nr_5g = @is_nr_5g";
            return await connection.QueryAsync<(string Version, int NrRsrp, int NrSnr)>(query, new { is_nr_5g });

        }
    }
}
