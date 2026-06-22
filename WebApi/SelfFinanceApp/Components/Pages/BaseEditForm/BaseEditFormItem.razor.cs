using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;
using SelfFinanceApp.Services.RouteHistory;
using InfrastructureApi.DTO;
using SelfFinanceApp.Services.ViewModelServices;
using MudBlazor;

namespace SelfFinanceApp.Components.Pages.BaseEditForm
{
    public abstract partial class BaseEditFormItem<TItem> where TItem : BaseEntityDTO
    {
        [Parameter]
        public int? Id { get; set; }
        [Parameter]
        public string HeaderPage { set; get; } = "Edit";

        [Inject] protected EntitiesService ApiCRUD { get; set; } = default!;
        [Inject] private NavigationManager Navigation { get; set; } = default!;
        [Inject] private RouteHistoryService RouteHistory { get; set; } = default!;
        
        [Inject] private IDialogService DialogService { get; set; } = default!;
        [Inject] private ISnackbar Snackbar { get; set; } = default!; 
        

        protected EditContext? editContext;
        protected TItem itemOrigin = default!;
        protected TItem itemEdited = default!;

        private RenderFragment? _renderInputComponetsForm { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
            _renderInputComponetsForm = RenderInputComponentsForm;
        }

        protected internal abstract void RenderInputComponentsForm(RenderTreeBuilder __builder);
        protected internal abstract void CopyItem();

        protected internal virtual async Task LoadData()
        {
            if (Id is not null && Id != 0)
            {
                itemOrigin = await ApiCRUD.GetById<TItem>(Id.Value);
                CopyItem();
            }
        }

        protected internal async Task Submit()
        {
            if (editContext == null || !editContext.Validate())
            {
                return;
            }

            if (await ShowModal() == null)
            {
                Snackbar.Add("Canceled operation!", Severity.Info);
                return;
            }

            if (Id is null || Id == 0)
            {
                await ApiCRUD.PostItem<TItem>(itemEdited);
                Snackbar.Add("Added success!", Severity.Success);
            }
            else
            {
                await ApiCRUD.PutOrDeleteItem<TItem>(itemEdited);
                Snackbar.Add("Edited success!", Severity.Success);
            }

            GoToBack();
        }

        protected void GoToBack()
        {
            var prevPath = RouteHistory.GetPrevPath();
            Navigation.NavigateTo(Navigation.ToBaseRelativePath(prevPath));
        }

        async Task<bool?> ShowModal()
        {
            bool? result = false;

            if (Id is null || Id == 0)
            {
                result = await DialogService.ShowMessageBox(
                "Add item",
                "Please confirm adding data",
                yesText: "ADD", cancelText: "Cancel");
            }
            else
            {
                result = await DialogService.ShowMessageBox(
                "Edit item",
                "Please confirm the data changes",
                yesText: "EDIT", cancelText: "Cancel");
            }

            return result;
        }
    }
}
