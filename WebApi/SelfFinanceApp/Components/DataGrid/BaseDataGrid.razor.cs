using InfrastructureApi.DTO;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SelfFinanceApp.Exceptions;
using SelfFinanceApp.Services.ViewModelServices;
using SelfFinanceApp.Services.RouteHistory;

namespace SelfFinanceApp.Components.DataGrid
{
    public partial class BaseDataGrid<TDataItem> where TDataItem : BaseEntityDTO
    {
        [Parameter, EditorRequired]
        public List<TDataItem> Items { get; set; } = default!;

        [Parameter]
        public string PageTitle { get; set; } = default!;

        [Parameter, EditorRequired]
        public Func<TDataItem, string> UriEditPageItem { get; set; } = default!;

        [Parameter, EditorRequired]
        public string UriAddPageItem { get; set; } = default!;

        [Parameter, EditorRequired]
        public RenderFragment? HeaderColumnsTemplate { get; set; }

        [Parameter, EditorRequired]
        public RenderFragment<TDataItem> ColumnsTemplate { get; set; } = default!;

        private int selectedRowNumber = -1;
        private MudTable<TDataItem> _mudTable = default!;
        private bool btnsCrudDisabled = true;
        private TDataItem? _selectedItem;

        [Inject] private NavigationManager Navigation { get; set; } = default!;
        [Inject] private RouteHistoryService RouteHistory { get; set; } = default!;
        [Inject] private EntitiesService ApiCRUD { get; set; } = default!;
        [Inject] private IDialogService DialogService { get; set; } = default!;
        [Inject] private ISnackbar Snackbar { get; set; } = default!;

        private void UpdateRouteHistory()
        {
            RouteHistory.Clear();
            RouteHistory.AddRoute(Navigation.Uri);
        }

        private void GoToEdit(TDataItem item, Func<TDataItem, string> getUri)
        {
            UpdateRouteHistory();
            string uri = getUri(item);
            Navigation.NavigateTo(uri);
        }

        private void GoToCreate()
        {
            UpdateRouteHistory();
            Navigation.NavigateTo(UriAddPageItem);
        }

        private async Task DeleteSelectedItem(TDataItem? selectedItem)
        {
            if (selectedItem is null)
            {
                throw new ArgumentNullException(nameof(selectedItem));
            }

            await ApiCRUD.PutOrDeleteItem(selectedItem);
        }

        private void RowClickEvent(TableRowClickEventArgs<TDataItem> tableRowClickEventArgs)
        {
            if (_selectedItem != null && tableRowClickEventArgs.Item!.Equals(_selectedItem))
            {
                _selectedItem = null;
                btnsCrudDisabled = true;
            }
            else
            {
                _selectedItem = tableRowClickEventArgs.Item;
                btnsCrudDisabled = false;
            }
        }

        private string SelectedRowClassFunc(TDataItem element, int rowNumber)
        {
            if (selectedRowNumber == rowNumber)
            {
                selectedRowNumber = -1;
                return string.Empty;
            }
            else if (_mudTable.SelectedItem != null && _mudTable.SelectedItem.Equals(element))
            {
                selectedRowNumber = rowNumber;
                return "selected";
            }
            else
            {
                return string.Empty;
            }
        }

        private async void OnDelete()
        {
            if (await ShowDialogDelete() == null)
            {
                Snackbar.Add("Canceled deleting!", Severity.Info);
            }
            else
            {
                try
                {
                    await DeleteSelectedItem(_mudTable.SelectedItem);
                    Snackbar.Add("Deleted successed!");
                }
                catch (SelfFinanceApiException ex)
                {
                    if ((int)ex.StatusCode != 404)
                    {
                        throw;
                    }

                    Snackbar.Add($"Status code: {(int)ex.StatusCode} - {ex.Message}", Severity.Error);
                }

                Items?.Remove(_mudTable.SelectedItem!);
                btnsCrudDisabled = true;
            }

            StateHasChanged();
        }

        private async Task<bool?> ShowDialogDelete()
        {
            bool? result = await DialogService.ShowMessageBox(
                "Delet item",
                "Please confirm deletion of data",
                yesText: "DELETE", cancelText: "Cancel");

            return result;
        }
    }
}
