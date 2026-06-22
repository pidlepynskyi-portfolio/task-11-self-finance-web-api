using InfrastructureApi.DTO;

namespace SelfFinanceApp.Services.Endpoints
{
    public class ApiService
    {
        private EntityEndpointsService<IncomeDTO> _incomeEndpointsService;
        private EntityEndpointsService<ExpenseDTO> _expenseEndpointsService;
        private EntityEndpointsService<TypeIncomesDTO> _typeIncomeEndpointsService;
        private EntityEndpointsService<TypeExpensesDTO> _typeExpenseEndpointsService;
        private ReportEndpointsService _reportEndpointsService;

        public ApiService(WebApplication? app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            _incomeEndpointsService = new EntityEndpointsService<IncomeDTO>(app);
            _expenseEndpointsService = new EntityEndpointsService<ExpenseDTO>(app);
            _typeIncomeEndpointsService = new EntityEndpointsService<TypeIncomesDTO>(app);
            _typeExpenseEndpointsService = new EntityEndpointsService<TypeExpensesDTO>(app);
            _reportEndpointsService = new ReportEndpointsService(app);
        }

        public void MapApi()
        {
            _incomeEndpointsService.Map();
            _expenseEndpointsService.Map();
            _typeIncomeEndpointsService.Map();
            _typeExpenseEndpointsService.Map();
            _reportEndpointsService.Map();
        }
    }
}
