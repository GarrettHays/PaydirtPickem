using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        
        public PicksController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<List<PickDTO>> Get()
        {
            var picksLogic = new PicksLogic();
            var picks = await picksLogic.GetPicks();
            return picks;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task Post([FromBody]List<UserPick> userPicks)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            foreach (var pick in userPicks)
            {
                pick.UserId = Guid.Parse(userId);
                pick.Id = Guid.NewGuid();
            }
        }
    }
}
