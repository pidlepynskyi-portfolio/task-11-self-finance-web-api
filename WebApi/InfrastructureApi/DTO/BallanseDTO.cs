using System.ComponentModel.DataAnnotations;
using InfrastructureApi.DTO.Validators;

namespace InfrastructureApi.DTO
{
    public abstract class BallanseDTO : BaseEntityDTO
    {
        [Required]
        [NumericUnsigned(typeof(double))]
        public double? Amount { get; set; }
        [Required]
        [NumericUnsigned(typeof(int))]
        public int? TypeId { get; set; }
        public string? Comments { get; set; }
    }
}
