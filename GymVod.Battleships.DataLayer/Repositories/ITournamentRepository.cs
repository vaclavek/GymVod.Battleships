using System.Collections.Generic;
using System.Threading.Tasks;
using GymVod.Battleships.DataLayer.Model;

namespace GymVod.Battleships.DataLayer.Repositories
{
    public interface ITournamentRepository
    {
        Task<List<Tournament>> GetAllTournamentsAsync();
    }
}