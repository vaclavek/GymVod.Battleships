using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BlazorInputFile;
using GymVod.Battleships.DataLayer.Model;
using GymVod.Battleships.Services;
using GymVod.Battleships.Services.Players;
using GymVod.Battleships.Services.Players.ViewModels;
using GymVod.Battleships.Web.Components.Pages.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Sotsera.Blazor.Toaster;

namespace GymVod.Battleships.Web.Components.Pages
{
    public class PlayersNewComponent : ComponentBase
    {
        [Inject]
        public IFileUploadService FileUploadService { get; set; }

        [Inject]
        public IPlayerService PlayerService { get; set; }

        [Inject]
        public IPluginTester PluginTester { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        protected IToaster Toaster { get; set; }

        protected PlayerNewVM Model = new PlayerNewVM();
        protected bool FileInserted => file != null;

        private IFileListEntry file;
        protected EditContext _editContext;

        protected string OkayDisabled { get; set; } = "disabled";

        protected override void OnInitialized()
        {
            base.OnInitialized();

            _editContext = new EditContext(Model);
            _editContext.OnFieldChanged += EditContext_OnFieldChanged;
        }

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            SetOkDisabledStatus();
        }

        public void HandleFileSelected(IFileListEntry[] files)
        {
            file = files.FirstOrDefault();
            SetOkDisabledStatus();
        }

        public async Task HandleValidSubmit()
        {
            byte[] fileData;
            using (var ms = new MemoryStream())
            {
                await file.Data.CopyToAsync(ms);
                fileData = ms.ToArray();
            }

            try
            {
                PluginTester.TestImplementation(Assembly.Load(fileData));
            }
            catch (Exception e)
            {
                Toaster.Error(e.Message, "Chyba implementace");
                return;
            }

            await FileUploadService.UploadAsync(file, Model.FileGuid);

            await PlayerService.InsertNewPlayerAsync(new PlayerVM
            {
                Name = Model.Name,
                Password = Model.Password,
                FileGuid = Model.FileGuid,
                League = (League)Convert.ToInt32(Model.League)
            });

            Toaster.Info("Nový hráč vložen.");
            NavigationManager.NavigateTo("/players-list");
        }

        private void EditContext_OnFieldChanged(object sender, FieldChangedEventArgs e)
        {
            SetOkDisabledStatus();
        }

        private void SetOkDisabledStatus()
        {
            if (_editContext.Validate() && FileInserted)
            {
                OkayDisabled = null;
            }
            else
            {
                OkayDisabled = "disabled";
            }
        }
    }
}