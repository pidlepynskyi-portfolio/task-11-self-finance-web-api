using System.ComponentModel.DataAnnotations;

namespace InfrastructureApi.DTO.Validators
{
    public class NumericUnsignedAttribute : ValidationAttribute
    {
        private Type _type;
        public NumericUnsignedAttribute(Type type)
        {
            _type = type ?? throw new ArgumentNullException(nameof(type));
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if ((_type.Name == "Int32" && (int)value! <= 0)
                || (_type.Name == "Double" && (double)value! <= 0))
                return new ValidationResult("It is allowed to enter a number greater than 0");

            return ValidationResult.Success;
        }
    }
}
