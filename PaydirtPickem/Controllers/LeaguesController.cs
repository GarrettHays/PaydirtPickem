using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaydirtPickem.Data;
using PaydirtPickem.Models;

namespace PaydirtPickem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaguesController : ControllerBase
    {
        private readonly PaydirtPickemDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public LeaguesController(UserManager<ApplicationUser> userManager, PaydirtPickemDbContext context)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Leagues
        [HttpGet]
        public async Task<ActionResult<IEnumerable<League>>> GetLeague()
        {
            return await _context.League.ToListAsync();
        }

        // GET: api/Leagues/5
        [HttpGet("{id}")]
        public async Task<ActionResult<League>> GetLeague(Guid id)
        {
            var league = await _context.League.FindAsync(id);

            if (league == null)
            {
                return NotFound();
            }

            return league;
        }

        // PUT: api/Leagues/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLeague(Guid id, League league)
        {
            if (id != league.Id)
            {
                return BadRequest();
            }

            _context.Entry(league).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeagueExists(id))
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

        // POST: api/Leagues
        [HttpPost]
        public async Task<ActionResult<League>> PostLeague(string leagueName)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentUser = await _userManager.FindByIdAsync(userId);
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                var league = new League
                {
                    LeagueName = leagueName
                };
                _context.League.Add(league);
                await _context.SaveChangesAsync();
                var createdLeague = _context.League.Include(x => x.Users).FirstOrDefault(x => x.LeagueName == leagueName);
                //var currentUser = 
                createdLeague.Users.Add(currentUser);

                await _context.SaveChangesAsync();
                dbContextTransaction.Commit();

                return CreatedAtAction("GetLeague", new { id = league.Id }, league);
            }
        }

        // DELETE: api/Leagues/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeague(Guid id)
        {
            var league = await _context.League.FindAsync(id);
            if (league == null)
            {
                return NotFound();
            }

            _context.League.Remove(league);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LeagueExists(Guid id)
        {
            return _context.League.Any(e => e.Id == id);
        }
    }
}
