using ModelApi.Common;

namespace ModelApi.ValueObjects
{
    public class FreeText : ValueObject
    {
        public string? Value { get; private set; }

        protected FreeText() { }
        public FreeText(string? text)
        {
            Value = text;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value!;
        }
    }
}
