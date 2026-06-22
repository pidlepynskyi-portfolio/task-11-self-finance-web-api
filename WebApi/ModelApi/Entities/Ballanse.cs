using ModelApi.Common;
using ModelApi.ValueObjects;

namespace ModelApi.Entities
{
    public abstract class Ballanse : Entity
    {
        public Amount Amount { get; private protected set; } = null!;
        public DateOperation CreateDate { get; private protected set; } = null!;
        public DateOperation? UpdateDate { get; private protected set; }
        public int? TypeId { get; private protected set; }
        public FreeText? Comments { get; private protected set; }

        protected Ballanse() : base() { }

        public void Change(Amount amount, int? typeId, FreeText? comments)
        {
            ValidateArguments(amount, typeId);

            Amount = amount;
            UpdateDate = new DateOperation();
            TypeId = typeId;
            Comments = comments;
        }

        private protected void ValidateArguments(Amount amount, int? typeId)
        {
            if (amount is null) { throw new ArgumentNullException(nameof(amount)); }

            if (typeId is null) { throw new ArgumentNullException(nameof(typeId)); }
            if (typeId <= 0) { throw new ArgumentException("TypeId could be is bigger then ziro!"); }
        }
    }
}
