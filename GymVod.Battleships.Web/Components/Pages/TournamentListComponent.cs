using System.Collections.Generic;
using System.Threading.Tasks;
using GymVod.Battleships.Services.Tournaments;
using GymVod.Battleships.Services.Tournaments.ViewModels;
using Microsoft.AspNetCore.Components;

namespace GymVod.Battleships.Web.Components.Pages
{
    public class TournamentListComponent : ComponentBase
    {
        [Inject]
        public ITournamentService TournamentService { get; set; }

        protected List<TournamentListVM> Tournaments { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            Tournaments = await TournamentService.GetAllTournamentsAsync();
        }
    }
}
