using InfrastructureApi.DTO.Reports;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SelfFinanceApp.Common.Enums;
using SelfFinanceApp.Services.ViewModelServices;

namespace SelfFinanceApp.Components.Pages.Reports
{
    public partial class Report
    {
        private TypeReportEnum _selectedTypeReport = TypeReportEnum.OnDate;

        private DateTime? _inputedOnDate = DateTime.Now.Date;
        private DateRange _dateRange = new DateRange(DateTime.Now.Date.AddDays(-1).Date, DateTime.Now.Date);

        private ReportDTO _report = null!;

        private string _msgWrongValidInputPeriodDate = "The start date of the period must not exceed the end date of the period!";
        private string _classesValid = "form-control";
        private string _classEnableValidMsg = "report__valid__list report__valid__list_disable";

        private RenderFragment _incomeReportContent { get; set; } = default!;
        private RenderFragment _expenseReportContent { get; set; } = default!;
        private RenderFragment _chartsReport { get; set; } = default!;

        [Inject] ReportService ReportService { get; set; } = default!;

        private TypeReportEnum SelectedTypeReport
        {
            get => _selectedTypeReport;
            set
            {
                OnSelectBeforeTypeReport(value);
                _selectedTypeReport = value;
            }
        }

        private async Task GenerateReport(TypeReportEnum typeReport)
        {
            switch (typeReport)
            {
                case TypeReportEnum.OnDate:
                    _report = await ReportService.ReportOnDate(_inputedOnDate!.Value);
                    return;
                case TypeReportEnum.ByPeriod:
                    _report = await ReportService.ReportByPeriod(_dateRange.Start!.Value, _dateRange.End!.Value);
                    return;
            }
        }

        private void ShowReport()
        {
            _incomeReportContent = RenderIncomeReportContent;
            _expenseReportContent = RenderExpenseReportContent;
            _chartsReport = RenderChartsReport;
        }

        private async Task RunReport()
        {
            Reset(true);

            ShowReport();

            await GenerateReport(_selectedTypeReport);
        }

        private void Reset(bool reReport = false)
        {
            if (!reReport)
            {
                _dateRange = new DateRange(DateTime.Now.Date.AddDays(-1).Date, DateTime.Now.Date);
                _inputedOnDate = DateTime.Now.Date;
            }

            if (_report is null)
            {
                return;
            }

            _incomeReportContent = default!;
            _expenseReportContent = default!;
            _chartsReport = default!;
            _report = null!;
        }

        private void OnSelectBeforeTypeReport(TypeReportEnum value)
        {
            if (value == _selectedTypeReport) { return; }

            Reset();
        }
    }
}
