using System;
using System.Threading.Tasks;
using GymVod.Battleships.DataLayer.Model;
using GymVod.Battleships.Services.Tournaments;
using GymVod.Battleships.Web.Components.Pages.ViewModels;
using Microsoft.AspNetCore.Components;
using Sotsera.Blazor.Toaster;

namespace GymVod.Battleships.Web.Components.Pages
{
    public class TournamentNewComponent : ComponentBase
    {
        public TournamentNewVM Model { get; set; } = new TournamentNewVM();

        [Inject]
        public ITournamentService TournamentService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        protected IToaster Toaster { get; set; }

        public async Task HandleValidSubmitAsync()
        {
            var league = (League)Convert.ToInt32(Model.League);
            try
            {
                var games = await TournamentService.NewTournamentAsync(league);
                await TournamentService.InsertNewTournamentAsync(league, games);
            }
            catch (Exception e)
            {
                Toaster.Error(e.Message);
                NavigationManager.NavigateTo("/tournaments");
                return;
            }

            Toaster.Info("Turnaj byl odehrán.");
            NavigationManager.NavigateTo("/tournaments");
        }
    }
}
