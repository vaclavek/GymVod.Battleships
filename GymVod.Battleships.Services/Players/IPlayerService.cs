using System.Collections.Generic;
using System.Threading.Tasks;
using GymVod.Battleships.Services.Players.ViewModels;

namespace GymVod.Battleships.Services.Players
{
    public interface IPlayerService
    {
        Task<List<PlayerListVM>> GetAllPlayersAsync();
        Task InsertNewPlayerAsync(PlayerVM player);
    }
}