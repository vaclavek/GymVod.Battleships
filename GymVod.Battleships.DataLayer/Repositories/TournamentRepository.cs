using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymVod.Battleships.DataLayer.Model;
using Microsoft.EntityFrameworkCore;

namespace GymVod.Battleships.DataLayer.Repositories
{
    public class TournamentRepository : ITournamentRepository
    {
        private readonly MyContext dbContext;

        public TournamentRepository(MyContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<Tournament> GetTournamentByIdAsync(int tournamentId)
        {
            return dbContext.Tournaments
                .Where(x => x.Id == tournamentId)
                .Include(x => x.Games)
                .ThenInclude(game => game.Player1)
                .Include(x => x.Games)
                .ThenInclude(game => game.Player2)
                .SingleAsync();
        }

        public Task<List<Tournament>> GetAllTournamentsAsync()
        {
            return dbContext.Tournaments
                .Include(x => x.Games)
                .ThenInclude(game => game.Player1)
                .Include(x => x.Games)
                .ThenInclude(game => game.Player2)
                .OrderByDescending(x => x.Created)
                .ToListAsync();
        }
    }
}
