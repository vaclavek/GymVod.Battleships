using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blazorise;
using GymVod.Battleships.Services.Players;
using GymVod.Battleships.Services.Players.ViewModels;
using Microsoft.AspNetCore.Components;
using Sotsera.Blazor.Toaster;

namespace GymVod.Battleships.Web.Components.Pages
{
    public class PlayersListComponent : ComponentBase
    {
        [Inject]
        public IPlayerService PlayerService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        protected IToaster Toaster { get; set; }

        protected Modal modalRef;

        protected List<PlayerListVM> Players { get; private set; }

        protected int? DeletePlayerId { get; set; }

        protected string DeletePassword { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            Players = await PlayerService.GetAllPlayersAsync();
            await base.OnParametersSetAsync();
        }

        protected void ShowDeleteModal(int playerId)
        {
            DeletePlayerId = playerId;
            modalRef.Show();
        }

        protected void HideModal()
        {
            modalRef.Hide();
        }

        protected async Task DeletePlayerAsync()
        {
            if (string.IsNullOrEmpty(DeletePassword))
            {
                Toaster.Error("Zadejte prosím heslo pro smazání.");
                return;
            }

            modalRef.Hide();

            if (DeletePlayerId == null)
            {
                return;
            }

            try
            {
                await PlayerService.DeleteAsync(DeletePlayerId.Value, DeletePassword);
            }
            catch (Exception e)
            {
                Toaster.Error(e.Message);
                return;
            }


            DeletePlayerId = null;

            Toaster.Info("Hráč byl smazán.");

            await OnParametersSetAsync();
        }
    }
}