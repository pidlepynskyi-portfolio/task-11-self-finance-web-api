namespace SelfFinanceApp.Services.RouteHistory
{
    public class RouteHistoryService
    {
        private List<string> _collectionHistoryRoutes { get; set; } = new List<string>();

        public void AddRoute(string uri)
        {
            _collectionHistoryRoutes.Add(uri);
        }

        public string GetPrevPath()
        {
            if (_collectionHistoryRoutes.Count < 1)
                throw new InvalidOperationException();

            return _collectionHistoryRoutes.Last();
        }

        public void Clear() => _collectionHistoryRoutes.Clear();
    }
}
