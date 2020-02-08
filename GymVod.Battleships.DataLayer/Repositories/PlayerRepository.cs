using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymVod.Battleships.DataLayer.Model;
using Microsoft.EntityFrameworkCore;

namespace GymVod.Battleships.DataLayer.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly MyContext dbContext;

        public PlayerRepository(MyContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<List<Player>> GetAllPlayersAsync()
        {
            return dbContext.Players.OrderByDescending(x => x.Created).ToListAsync();
        }

        public async Task<Player> GetPlayerAsync(int playerId)
        {
            return await dbContext.Players.FindAsync(playerId);
        }
    }
}
