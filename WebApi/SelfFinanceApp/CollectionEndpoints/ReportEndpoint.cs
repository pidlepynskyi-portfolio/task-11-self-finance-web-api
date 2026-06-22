using InfrastructureApi.DTO;
using InfrastructureApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace SelfFinanceApp.CollectionEndpoints
{
    public class ReportEndpoint
    {
        public string GetReportRoute { get; } = "/api/report/date";
        public string GetReportByPeriod { get; } = "/api/report/period";

        private readonly string warningMsgParsDate = "The date entered is incorrect.";
        private readonly string warningMsgDateComparison = "The start date of the period must not exceed the end date of the period!";

        public ReportEndpoint() { }

        public async Task<object> GetDataReportFuncAsync(
            [FromQuery(Name = "date")] string onDate
            , IBallanseService<IncomeDTO> incomeService
            , IBallanseService<ExpenseDTO> expenseService
            , ILogger<ReportEndpoint> logger)
        {
            string endRequestMessage = "End GET request to get data for report by period";

            logger.LogInformation("Start GET request to get data for daily report");

            double? incomeSum = null;
            double? expenseSum = null;

            if (!TryParseDateParam(onDate, out DateTime convertEndDate))
            {
                return CallBadRequest(logger, warningMsgParsDate, endRequestMessage);
            }

            var listIncomeOperations = await incomeService.GetByEnterDateAsync(convertEndDate);
            if (listIncomeOperations != null)
            {
                incomeSum = await incomeService.GetSumByEnterDateAsync(convertEndDate);
            }

            var listExpenseOperations = await expenseService.GetByEnterDateAsync(convertEndDate);
            if (listExpenseOperations != null)
            {
                expenseSum = await expenseService.GetSumByEnterDateAsync(convertEndDate);
            }


            logger.LogInformation(endRequestMessage);

            return new
            {
                incomeReport = new
                {
                    incomeSum,
                    listIncomeOperations
                },
                expenseReport = new
                {
                    expenseSum,
                    listExpenseOperations
                }
            };
        }

        public async Task<object> GetDataReportByPeriodFuncAsync(
            [FromQuery(Name = "from")] string fromDate
            , IBallanseService<IncomeDTO> incomeService, IBallanseService<ExpenseDTO> expenseService
            , ILogger<ReportEndpoint> logger
            , [FromQuery(Name = "to")] string? toDate = null)
        {
            string endRequestMessage = "End GET request to get data for report by period";

            logger.LogInformation("Start GET request to get data for report by period");

            double? incomeSum = null;
            double? expenseSum = null;

            DateTime convertEndDate = DateTime.Now;

            if (!TryParseDateParam(fromDate, out DateTime convertStartDate))
            {
                return CallBadRequest(logger, warningMsgParsDate, endRequestMessage);
            }

            if (toDate != null)
            {
                if (!TryParseDateParam(toDate, out convertEndDate))
                {
                    return CallBadRequest(logger, warningMsgParsDate, endRequestMessage);
                }

                if (convertStartDate > convertEndDate)
                {
                    return CallBadRequest(logger, warningMsgDateComparison, endRequestMessage);
                }
            }

            var listIncomeOperations = await incomeService.GetByPeriodAsync(convertStartDate, convertEndDate);
            if (listIncomeOperations != null)
            {
                incomeSum = await incomeService.GetSumByPeriodAsync(convertStartDate, convertEndDate);
            }

            var listExpenseOperations = await expenseService.GetByPeriodAsync(convertStartDate, convertEndDate);
            if (listExpenseOperations != null)
            {
                expenseSum = await expenseService.GetSumByPeriodAsync(convertStartDate, convertEndDate);
            }

            logger.LogInformation(endRequestMessage);

            return new
            {
                incomeReport = new
                {
                    incomeSum,
                    listIncomeOperations
                },
                expenseReport = new
                {
                    expenseSum,
                    listExpenseOperations
                }
            };
        }

        private object CallBadRequest(ILogger<ReportEndpoint> logger, string warningMsg, string endRequestMessage)
        {
            logger.LogError(warningMsg);
            logger.LogInformation(endRequestMessage);
            return Results.BadRequest(new { message = warningMsg });
        }

        private bool TryParseDateParam(string date, out DateTime result)
        {
            return DateTime.TryParseExact(date, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out result);
        }
    }
}
