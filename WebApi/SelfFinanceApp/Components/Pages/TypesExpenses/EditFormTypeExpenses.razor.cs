using InfrastructureApi.DTO;
using SelfFinanceApp.Components.Pages.BaseEditForm;

namespace SelfFinanceApp.Components.Pages.TypesExpenses
{
    public partial class EditFormTypeExpenses : BaseEditFormItem<TypeExpensesDTO>
    {
        public EditFormTypeExpenses()
        {
            itemOrigin = new TypeExpensesDTO();
            itemEdited = new TypeExpensesDTO();
            editContext = new(itemEdited);
        }

        protected internal override void CopyItem()
        {
            itemEdited.Id = itemOrigin.Id;
            itemEdited.Name = itemOrigin.Name;
            itemEdited.Description = itemOrigin.Description;
        }
    }
}
