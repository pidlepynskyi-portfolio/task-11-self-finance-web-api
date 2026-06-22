using InfrastructureApi.DTO;
using Microsoft.AspNetCore.Components;
using SelfFinanceApp.Services.ViewModelServices;

namespace SelfFinanceApp.Common
{
    abstract public class BaseEntityPage<TEntityDTO> : ComponentBase 
        where TEntityDTO : BaseEntityDTO 
    {
        protected string namePageController;

        protected string? uriAddPageItem;
        protected string? uriBaseEditPageItem;

        protected List<TEntityDTO>? listEntity;

        [Parameter]
        public int CurrentPage { get; set; }

        [Inject] private EntitiesService ApiCRUD { get; set; } = default!;

        public BaseEntityPage()
        {
            namePageController = this.GetType().Name
                .LowerFirstChar();
        }

        protected override async Task OnInitializedAsync()
        {
            uriAddPageItem = $"{namePageController}/add";
            uriBaseEditPageItem = namePageController + "/edit/{0}";

            await LoadData();
        }

        protected async Task LoadData()
        {
            listEntity = await ApiCRUD.GetAll<TEntityDTO>();

            if (listEntity != null || listEntity!.Count != 0)
            {
                listEntity = listEntity.OrderByDescending(i => i.Id).ToList();
            }
        }
    }
}
