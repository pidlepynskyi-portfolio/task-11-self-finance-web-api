using InfrastructureApi.DTO.Reports;
using InfrastructureApi.DTO;
using Microsoft.AspNetCore.Components;
using SelfFinanceApp.Services.ViewModelServices;

namespace SelfFinanceApp.Components.Pages.Reports
{
    public partial class ChartsReport
    {
        private string[] _labelsTotalAllSum = default!;
        private double[] _dataTotalAllSum = default!;

        private double[] _dataTotalTypesIncomesSum = default!;
        private double[] _dataTotalTypesExpnesesSum = default!;
        private string[] _labelsTotalTypesIncomesSum = default!;
        private string[] _labelsTotalTypesExensesSum = default!;

        [Parameter]
        public ReportDTO Report { get; set; } = default!;

        [Inject] ReportService ReportService { get; set; } = default!;
        [Inject] EntitiesService EntitiesService { get; set; } = default!;


        protected override async Task OnInitializedAsync()
        {
            if (Report.IncomeReport!.IncomeSum == 0
                && Report.ExpenseReport!.ExpenseSum == 0)
                return;

            _labelsTotalAllSum = ReportService.GetLabelsTotalAllSum(Report.IncomeReport!.IncomeSum, Report.ExpenseReport!.ExpenseSum);
            _dataTotalAllSum = new[] { Report.IncomeReport!.IncomeSum, Report.ExpenseReport!.ExpenseSum };

            if (Report.IncomeReport!.IncomeSum != 0)
            {
                var typesIncomes = await EntitiesService.GetAll<TypeIncomesDTO>();
                _dataTotalTypesIncomesSum = ReportService.GetDataTotalTypesSum<TypeIncomesDTO, IncomeDTO>(typesIncomes, Report.IncomeReport.ListIncomeOperations!);
                _labelsTotalTypesIncomesSum = ReportService.GetLabelsTotalTypesSum<TypeIncomesDTO>(typesIncomes, _dataTotalTypesIncomesSum);
            }

            if (Report.ExpenseReport!.ExpenseSum != 0)
            {
                var typesExpenses = await EntitiesService.GetAll<TypeExpensesDTO>();
                _dataTotalTypesExpnesesSum = ReportService.GetDataTotalTypesSum<TypeExpensesDTO, ExpenseDTO>(typesExpenses, Report.ExpenseReport.ListExpenseOperations!);
                _labelsTotalTypesExensesSum = ReportService.GetLabelsTotalTypesSum<TypeExpensesDTO>(typesExpenses, _dataTotalTypesExpnesesSum);
            }
        }
    }
}
