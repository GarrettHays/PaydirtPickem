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
            var daysToGet = 3;
            if (lastDateScored != null)
            {
                daysToGet = Convert.ToInt32((DateTime.UtcNow - lastDateScored.LastScored).TotalDays);
                //API only accepts values for 'daysFrom' event to be 1, 2 or 3.
                if (daysToGet > 3)
                {
                    daysToGet = 3;
                } 
            }
            var gameLogic = new GameLogic();
            await gameLogic.GetScores(daysToGet, _context);

            return NoContent();
        }
    }
}
