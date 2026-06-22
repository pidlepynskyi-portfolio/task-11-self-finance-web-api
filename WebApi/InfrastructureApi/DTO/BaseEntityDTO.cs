namespace InfrastructureApi.DTO
{
    public abstract class BaseEntityDTO
    {
        public int? Id { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
