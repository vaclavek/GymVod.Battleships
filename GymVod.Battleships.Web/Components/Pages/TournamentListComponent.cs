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
            Tournaments = await TournamentService.GetAllTournamentsListAsync();
        }

        protected string GetCssClass(TournamentListPlayerVM player)
        {
            if (player.Position == 0)
            {
                return "table-success";
            }
            if (player.Position == 1)
            {
                return "table-warning";
            }
            if (player.Position == 2)
            {
                return "table-danger";
            }
            return "table-primary";
        }
    }
}
