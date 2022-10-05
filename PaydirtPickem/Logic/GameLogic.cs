using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PaydirtPickem.Data;
using PaydirtPickem.Models;
using PaydirtPickem.Models.Responses;
using System;
using System.Xml.Linq;
using static PaydirtPickem.Models.Responses.ScoresAPIResponse;

namespace PaydirtPickem.Logic
{
    public class GameLogic
    {
        public GameLogic()
        {

        }

        public async Task<List<Game>> GetGames()
        {
            var BASE_URL = "https://api.the-odds-api.com";

            using var client = new HttpClient();

            var sport = "americanfootball_nfl";
            var apiKey = "824e794d3659abd375e82e9cd361678a";
            var region = "us";
            var market = "spreads";
            var format = "american";
            var bookie = "draftkings";

            var resp = await client.GetAsync(BASE_URL + $"/v4/sports/{sport}/odds?apiKey={apiKey}&regions={region}&markets={market}&oddFormat={format}&bookmakers={bookie}");
            var respString = await resp.Content.ReadAsStringAsync();
            var odds = JsonConvert.DeserializeObject<List<Odd>>(respString);
            var gameList = new List<Game>();

            foreach (var odd in odds)
            {
                var game = new Game
                {
                    Id = Guid.Parse(odd.Id),
                    HomeTeam = odd.HomeTeam,
                    AwayTeam = odd.AwayTeam,
                    HomeTeamSpread = odd.Bookmakers.FirstOrDefault()?.Markets.FirstOrDefault()?.Outcomes.FirstOrDefault(x => x.Name == odd.HomeTeam)?.Point,
                    GameTime = odd.CommenceTime
                };
                gameList.Add(game);
            }

            return gameList;
        }

        public async Task GetScores(int daysFrom, PaydirtPickemDbContext _context)
        {
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                var BASE_URL = "https://api.the-odds-api.com";

                using var client = new HttpClient();

                var sport = "americanfootball_nfl";
                var apiKey = "824e794d3659abd375e82e9cd361678a";

                //var resp = await client.GetAsync(BASE_URL + $"/v4/sports/{sport}/scores/?apiKey={apiKey}&daysFrom={daysFrom}");
                //var respString = await resp.Content.ReadAsStringAsync();
                //var scores = JsonConvert.DeserializeObject<List<ScoresAPIResponse>>(respString);

                var scores = CreateScoresList();


                foreach (var score in scores.Where(x => x.Completed.HasValue && x.Completed.Value && x.CommenceTime.HasValue))
                {
                    var gameWeek = GetWeekNumberForGame(score.CommenceTime.Value);

                    var gameToScore = _context.Games.FirstOrDefault(x => x.WeekNumber == gameWeek && x.HomeTeam == score.HomeTeam && x.AwayTeam == score.AwayTeam && (!x.AwayTeamScore.HasValue || !x.HomeTeamScore.HasValue));
                    if (gameToScore != null)
                    {
                        gameToScore.AwayTeamScore = int.Parse(score.Scores.FirstOrDefault(x => x.Name == score.AwayTeam)?.TeamScore);
                        gameToScore.HomeTeamScore = int.Parse(score.Scores.FirstOrDefault(x => x.Name == score.HomeTeam)?.TeamScore);
                        _context.Entry(gameToScore).State = EntityState.Modified;


                        var winningTeam = "";
                        //math to calculate home team score against away team score after factoring the spread
                        if ((gameToScore.HomeTeamScore + gameToScore.HomeTeamSpread) > gameToScore.AwayTeamScore)
                        {
                            //home team won
                            winningTeam = gameToScore.HomeTeam;
                        }
                        else
                        {
                            //away team won
                            winningTeam = gameToScore.AwayTeam;
                        }

                        var userPicksToUpdate = _context.UserPicks.Where(x => x.GameId == gameToScore.Id);
                        //foreach users by winner and game id - then update winner
                        foreach (var user in userPicksToUpdate)
                        {
                            user.CorrectPick = user.PickedTeam == winningTeam;
                            _context.Entry(user).State = EntityState.Modified;


                            var userWeekScore = _context.UserWeekScores.FirstOrDefault(x => x.WeekNumber == gameWeek && x.UserId == user.UserId);
                            var userSeasonScore = _context.UserSeasonScores.FirstOrDefault(x => x.UserId == user.UserId);
                            if (userWeekScore == null)
                            {
                                userWeekScore = new UserWeekScore
                                {

                                    WeekNumber = gameWeek.Value,
                                    UserId = user.UserId,
                                    WeekTotalWin = 0,
                                    WeekTotalLoss = 0
                                };
                                _context.UserWeekScores.Add(userWeekScore);
                                await _context.SaveChangesAsync();

                            }

                            if (userSeasonScore == null)
                            {
                                userSeasonScore = new UserSeasonScore
                                {

                                    UserId = user.UserId,
                                    SeasonTotalWin = 0,
                                    SeasonTotalLoss = 0
                                };
                                _context.UserSeasonScores.Add(userSeasonScore);
                                await _context.SaveChangesAsync();

                            }
                            if (user.CorrectPick.Value)
                            {
                                userWeekScore.WeekTotalWin++;
                                userSeasonScore.SeasonTotalWin++;
                                _context.Entry(userWeekScore).State = EntityState.Modified;
                                _context.Entry(userSeasonScore).State = EntityState.Modified;

                            }
                            else
                            {
                                userWeekScore.WeekTotalLoss++;
                                userSeasonScore.SeasonTotalLoss++;
                                _context.Entry(userWeekScore).State = EntityState.Modified;
                                _context.Entry(userSeasonScore).State = EntityState.Modified;

                            }
                        }
                    }
                }
                await _context.SaveChangesAsync();
                dbContextTransaction.Commit();
            }
        }

