using InfrastructureApi.DTO;
using InfrastructureApi.DTO.Reports;
using SelfFinanceApp.Services.RoutesCollection;

namespace SelfFinanceApp.Services.ViewModelServices
{
    public class ReportService : BaseService
    {
        public ReportService(IHttpClientFactory clientFactory, IConfiguration appConfig, RoutesCollectionService routesCollection)
            : base(clientFactory, appConfig, routesCollection)
        {
        }

        public async Task<ReportDTO> ReportOnDate(DateTime onDate)
        {
            string requestUri = routesCollection.GetRouteReportOnDate(onDate.ToString("yyyyMMdd"));
            var result = await ExecuteReportRequest(requestUri);

            return result;
        }

        public async Task<ReportDTO> ReportByPeriod(DateTime fromDate, DateTime toDate)
        {
            string requestUri = routesCollection.GetRouteReportByPeriod(fromDate.ToString("yyyyMMdd"), toDate.ToString("yyyyMMdd"));
            var result = await ExecuteReportRequest(requestUri);

            return result;
        }

        public double[] GetDataTotalTypesSum<TypeBase, TBallanse>(List<TypeBase> types, List<TBallanse> ballanses)
            where TBallanse : BallanseDTO
            where TypeBase : TypeBaseDTO
        {
            if (types is null) throw new ArgumentNullException(nameof(types));
            if (ballanses is null) throw new ArgumentException(nameof(ballanses));

            List<double> result = new List<double>();

            foreach (var type in types)
            {
                double typeSum = 0;

                foreach (var item in ballanses)
                {
                    if (item.TypeId != type.Id)
                        continue;

                    typeSum += item.Amount!.Value;
                }

                result.Add(typeSum);
            }

            return result.ToArray();
        }

        public string[] GetLabelsTotalTypesSum<TypeBase>(List<TypeBase> types, double[] dataTotalTypesSum) where TypeBase : TypeBaseDTO
        {
            if (types is null) throw new ArgumentNullException(nameof(types));

            double totalSum = dataTotalTypesSum.Sum();

            List<string> result = new List<string>();

            for (int i = 0; i < types.Count; i++)
            {
                int persentTypeSum = GetPercentSum(dataTotalTypesSum[i], totalSum);

                result.Add($"{persentTypeSum}% - {types[i].Name}");
            }

            return result.ToArray();
        }

        public string[] GetLabelsTotalAllSum(double incomesSum, double expensesSum)
        {
            double totalSum = incomesSum + expensesSum;

            List<string> result = new List<string>();

            result.Add($"{GetPercentSum(incomesSum, totalSum)}% - Incomes");
            result.Add($"{GetPercentSum(expensesSum, totalSum)}% - Expenses");

            return result.ToArray();
        }

        private int GetPercentSum(double partSum, double totalSum)
        {
            if (totalSum == 0)
            {
                return 0;
            }

            double result = (partSum / totalSum) * 100;
            return (int)Math.Round(result);
        }

        private async Task<ReportDTO> ExecuteReportRequest(string requestUri)
        {
            var response = await httpClient.GetAsync(requestUri);
            await CheckErrorResponse(response);
            var result = await response.Content.ReadFromJsonAsync<ReportDTO>();
            CheckReadFromJsonResult(result);

            return result!;
        }
    }
}
