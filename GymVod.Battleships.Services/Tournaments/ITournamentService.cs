using System.Collections.Generic;
using System.Threading.Tasks;
using GymVod.Battleships.DataLayer.Model;
using GymVod.Battleships.Services.Tournaments.ViewModels;

namespace GymVod.Battleships.Services.Tournaments
{
    public interface ITournamentService
    {
        Task<List<TournamentListVM>> GetAllTournamentsAsync();
        Task<List<GameServer.Game>> NewTournamentAsync(League league);
        Task InsertNewTournament(League league, List<GameServer.Game> games);
    }
}