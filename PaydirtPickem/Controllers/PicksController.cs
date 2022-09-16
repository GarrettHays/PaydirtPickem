using Microsoft.AspNetCore.Mvc;
using PaydirtPickem.Logic;
using PaydirtPickem.Models;

namespace PaydirtPickem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PicksController : ControllerBase
    {
        //[HttpGet]
        //public async Task<List<PickDTO>>GetAsync()
        //{
        //    var picksLogic = new PicksLogic();
        //    var picks = await picksLogic.GetPicks();
        //    return picks;
        //}

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55)
            })
            .ToArray();
        }
    }
}
