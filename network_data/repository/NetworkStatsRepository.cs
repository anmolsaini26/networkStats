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

        public async Task<IEnumerable<(string Version, int Rsrp, int Snr)>> GetVersionStatsAsync5G(int is_nr_5g)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            Console.Write(is_nr_5g);
            var query = @"
                SELECT version, nr_rsrp, nr_snr 
                FROM network_data 
                WHERE is_nr_5g = @is_nr_5g";
            return await connection.QueryAsync<(string Version, int Rsrp, int Snr)>(query, new { is_nr_5g });

        }

        public async Task<IEnumerable<(string Version, int Rsrp, int Snr)>> GetVersionStatsAsync4G(int is_lte)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            Console.Write(is_lte);
            var query = @"
                SELECT version, rsrp, snr 
                FROM network_data 
                WHERE is_lte = @is_lte";
            return await connection.QueryAsync<(string Version, int Rsrp, int Snr)>(query, new { is_lte });

        }
    }
}
