using SelfFinanceApp.CollectionEndpoints;

namespace SelfFinanceApp.Services.Endpoints
{
    public class ReportEndpointsService
    {
        private WebApplication? _app;
        private ReportEndpoint _endpoint;

        public ReportEndpointsService(WebApplication? app)
        {
            _app = app;
            _endpoint = new ReportEndpoint();
        }

        public void Map()
        {
            _app?.MapGet(_endpoint.GetReportRoute, _endpoint.GetDataReportFuncAsync);
            _app?.MapGet(_endpoint.GetReportByPeriod, _endpoint.GetDataReportByPeriodFuncAsync);
        }
    }
}
