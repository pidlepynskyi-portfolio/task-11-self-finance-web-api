using ModelApi.Common;

namespace ModelApi.ValueObjects
{
    public class Amount : ValueObject
    {
        public double? Value { get; private set; }

        protected Amount() { }
        public Amount(double? amount)
        {
            ValidateArguments(amount);

            Value = amount;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value!;
        }

        private void ValidateArguments(double? amount)
        {
            if (amount is null) { throw new ArgumentNullException(nameof(amount));}
            if (amount <= 0) { throw new ArgumentException("Amount could be is bigger then ziro!"); }
        }
    }
}
