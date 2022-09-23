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

            return new List<PickDTO>
                {
                    new PickDTO
                    {
                    Id = 1,
                    HomeTeam = "New England Patriots",
                    AwayTeam = "Baltimore Ravens",
                    HomeTeamSpread = 3,
                    GameTime = new DateTime()
                    },
                    new PickDTO
                    {
                    Id = 2,
                    HomeTeam = "Miami Dolphins",
                    AwayTeam = "Buffalo Bills",
                    HomeTeamSpread = 5,
                    GameTime = new DateTime()
                    }
                };
                
            // var BASE_URL = "https://api.the-odds-api.com";

            // using var client = new HttpClient();

            // var resp = await client.GetAsync(BASE_URL + "/v4/sports/americanfootball_nfl/odds?apiKey=824e794d3659abd375e82e9cd361678a&regions=us&markets=spreads&oddFormat=american&bookmakers=draftkings");
            // var respString = await resp.Content.ReadAsStringAsync();
            // var odds = JsonConvert.DeserializeObject<List<Odd>>(respString);
            // var pickList = new List<PickDTO>();

            // foreach (var odd in odds)
            // {
            //     var pick = new PickDTO
            //     {
            //         HomeTeam = odd.HomeTeam,
            //         AwayTeam = odd.AwayTeam,
            //         HomeTeamSpread = odd.Bookmakers.FirstOrDefault()?.Markets.FirstOrDefault()?.Outcomes.FirstOrDefault(x => x.Name == odd.HomeTeam)?.Point,
            //         GameTime = odd.CommenceTime
            //     };
            //     pickList.Add(pick);
            // }

            // return pickList;
        }
    }
}