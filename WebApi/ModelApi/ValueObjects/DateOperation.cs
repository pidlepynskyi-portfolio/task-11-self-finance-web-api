using ModelApi.Common;

namespace ModelApi.ValueObjects
{
    public class DateOperation : ValueObject
    {
        public DateTime Value { get; private set; } = DateTime.Now;

        public DateOperation() { }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
