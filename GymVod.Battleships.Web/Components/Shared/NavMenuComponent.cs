using Microsoft.AspNetCore.Components;

namespace GymVod.Battleships.Web.Components.Shared
{
    public class NavMenuComponent : ComponentBase
    {
        private bool collapseNavMenu = true;

        public string NavMenuCssClass => collapseNavMenu ? "collapse" : null;

        public void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }
    }
}
