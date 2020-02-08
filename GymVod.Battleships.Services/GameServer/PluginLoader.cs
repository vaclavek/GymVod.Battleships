using System.Linq;
using System.Threading.Tasks;
using GymVod.Battleships.DataLayer.Repositories;

namespace GymVod.Battleships.Services.GameServer
{
    public class PluginLoader
    {
        private readonly IPlayerRepository playerRepository;

        public PluginLoader(IPlayerRepository playerRepository)
        {
            this.playerRepository = playerRepository;
        }

        public async Task LoadPluginsAsync()
        {
            var players = await playerRepository.GetAllPlayersAsync();
            players.Select(x => x.FileGuid);
        }
    }
}
