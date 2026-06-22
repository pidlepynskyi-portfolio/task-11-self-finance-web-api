using System.ComponentModel.DataAnnotations;

namespace InfrastructureApi.DTO
{
    public abstract class TypeBaseDTO : BaseEntityDTO
    {
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
