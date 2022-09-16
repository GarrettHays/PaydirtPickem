using Newtonsoft.Json;
using PaydirtPickem.Models;

namespace PaydirtPickem.Logic
{
    public class OddsLogic
    {
        public OddsLogic()
        {

        }

        public async Task<List<OddsAPIResponse>> GetOdds()
        {
            var BASE_URL = "https://api.the-odds-api.com";

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-api-key", "824e794d3659abd375e82e9cd361678a");

            var resp = await client.GetStringAsync(BASE_URL + "/v4/sports/americanfootball_nfl/odds/?apiKey=824e794d3659abd375e82e9cd361678a&regions=us&markets=spreads&oddFormat=american");
            var odds = JsonConvert.DeserializeObject<List<OddsAPIResponse>>(resp);

            return odds;
        }
    }
}