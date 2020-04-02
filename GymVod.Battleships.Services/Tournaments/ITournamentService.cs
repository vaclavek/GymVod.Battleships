using System.Collections.Generic;
using System.Threading.Tasks;
using GymVod.Battleships.DataLayer.Model;
using GymVod.Battleships.Services.Tournaments.ViewModels;

namespace GymVod.Battleships.Services.Tournaments
{
    public interface ITournamentService
    {
        Task<List<TournamentListVM>> GetAllTournamentsListAsync();
        Task<TournamentResultVM> GetTournamentResultByIdAsync(int tournamentId);
        Task<List<GameServer.Game>> NewTournamentAsync(League league);
        Task InsertNewTournamentAsync(League league, List<GameServer.Game> games);
    }
}