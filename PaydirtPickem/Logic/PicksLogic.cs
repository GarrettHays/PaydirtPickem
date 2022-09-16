using Newtonsoft.Json;
using PaydirtPickem.Models;

namespace PaydirtPickem.Logic
{
    public class PicksLogic
    {
        public PicksLogic()
        {

        }

        public async Task<List<PickDTO>> GetPicks()
        {
            var pickList = new List<PickDTO>()
            {
                new PickDTO 
                { 
                    HomeTeam = "New York Giants", 
                    AwayTeam = "Carolina Panthers", 
                    HomeTeamSpread = -1.5, 
                    GameTime = DateTime.Now 
                } 
            };
            Console.WriteLine("getting them picks!");
            //var BASE_URL = "https://api.the-odds-api.com";

            //using var client = new HttpClient();

            //var resp = await client.GetStringAsync(BASE_URL + "/v4/sports/americanfootball_nfl/odds?apiKey=824e794d3659abd375e82e9cd361678a&regions=us&markets=spreads&oddFormat=american&bookmakers=draftkings");
            //var odds = JsonConvert.DeserializeObject<List<OddsAPIResponse>>(resp);

            //var pickList = new List<PickDTO>();

            //foreach(var odd in odds)
            //{
            //    var pick = new PickDTO
            //    {
            //        HomeTeam = odd.HomeTeam,
            //        AwayTeam = odd.AwayTeam,
            //        HomeTeamSpread = odd.Bookmakers.FirstOrDefault()?.Markets.FirstOrDefault()?.Outcomes.FirstOrDefault(x => x.Name == odd.HomeTeam)?.Point,
            //        GameTime = odd.CommenceTime
            //    };
            //    pickList.Add(pick);
            //} 

            return pickList;
        }
    }
}