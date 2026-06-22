using ModelApi.Common;
using ModelApi.ValueObjects;

namespace ModelApi.Entities
{
    public abstract class TypeBase : Entity
    {
        public Name Name { get; private protected set; } = null!;
        public FreeText? Description { get; private protected set; }
        public DateOperation CreateDate { get; private protected set; } = null!;
        public DateOperation? UpdateDate { get; private protected set; }


        protected TypeBase() : base() { }

        public void Change(Name name, FreeText? description)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));

            Description = description;
            UpdateDate = new DateOperation();
        }
    }
}
