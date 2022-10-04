using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaydirtPickem.Data;
using PaydirtPickem.Logic;
using PaydirtPickem.Models;

namespace PaydirtPickem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoresController : ControllerBase
    {
        private readonly PaydirtPickemDbContext _context;

        public ScoresController(PaydirtPickemDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostScore(Guid id)
        {
            var lastDateScored = await _context.LastScored.FirstOrDefaultAsync();
            if (lastDateScored != null)
            {
                var daysToGet = (DateTime.UtcNow - lastDateScored.LastScored).TotalDays;
            }
            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            _context.Games.Remove(game);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GameExists(Guid id)
        {
            return _context.Games.Any(e => e.Id == id);
        }
    }
}
