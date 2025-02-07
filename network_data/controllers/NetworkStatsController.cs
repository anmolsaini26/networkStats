using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetworkData.Services;

namespace NetworkData.Controllers
{
    [ApiController]
    [Route("api/network_stats")]
    public class NetworkStatsController : ControllerBase
    {
        private readonly INetworkStatsService _networkStatsService;

        public NetworkStatsController(INetworkStatsService networkStatsService)
        {
            _networkStatsService = networkStatsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetNetworkStats()
        {
            var result = await _networkStatsService.GetVersionStatsAsync();
            return Ok(result);
        }
    }
}