        public List<ScoresAPIResponse> CreateScoresList()
        {
            var scoreDict = new Dictionary<string, string>()
            {
                { "Denver Broncos" , "Indianapolis Colts" },
                { "Green Bay Packers" , "New York Giants" },
                { "New Orleans Saints" , "Seattle Seahawks" },
                { "New York Jets" , "Miami Dolphins" },
                { "New England Patriots" , "Detroit Lions" },
                { "Cleveland Browns" , "Los Angeles Chargers" },
                { "Washington Commanders" , "Tennessee Titans" },
                { "Tampa Bay Buccaneers" , "Atlanta Falcons" },
                { "Buffalo Bills" , "Pittsburgh Steelers" },
                { "Minnesota Vikings" , "Chicago Bears" },
                { "Jacksonville Jaguars" , "Houston Texans" },
                { "Carolina Panthers" , "San Francisco 49ers" },
                { "Arizona Cardinals" , "Philadelphia Eagles" },
                { "Los Angeles Rams" , "Dallas Cowboys" },
                { "Baltimore Ravens" , "Cincinnati Bengals" },
                { "Kansas City Chiefs" , "Las Vegas Raiders" }
            };

            var resp = new List<ScoresAPIResponse>();

            foreach (var score in scoreDict)
            {
                resp.Add(CreateScoresResponse(score.Key, score.Value));
            }

            return resp;
        }

        public ScoresAPIResponse CreateScoresResponse(string homeTeam, string awayTeam)
        {
            var rnd = new Random();
            var htScore = rnd.Next(7, 42);
            var atScore = rnd.Next(7, 42);
            return new ScoresAPIResponse
            {
                CommenceTime = DateTime.UtcNow,
                Completed = true,
                HomeTeam = homeTeam,
                AwayTeam = awayTeam,
                Scores = new List<Score>
                {
                    new Score
                    {
                        Name = homeTeam,
                        TeamScore = htScore.ToString()
                    },
                    new Score
                    {
                        Name = awayTeam,
                        TeamScore = atScore.ToString()
                    }
                }
            };
        }

