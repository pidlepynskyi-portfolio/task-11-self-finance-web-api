using ModelApi.ValueObjects;

namespace ModelApi.Entities
{
    public class Income : Ballanse
    {
        public TypeIncome? TypeIncome { get; private set; }
        
        protected Income() : base() { }
        public Income(Amount amount, int? typeId, FreeText? comments) : this()
        {
            ValidateArguments(amount, typeId);

            Amount = amount;
            TypeId = typeId;
            Comments = comments;
        }
    }
}
