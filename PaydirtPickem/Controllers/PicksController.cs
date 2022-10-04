using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using PaydirtPickem.Data;
using PaydirtPickem.Logic;
using PaydirtPickem.Models;
using System.Security.Claims;

namespace PaydirtPickem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PicksController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly PaydirtPickemDbContext _db;

        public PicksController(UserManager<ApplicationUser> userManager, PaydirtPickemDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        [HttpGet]
        public async Task<List<UserPick>> Get()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentUser = await _userManager.FindByIdAsync(userId);

            var gamesLogic = new GameLogic();
            var currentWeek = gamesLogic.GetWeekNumberForGame(DateTime.UtcNow);

            var picks = _db.UserPicks.Where(x => x.UserId == Guid.Parse(currentUser.Id) && x.Game.WeekNumber == currentWeek)?.Include(x => x.Game).ToList();
            return picks;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task Post([FromBody]Dictionary<Guid, string> userPicks)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentUser = await _userManager.FindByIdAsync(userId);

            foreach (var pick in userPicks)
            {
                var userPick = new UserPick();
                userPick.GameId = pick.Key;
                userPick.PickedTeam = pick.Value;
                userPick.UserId = Guid.Parse(currentUser.Id);
                _db.UserPicks.Add(userPick);
                _db.SaveChanges();
            }
        }
    }
}
