namespace SelfFinanceApp.Services.RoutesCollection
{
    public class RoutesCollectionService
    {
        public string GetRouteEntity(string nameEntityController, int? id = null)
        {
            if (id is null)
            {
                return String.Format(EntitiesRoutes.ListAndAddItem, nameEntityController);
            }

            return String.Format(EntitiesRoutes.GetAndEditAndDeleteItemById, nameEntityController, id);
        }

        public string GetRouteReportOnDate(string onDate)
        {
            return String.Format(ReportRoutes.OnDate, onDate);
        }

        public string GetRouteReportByPeriod(string fromDate, string toDate)
        {
            return String.Format(ReportRoutes.ByPeriod, fromDate, toDate);
        }
    }
}
