using ModelApi.ValueObjects;

namespace ModelApi.Entities
{
    public class TypeExpense : TypeBase
    {
        public List<Expense> Expenses { get; private set; } = new();
        protected TypeExpense() : base() { }
        public TypeExpense(Name name, FreeText? description) : this()
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description;
        }
    }
}
