namespace SelfFinanceApp.Services.RoutesCollection
{
    public static class ReportRoutes
    {
        public static string OnDate { get; } = "/api/report/date?date={0}";
        public static string ByPeriod { get; } = "/api/report/period?from={0}&to={1}";
    }
}
