using ModelApi.Entities;
using System.Linq.Expressions;

namespace InfrastructureApi.DTO
{
    public class ExpenseDTO : BallanseDTO
    {
        public TypeExpensesDTO? TypeExpense { get; set; }

        public static Expression<Func<Expense, ExpenseDTO>> ExpenseSelector
        {
            get
            {
                return expense => new ExpenseDTO()
                {
                    Id = expense.Id,
                    Amount = expense.Amount.Value,
                    CreateDate = expense.CreateDate.Value,
                    UpdateDate = expense.UpdateDate!.Value,
                    TypeId = expense.TypeId,
                    Comments = expense.Comments!.Value,
                    TypeExpense = new TypeExpensesDTO()
                    {
                        Id = expense.TypeExpense!.Id,
                        Name = expense.TypeExpense.Name.Value!
                    }
                };
            }
        }
    }
}
