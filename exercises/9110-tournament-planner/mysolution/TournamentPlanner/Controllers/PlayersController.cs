using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TournamentPlanner.Data;

namespace TournamentPlanner.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayersController : ControllerBase
    {
        private readonly TournamentPlannerDbContext _context;
        private readonly ILogger<PlayersController> _logger;

        public PlayersController(TournamentPlannerDbContext context, ILogger<PlayersController> logger)
        {
            _context = context;
            _logger = logger;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetPlayers([FromQuery] string name = null)
        {
            return Ok(await _context.GetFilteredPlayers(name));
        }

        [HttpPost]
        public async Task<ActionResult<Player>> AddPlayer([FromBody] Player newPlayer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            return Ok(await _context.AddPlayer(newPlayer));
        }
    }
}