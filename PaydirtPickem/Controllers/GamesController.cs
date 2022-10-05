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
    public class GamesController : ControllerBase
    {
        private readonly PaydirtPickemDbContext _context;

        public GamesController(PaydirtPickemDbContext context)
        {
            _context = context;
        }

        // GET: api/Games
        [HttpGet]
        public async Task<List<Game>> Get()
        {
            var games = _context.Games.Where(x => x.IsActive).OrderBy(x => x.GameTime).ToList();
            return games;
        }

        // POST: api/Games
        [HttpPost]
        public async Task<ActionResult> PostGame()
        {
            var gamesLogic = new GameLogic();
            var currentWeek = gamesLogic.GetWeekNumberForGame(DateTime.UtcNow);

            if (currentWeek != null)
            {
                gamesLogic.CheckForInactiveGames(_context, currentWeek.Value);
            }

            var games = await gamesLogic.GetGames();
            foreach (var game in games)
            {
                var weekNumber = gamesLogic.GetWeekNumberForGame(game.GameTime.Value);
                
                var isActive = currentWeek == weekNumber;

                if (!GameExists(game.Id) && isActive)
                {
                    var newGame = new Game()
                    {
                        Id = game.Id,
                        HomeTeam = game.HomeTeam,
                        AwayTeam = game.AwayTeam,
                        HomeTeamSpread = game.HomeTeamSpread,
                        GameTime = game.GameTime,
                        WeekNumber = weekNumber, 
                        IsActive = isActive
                    };
                    _context.Games.Add(newGame);
                    
                }
            }
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(Guid id)
        {
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
