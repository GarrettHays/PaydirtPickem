using Newtonsoft.Json;

namespace PaydirtPickem.Models
{ 
    public class Bookmaker
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("last_update")]
        public DateTime LastUpdate { get; set; }

        [JsonProperty("markets")]
        public List<Market> Markets { get; set; }
    }

    public class Market
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("outcomes")]
        public List<Outcome> Outcomes { get; set; }
    }

    public class Outcome
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public int Price { get; set; }

        [JsonProperty("point")]
        public double Point { get; set; }
    }

    public class OddsAPIResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("sport_key")]
        public string SportKey { get; set; }

        [JsonProperty("sport_title")]
        public string SportTitle { get; set; }

        [JsonProperty("commence_time")]
        public DateTime CommenceTime { get; set; }

        [JsonProperty("home_team")]
        public string HomeTeam { get; set; }

        [JsonProperty("away_team")]
        public string AwayTeam { get; set; }

        [JsonProperty("bookmakers")]
        public List<Bookmaker> Bookmakers { get; set; }
    }


}
