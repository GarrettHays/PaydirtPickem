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
        public async Task<ActionResult<IEnumerable<Game>>> GetGames()
        {
            return await _context.Games.ToListAsync();
        }

        // GET: api/Games/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetGame(Guid id)
        {
            var game = await _context.Games.FindAsync(id);

            if (game == null)
            {
                return NotFound();
            }

            return game;
        }

        // PUT: api/Games/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(Guid id, Game game)
        {
            if (id != game.Id)
            {
                return BadRequest();
            }

            _context.Entry(game).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Games
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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
