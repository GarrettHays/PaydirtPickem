using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaydirtPickem.Logic;
using PaydirtPickem.Models;

namespace PaydirtPickem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PicksController : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<List<PickDTO>> Get()
        {
            var picksLogic = new PicksLogic();
            var picks = await picksLogic.GetPicks();
            return picks;
        }
    }
}
