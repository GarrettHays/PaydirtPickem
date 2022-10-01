using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
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
        [AllowAnonymous]
        public async Task<List<Game>> Get()
        {
            var games = _db.Games.Where(x => x.IsActive).ToList();
            return games;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task Post([FromBody]List<UserPickDTO> userPicks)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentUser = await _userManager.FindByIdAsync(userId);

            var accessToken = Request.Headers[HeaderNames.Authorization];

            foreach (var pick in userPicks)
            {
                var userPick = new UserPick();
                userPick.GameId = pick.GameId;
                userPick.PickedTeam = pick.PickedTeam;
                userPick.UserId = Guid.Parse(currentUser.Id);
                _db.UserPicks.Add(userPick);
                _db.SaveChanges();
            }
        }
    }
}
