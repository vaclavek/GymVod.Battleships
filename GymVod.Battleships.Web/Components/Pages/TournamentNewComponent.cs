using System;
using System.Threading.Tasks;
using GymVod.Battleships.DataLayer.Model;
using GymVod.Battleships.Services.Tournaments;
using GymVod.Battleships.Web.Components.Pages.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
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

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        public bool IsDisabledButton { get; set; }

        public async Task HandleValidSubmitAsync()
        {
            IsDisabledButton = true;
            StateHasChanged();

            var league = (League)Convert.ToInt32(Model.League);

            TournamentService.NewGamePlayed += NewGamePlayedEvent;
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

        private void NewGamePlayedEvent(object sender, RunGamesEventArgs e)
        {
            var notificationText = $"Odehráno {e.GamesRunCount} ze {e.GamesTotalCount} her.";

            JSRuntime.InvokeVoidAsync("setNotification", notificationText);
        }
    }
}
