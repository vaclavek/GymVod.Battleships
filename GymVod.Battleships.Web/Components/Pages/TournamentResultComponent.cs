using System.Threading.Tasks;
using GymVod.Battleships.Services.Tournaments;
using GymVod.Battleships.Services.Tournaments.ViewModels;
using Microsoft.AspNetCore.Components;

namespace GymVod.Battleships.Web.Components.Pages
{
    public class TournamentResultComponent : ComponentBase
    {
        [Inject]
        public ITournamentService TournamentService { get; set; }

        [Parameter]
        public int TournamentId { get; set; }

        protected TournamentResultVM Tournament { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            Tournament = await TournamentService.GetTournamentResultByIdAsync(TournamentId);
        }
    }
}
