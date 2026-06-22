using ModelApi.Entities;
using System.Linq.Expressions;

namespace InfrastructureApi.DTO
{
    public class IncomeDTO : BallanseDTO
    {
        public TypeIncomesDTO? TypeIncome { get; set; }

        public static Expression<Func<Income, IncomeDTO>> IncomeSelector
        {
            get
            {
                return income => new IncomeDTO()
                {
                    Id = income.Id,
                    Amount = income.Amount.Value,
                    CreateDate = income.CreateDate.Value,
                    UpdateDate = income.UpdateDate!.Value,
                    TypeId = income.TypeId,
                    Comments = income.Comments!.Value,
                    TypeIncome = new TypeIncomesDTO()
                    {
                        Id = income.TypeIncome!.Id,
                        Name = income.TypeIncome.Name.Value!
                    }
                };
            }
        }
    }
}
