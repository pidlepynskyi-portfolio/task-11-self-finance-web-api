namespace InfrastructureApi.DTO.Reports
{
    public class IncomesReport
    {
        public double IncomeSum { get; set; }
        public List<IncomeDTO>? ListIncomeOperations { get; set; }
    }
}