        public void CheckForInactiveGames(PaydirtPickemDbContext _context, int currentWeek)
        {
            var inactiveGames = _context.Games.Where(x => x.IsActive && currentWeek != x.WeekNumber).ToList();
            foreach (var game in inactiveGames)
            {
                game.IsActive = false;
                _context.Entry(game).State = EntityState.Modified;
            }
        }

        public int? GetWeekNumberForGame(DateTime gameTime)
        {
            if (gameTime > DateTime.Parse("2022-09-08 00:00:00.0000000") && gameTime < DateTime.Parse("2022-09-13 23:59:59.0000000"))
            {
                return 1;
            }
            else if (gameTime > DateTime.Parse("2022-09-14 00:00:00.0000000") && gameTime < DateTime.Parse("2022-09-20 23:59:59.0000000"))
            {
                return 2;
            }
            else if (gameTime > DateTime.Parse("2022-09-21 00:00:00.0000000") && gameTime < DateTime.Parse("2022-09-27 23:59:59.0000000"))
            {
                return 3;
            }
            else if (gameTime > DateTime.Parse("2022-09-28 00:00:00.0000000") && gameTime < DateTime.Parse("2022-10-04 23:59:59.0000000"))
            {
                return 4;
            }
            else if (gameTime > DateTime.Parse("2022-10-05 00:00:00.0000000") && gameTime < DateTime.Parse("2022-10-11 23:59:59.0000000"))
            {
                return 5;
            }
            else if (gameTime > DateTime.Parse("2022-10-12 00:00:00.0000000") && gameTime < DateTime.Parse("2022-10-18 23:59:59.0000000"))
            {
                return 6;
            }
            else if (gameTime > DateTime.Parse("2022-10-19 00:00:00.0000000") && gameTime < DateTime.Parse("2022-10-25 23:59:59.0000000"))
            {
                return 7;
            }
            else if (gameTime > DateTime.Parse("2022-10-26 00:00:00.0000000") && gameTime < DateTime.Parse("2022-11-01 23:59:59.0000000"))
            {
                return 8;
            }
            else if (gameTime > DateTime.Parse("2022-11-02 00:00:00.0000000") && gameTime < DateTime.Parse("2022-11-08 23:59:59.0000000"))
            {
                return 9;
            }
            else if (gameTime > DateTime.Parse("2022-11-09 00:00:00.0000000") && gameTime < DateTime.Parse("2022-11-15 23:59:59.0000000"))
            {
                return 10;
            }
            else if (gameTime > DateTime.Parse("2022-11-16 00:00:00.0000000") && gameTime < DateTime.Parse("2022-11-22 23:59:59.0000000"))
            {
                return 11;
            }
            else if (gameTime > DateTime.Parse("2022-11-23 00:00:00.0000000") && gameTime < DateTime.Parse("2022-11-29 23:59:59.0000000"))
            {
                return 12;
            }
            else if (gameTime > DateTime.Parse("2022-11-30 00:00:00.0000000") && gameTime < DateTime.Parse("2022-12-06 23:59:59.0000000"))
            {
                return 13;
            }
            else if (gameTime > DateTime.Parse("2022-12-07 00:00:00.0000000") && gameTime < DateTime.Parse("2022-12-13 23:59:59.0000000"))
            {
                return 14;
            }
            else if (gameTime > DateTime.Parse("2022-12-14 00:00:00.0000000") && gameTime < DateTime.Parse("2022-12-20 23:59:59.0000000"))
            {
                return 15;
            }
            else if (gameTime > DateTime.Parse("2022-12-21 00:00:00.0000000") && gameTime < DateTime.Parse("2022-12-27 23:59:59.0000000"))
            {
                return 16;
            }
            else if (gameTime > DateTime.Parse("2022-12-28 00:00:00.0000000") && gameTime < DateTime.Parse("2023-01-03 23:59:59.0000000"))
            {
                return 17;
            }
            else if (gameTime > DateTime.Parse("2023-01-04 00:00:00.0000000") && gameTime < DateTime.Parse("2023-01-11 23:59:59.0000000"))
            {
                return 18;
            }
            else
            {
                return null;
            }
        }
    }
}