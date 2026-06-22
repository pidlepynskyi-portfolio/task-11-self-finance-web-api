namespace InfrastructureApi.DTO.Reports
{
    public class ExpensesReport
    {
        public double ExpenseSum { get; set; }
        public List<ExpenseDTO>? ListExpenseOperations { get; set; }
    }
}
