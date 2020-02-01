using System.Collections.Generic;
using System.Threading.Tasks;
using GymVod.Battleships.Services.Players;
using GymVod.Battleships.Services.Players.ViewModels;
using Microsoft.AspNetCore.Components;

namespace GymVod.Battleships.Web.Components.Pages
{
    public class PlayersListComponent : ComponentBase
    {
        [Inject]
        public IPlayerService PlayerService { get; set; }

        protected List<PlayerListVM> Players { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            Players = await PlayerService.GetAllPlayersAsync();
        }
    }
}