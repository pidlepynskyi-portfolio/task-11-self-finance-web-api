using InfrastructureApi.DTO;
using SelfFinanceApp.Components.Pages.BaseEditForm;

namespace SelfFinanceApp.Components.Pages.Expenses
{
    public partial class EditFormExpense : BaseEditFormItem<ExpenseDTO>
    {
        List<TypeExpensesDTO>? typesExpenses;

        public EditFormExpense()
        {
            itemOrigin = new ExpenseDTO();
            itemEdited = new ExpenseDTO();
            editContext = new(itemEdited);
        }

        protected internal override async Task LoadData()
        {
            typesExpenses = await ApiCRUD.GetAll<TypeExpensesDTO>();
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
