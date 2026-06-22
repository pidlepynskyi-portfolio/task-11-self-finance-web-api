using ModelApi.Common;
using System.Text.RegularExpressions;

namespace ModelApi.ValueObjects
{
    public class Name : ValueObject
    {
        public string? Value { get; private set; }
        
        protected Name() { }
        public Name(string? name)
        {
            ValidateArguments(name);

            Value = name;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value!;
        }

        private void ValidateArguments(string? name)
        {
            if (String.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
            if (!Regex.IsMatch(name, @"^\w+(\s*\w*)*\w+$")) throw new ArgumentException("The argument \"Name\" must consist of letters, numbers and spasies only! ");
        }
    }
}
