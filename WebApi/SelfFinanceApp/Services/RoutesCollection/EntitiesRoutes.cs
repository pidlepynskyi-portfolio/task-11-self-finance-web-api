namespace SelfFinanceApp.Services.RoutesCollection
{
    public static class EntitiesRoutes
    {
        public static string ListAndAddItem { get; } = "/api/{0}";
        public static string GetAndEditAndDeleteItemById { get; } = "/api/{0}/{1}";
    }
}
