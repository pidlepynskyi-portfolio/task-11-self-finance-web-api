using InfrastructureApi.DTO;
using SelfFinanceApp.Common;
using SelfFinanceApp.Services.RoutesCollection;

namespace SelfFinanceApp.Services.ViewModelServices
{
    public class EntitiesService : BaseService
    {
        public EntitiesService(IHttpClientFactory clientFactory, IConfiguration appConfig, RoutesCollectionService routesCollection)
            : base(clientFactory, appConfig, routesCollection)
        {
        }

        public async Task<List<TEntityDTO>> GetAll<TEntityDTO>() where TEntityDTO : BaseEntityDTO
        {
            string requestUri = routesCollection.GetRouteEntity(GetNameController(typeof(TEntityDTO)));
            var response = await GetResponseMessage(requestUri);

            await CheckErrorResponse(response);
            var result = await response.Content.ReadFromJsonAsync<List<TEntityDTO>>();
            CheckReadFromJsonResult(result);

            return result!;
        }

        public async Task<TEntityDTO> GetById<TEntityDTO>(int id) where TEntityDTO : BaseEntityDTO
        {
            string requestUri = routesCollection.GetRouteEntity(GetNameController(typeof(TEntityDTO)), id);
            var response = await GetResponseMessage(requestUri);

            await CheckErrorResponse(response);
            var result = await response.Content.ReadFromJsonAsync<TEntityDTO>();
            CheckReadFromJsonResult(result);

            return result!;
        }

        public async Task PostItem<TEntityDTO>(TEntityDTO item) where TEntityDTO : BaseEntityDTO
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            string requestUri = routesCollection.GetRouteEntity(GetNameController(typeof(TEntityDTO)));

            var response = await httpClient.PostAsJsonAsync(requestUri, item);
            await CheckErrorResponse(response);
        }

        public async Task PutOrDeleteItem<TEntityDTO>(TEntityDTO item) where TEntityDTO : BaseEntityDTO
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            string requestUri = routesCollection.GetRouteEntity(GetNameController(typeof(TEntityDTO)), item.Id);

            var response = await httpClient.PutAsJsonAsync(requestUri, item);
            await CheckErrorResponse(response);
        }

        private async Task<HttpResponseMessage> GetResponseMessage(string requestUriForLoad)
        {
            var response = await httpClient.GetAsync(requestUriForLoad);

            await CheckErrorResponse(response);

            return response;
        }

        private string GetNameController(Type type)
        {
            return type.Name
                .DeletePartNameTypeEntity("DTO")
                .LowerFirstChar();
        }
    }
}
