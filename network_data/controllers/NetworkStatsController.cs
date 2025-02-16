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
        public async Task<IActionResult> GetNetworkStats(  [FromQuery] int is_nr_5g)
        {   
            // if (is_nr_5g != 1)
            // {
            //     return BadRequest("This endpoint supports only is_nr_5g=1.");
            // }
            var result = await _networkStatsService.GetVersionStatsAsync(is_nr_5g);
            return Ok(result);
        }
    }
}
