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
    public class MatchesController : ControllerBase
    {
        private readonly TournamentPlannerDbContext _context;
        private readonly ILogger<MatchesController> _logger;

        public MatchesController(TournamentPlannerDbContext context, ILogger<MatchesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("open")]
        public async Task<ActionResult<List<Match>>> GetOpenMatches()
        {
            var openMatches = await _context.GetIncompleteMatches();
            if (openMatches == null || !openMatches.Any())
                return NotFound();

            return Ok(openMatches);
        }

        [HttpPost("generate")]
        public async Task<ActionResult<List<Match>>> GenerateMatchesForNextRound()
        {
            await _context.GenerateMatchesForNextRound();
            return await GetOpenMatches();
        }
    }
}