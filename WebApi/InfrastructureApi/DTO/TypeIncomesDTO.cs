using ModelApi.Entities;
using System.Linq.Expressions;

namespace InfrastructureApi.DTO
{
    public class TypeIncomesDTO : TypeBaseDTO
    {
        public List<IncomeDTO>? Incomes { get; set; }

        public static Expression<Func<TypeIncome, TypeIncomesDTO>> TypeIncomeSelector
        {
            get
            {
                return typeIncome => new TypeIncomesDTO()
                {
                    Id = typeIncome.Id,
                    Name = typeIncome.Name.Value!,
                    Description = typeIncome.Description!.Value,
                    CreateDate = typeIncome.CreateDate.Value,
                    UpdateDate = typeIncome.UpdateDate!.Value
                };
            }
        }
    }
}
