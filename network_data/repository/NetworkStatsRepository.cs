using Npgsql;
using System;
using System.Threading.Tasks;

namespace NetworkData.Repositories
{
    public class NetworkStatsRepository : INetworkStatsRepository
    {
        private readonly string _connectionString;

        public NetworkStatsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> GetTotalCountByNr5gAsync(int nr_5g)
        {
            int count = 0;

            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                await connection.OpenAsync();

                string query = "SELECT COUNT(*) FROM network_data WHERE is_nr_5g = @nr_5g";

                using var command = new NpgsqlCommand(query, connection);
                command.Parameters.AddWithValue("@nr_5g", nr_5g);

                // Use Convert.ToInt32 to handle the cast
                count = Convert.ToInt32(await command.ExecuteScalarAsync());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing query: {ex.Message}");
                throw;
            }

            return count;
        }
    }
}
