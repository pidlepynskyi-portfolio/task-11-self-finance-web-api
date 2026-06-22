using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;
using System.Text.RegularExpressions;
using SelfFinanceApp.Services.RouteHistory;

namespace SelfFinanceApp.Components.Pages.BaseEditForm
{
    public partial class EditFormWrap
    {
        [Parameter]
        public int Id { get; set; }

        RenderFragment? _renderForm { set; get; }

        [Inject] RouteHistoryService RouteHistory { get; set; } = default!;
        [Inject] NavigationManager Navigation { get; set; } = default!;

        ErrorBoundary errorBoundary = default!;

        protected override void OnInitialized()
        {
            errorBoundary = new ErrorBoundary();
            _renderForm = RenderForm;
        }

        void GoToBack()
        {
            var prevPath = RouteHistory.GetPrevPath();
            Navigation.NavigateTo(Navigation.ToBaseRelativePath(prevPath));
        }

        string GetHeaderPageFromAction(string action, string headerPage)
        {
            switch (action)
            {
                case "add":
                    return $"Add {headerPage}";
                case "edit":
                    return $"Edit {headerPage}";
                default:
                    return Regex.Replace(action, @"^\w+", action[0].ToString().ToUpper());
            }
        }
    }
}
