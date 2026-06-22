using InfrastructureApi.DTO;
using SelfFinanceApp.CollectionEndpoints;

namespace SelfFinanceApp.Services.Endpoints
{
    public class EntityEndpointsService<TypeDTO> where TypeDTO : BaseEntityDTO
    {
        private WebApplication? _app;
        private EntityEndpoints<TypeDTO> _entityEndpoints;
        public EntityEndpointsService(WebApplication? app)
        {
            _app = app ?? throw new ArgumentNullException(nameof(app));
            _entityEndpoints = new EntityEndpoints<TypeDTO>();
        }

        public void Map()
        {
            _app?.MapGet(_entityEndpoints.GetAllAndPostRoute, _entityEndpoints.GetAllFunc);
            _app?.MapGet(_entityEndpoints.GetAndEditAndDeleteByIdRoute, _entityEndpoints.GetByIdFunc);
            _app?.MapPost(_entityEndpoints.GetAllAndPostRoute, _entityEndpoints.PostAddFunc);
            _app?.MapPut(_entityEndpoints.GetAndEditAndDeleteByIdRoute, _entityEndpoints.PutEditByIdFunc);
            _app?.MapDelete(_entityEndpoints.GetAndEditAndDeleteByIdRoute, _entityEndpoints.DeleteFunc);
        }
    }
}
