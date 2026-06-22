using ModelApi.Entities;
using System.Linq.Expressions;

namespace InfrastructureApi.DTO
{
    public class TypeExpensesDTO : TypeBaseDTO
    {
        public List<ExpenseDTO>? Expenses { get; set; }

        public static Expression<Func<TypeExpense, TypeExpensesDTO>> TypeExpenseSelector
        {
            get
            {
                return typeExpense => new TypeExpensesDTO()
                {
                    Id = typeExpense.Id,
                    Name = typeExpense.Name.Value!,
                    Description = typeExpense.Description!.Value,
                    CreateDate = typeExpense.CreateDate.Value,
                    UpdateDate = typeExpense.UpdateDate!.Value
                };
            }
        }
    }
}
