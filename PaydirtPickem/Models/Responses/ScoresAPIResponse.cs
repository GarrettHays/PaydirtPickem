using Newtonsoft.Json;

namespace PaydirtPickem.Models.Responses
{
    public class ScoresAPIResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("sport_key")]
        public string SportKey { get; set; }

        [JsonProperty("sport_title")]
        public string SportTitle { get; set; }

        [JsonProperty("commence_time")]
        public DateTime? CommenceTime { get; set; }

        [JsonProperty("completed")]
        public bool? Completed { get; set; }

        [JsonProperty("home_team")]
        public string HomeTeam { get; set; }

        [JsonProperty("away_team")]
        public string AwayTeam { get; set; }

        [JsonProperty("scores")]
        public List<Score> Scores { get; set; }

        [JsonProperty("last_update")]
        public DateTime? LastUpdate { get; set; }
        

        public class Score
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("score")]
            public string TeamScore { get; set; }
        }


    }
}
