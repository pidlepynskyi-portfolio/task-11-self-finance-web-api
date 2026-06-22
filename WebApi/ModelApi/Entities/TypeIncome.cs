using ModelApi.ValueObjects;

namespace ModelApi.Entities
{
    public class TypeIncome : TypeBase
    {
        public List<Income>? Incomes { get; private set; }
        protected TypeIncome() : base() { }
        public TypeIncome(Name name, FreeText? description) : this()
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description;
        }
    }
}
