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
        public async Task<IActionResult> GetNetworkStats(  [FromQuery] int? is_nr_5g, [FromQuery] int? is_lte)
        {
            var result = await _networkStatsService.GetVersionStatsAsync(is_nr_5g, is_lte);
            return Ok(result);

            // if(is_nr_5g.HasValue)   
            // {
            //     var result = await _networkStatsService.GetVersionStatsAsync(is_nr_5g);
            //     return Ok(result);
            // }

            // else if(is_lte.HasValue)
            // {
            //     var result = await _networkStatsService.GetVersionStatsAsync(is_lte);
            //     return Ok(result);
            // }
            // else
            //     return BadRequest("Please provide a valid query parameter");

            
        }
    }
}
