using ModelApi.ValueObjects;

namespace ModelApi.Entities
{
    public class Expense : Ballanse
    {
        public TypeExpense? TypeExpense { get; private set; }

        protected Expense() : base() { }
        public Expense(Amount amount, int? typeId, FreeText? comments) : this()
        {
            ValidateArguments(amount, typeId);

            Amount = amount;
            TypeId = typeId;
            Comments = comments;
        }
    }
}
