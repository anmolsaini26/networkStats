using Microsoft.AspNetCore.Mvc;
using NetworkData.Services;
using System.Threading.Tasks;

namespace NetworkData.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NetworkStatsController : ControllerBase
    {
        private readonly INetworkStatsService _networkStatsService;

        public NetworkStatsController(INetworkStatsService networkStatsService)
        {
            _networkStatsService = networkStatsService;
        }

        [HttpGet("aparna")]
        public async Task<IActionResult> GetNetworkStats([FromQuery] int nr_5g)
        {
            if (nr_5g != 0 && nr_5g != 1)
            {
                return BadRequest("The query parameter 'nr_5g' must be 0 or 1.");
            }

            var count = await _networkStatsService.GetTotalCountByNr5gAsync(nr_5g);
            return Ok(new { nr_5g, totalCount = count });
        }
    }
}
