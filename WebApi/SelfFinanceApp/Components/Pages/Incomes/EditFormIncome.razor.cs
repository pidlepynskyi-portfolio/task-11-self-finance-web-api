using InfrastructureApi.DTO;
using SelfFinanceApp.Components.Pages.BaseEditForm;

namespace SelfFinanceApp.Components.Pages.Incomes
{
    public partial class EditFormIncome : BaseEditFormItem<IncomeDTO>
    {
        List<TypeIncomesDTO>? typesIncomes;

        public EditFormIncome()
        {
            itemOrigin = new IncomeDTO();
            itemEdited = new IncomeDTO();
            editContext = new(itemEdited);
        }

        protected internal override async Task LoadData()
        {
            typesIncomes = await ApiCRUD.GetAll<TypeIncomesDTO>();
            await base.LoadData();
        }

        protected internal override void CopyItem()
        {
            itemEdited.Id = itemOrigin.Id;
            itemEdited.TypeId = itemOrigin.TypeId;
            itemEdited.Amount = itemOrigin.Amount;
            itemEdited.Comments = itemOrigin.Comments;
        }
    }
}
