using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using PaydirtPickem.Data;
using PaydirtPickem.Logic;
using PaydirtPickem.Models;
using PaydirtPickem.Models.DTOs;

namespace PaydirtPickem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoresController : ControllerBase
    {
        private readonly PaydirtPickemDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ScoresController(UserManager<ApplicationUser> userManager, PaydirtPickemDbContext context)
        {
            _context = context;
            _userManager = userManager;
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

        [HttpGet]
        public async Task<List<SeasonScoreDTO>> Get()
        {
            var scores = _context.UserSeasonScores.ToList();
            var seasonScores = new List<SeasonScoreDTO>();

            foreach (var score in scores)
            {
                var currentUser = await _userManager.FindByIdAsync(score.UserId.ToString());
                var scoreDTO = new SeasonScoreDTO
                {
                    SeasonTotalWin = score.SeasonTotalWin,
                    SeasonTotalLoss = score.SeasonTotalLoss,
                    UserId = score.UserId,
                    TeamName = currentUser.TeamName
                };
                seasonScores.Add(scoreDTO);
            }

            return seasonScores.OrderByDescending(x => x.SeasonTotalWin).ToList();
        }
    }
}
