using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymVod.Battleships.Common;
using GymVod.Battleships.DataLayer;
using GymVod.Battleships.DataLayer.Model;
using GymVod.Battleships.DataLayer.Repositories;
using GymVod.Battleships.DataLayer.UnitOfWorks;
using GymVod.Battleships.Services.GameServer;
using GymVod.Battleships.Services.Players;
using GymVod.Battleships.Services.Tournaments.ViewModels;
using Game = GymVod.Battleships.Services.GameServer.Game;

namespace GymVod.Battleships.Services.Tournaments
{
    public class TournamentService : ITournamentService
    {
        private readonly ITournamentRepository tournamentRepository;
        private readonly IPlayerRepository playerRepository;
        private readonly IPluginLoader pluginLoader;
        private readonly IUnitOfWork unitOfWork;
        private readonly GameSettings gameSettings = new GameSettings(20, 20, new ShipType[]
        {
            ShipType.Carrier,                       // 1x
            ShipType.Battleship,                    // 1x
            ShipType.Cruiser, ShipType.Cruiser,     // 2x
            ShipType.Destroyer, ShipType.Destroyer, // 2x
            ShipType.Submarine, ShipType.Submarine, // 2x
        });


        public TournamentService(ITournamentRepository tournamentRepository,
            IPlayerRepository playerRepository,
            IPluginLoader pluginLoader,
            IUnitOfWork unitOfWork)
        {
            this.tournamentRepository = tournamentRepository;
            this.playerRepository = playerRepository;
            this.pluginLoader = pluginLoader;
            this.unitOfWork = unitOfWork;
        }

        public async Task<List<TournamentListVM>> GetAllTournamentsAsync()
        {
            var tournaments = await tournamentRepository.GetAllTournamentsAsync();
            return tournaments.Select(x => new TournamentListVM
            {
                Created = x.Created,
                League = x.League,
                Games = new List<TournamentGameListVM>(x.Games.Select(y => new TournamentGameListVM
                {
                    Player1Name = y.Player1.Name,
                    Player2Name = y.Player2.Name,
                    RoundsCount = y.RoundsCount,
                    WhichPlayerWins = y.WhichPlayerWins,
                    ErrorMessage = y.ErrorMessage
                }))
            }).ToList();
        }

        public async Task<List<Game>> NewTournamentAsync(League league)
        {
            var competitors = await GetCompetitorsAsync(league);
            var games = GetGames(competitors);

            RunGames(games);
            return games;
        }

        public async Task InsertNewTournamentAsync(League league, List<Game> games)
        {
            var tournament = new Tournament
            {
                League = league
            };
            foreach (var game in games)
            {
                tournament.Games.Add(new DataLayer.Model.Game
                {
                    Player1Id = game.Player1Id,
                    Player2Id = game.Player2Id,
                    RoundsCount = game.RoundsCount,
                    WhichPlayerWins = game.WhichPlayerWins,
                    ErrorMessage = game.ErrorMessage
                });
            }

            unitOfWork.AddForInsert(tournament);
            await unitOfWork.CommitAsync();
        }

        private async Task<Dictionary<int, IBattleshipsGame>> GetCompetitorsAsync(League league)
        {
            var players = (await playerRepository.GetAllPlayersAsync())
                            .Where(x => x.League == league)
                            .ToDictionary(x => x.FileGuid);

            var competitors = new Dictionary<int, IBattleshipsGame>();
            foreach (var player in players)
            {
                var pluginAssembly = pluginLoader.LoadPlugin(player.Key);
                var instance = pluginLoader.GetInstance(pluginAssembly);
                competitors.Add(player.Value.Id, instance);
            }

            return competitors;
        }

        private List<Game> GetGames(Dictionary<int, IBattleshipsGame> competitors)
        {
            var games = new List<Game>();
            foreach (var competitor1 in competitors)
            {
                foreach (var competitor2 in competitors)
                {
                    if (competitor1.Key != competitor2.Key)
                    {
                        for (int i = 0; i < 10; i++) // games repeat
                        {
                            games.Add(new Game
                            {
                                Player1 = competitor1.Value,
                                Player2 = competitor2.Value,
                                Player1Id = competitor1.Key,
                                Player2Id = competitor2.Key,
                                GameSettings = gameSettings
                            });
                        }
                    }
                }
            }

            return games;
        }

        private static void RunGames(List<Game> games)
        {
            foreach (var game in games)
            {
                var ge = new GameEngine(game);
                ge.InitGame();

                if (game.WhichPlayerWins == WhichPlayerWins.NotFinished) // jinak chyba inicializace - někdo špatně rozmístil lodě
                {
                    ge.PlayGame();
                }
            }
        }
    }
}
