using InfrastructureApi.DTO;
using InfrastructureApi.Common;
using SelfFinanceApp.Common;

namespace SelfFinanceApp.CollectionEndpoints
{
    public class EntityEndpoints<TypeDTO> 
        where TypeDTO : BaseEntityDTO
    {
        public string GetAllAndPostRoute { get; }
        public string GetAndEditAndDeleteByIdRoute { get; }

        private string _nameTypeEntity = String.Empty;
        private string _apiController = String.Empty;

        public EntityEndpoints()
        {
            _nameTypeEntity = typeof(TypeDTO).Name.DeletePartNameTypeEntity("DTO");
            _apiController = _nameTypeEntity.LowerFirstChar();

            GetAllAndPostRoute = $"/api/{_apiController}";
            GetAndEditAndDeleteByIdRoute = $"/api/{_apiController}/{{id}}";
        }

        public async Task<IEnumerable<TypeDTO>> GetAllFunc(IEntityService<TypeDTO> entityService, ILogger<EntityEndpoints<TypeDTO>> logger)
        {
            logger.LogInformation($"Start GET request to get all {_nameTypeEntity}s");
            var listResult = await entityService.GetAllAsync();
            logger.LogInformation($"End GET request to get all {_nameTypeEntity}s");
            return listResult;
        }

        public async Task<object?> GetByIdFunc(int id, IEntityService<TypeDTO> entityService, ILogger<EntityEndpoints<TypeDTO>> logger)
        {
            logger.LogInformation($"Start GET request to get by id {_nameTypeEntity}");
            string endRequestMsgLog = $"End GET request to get by id {_nameTypeEntity}";
            var entity = await entityService.GetByIdAsync(id);
            if (entity is null)
            {
                return Results.NotFound(NotFoundObjectRequest(logger, endRequestMsgLog));
            }

            logger.LogInformation(endRequestMsgLog);
            return entity;
        }

        public async Task<object> PostAddFunc(TypeDTO entity, IEntityService<TypeDTO> entityService, ILogger<EntityEndpoints<TypeDTO>> logger)
        {
            logger.LogInformation($"Start POST request to add new {_nameTypeEntity}");
            await entityService.CreateAsync(entity);

            logger.LogInformation($"End POST request to add new {_nameTypeEntity}");
            return new { message = $"{_nameTypeEntity} added successed!" };
        }

        public async Task<object?> PutEditByIdFunc(int id, TypeDTO entity, IEntityService<TypeDTO> entityService, ILogger<EntityEndpoints<TypeDTO>> logger)
        {
            logger.LogInformation($"Start PUT request to edit exists {_nameTypeEntity}");
            string endRequestMsgLog = $"End PUT request to remove exists {_nameTypeEntity}";

            if (id != entity.Id)
            {
                logger.LogInformation(endRequestMsgLog);
                return Results.BadRequest("Path param Id not equal entity Id");
            }

            var editedEntity = await entityService.GetByIdAsync(id);
            if (editedEntity is null)
            {
                return Results.NotFound(NotFoundObjectRequest(logger, endRequestMsgLog));
            }

            await entityService.UpdateAsync(id, entity);

            editedEntity = await entityService.GetByIdAsync(id);
            if (editedEntity is null)
            {
                return Results.NotFound(NotFoundObjectRequest(logger, endRequestMsgLog));
            }
            logger.LogInformation(endRequestMsgLog);
            return editedEntity;
        }

        public async Task<object?> DeleteFunc(int id, IEntityService<TypeDTO> entityService, ILogger<EntityEndpoints<TypeDTO>> logger)
        {
            logger.LogInformation($"Start DELETE request to remove exists {_nameTypeEntity}");
            string endRequestMsgLog = $"End DELETE request to remove exists {_nameTypeEntity}";
            var entity = await entityService.GetByIdAsync(id);
            if (entity is null)
            {
                return Results.NotFound(NotFoundObjectRequest(logger, endRequestMsgLog));
            }

            await entityService.DeleteAsync(id);

            logger.LogInformation(endRequestMsgLog);
            return entity;
        }

        private object NotFoundObjectRequest(ILogger<EntityEndpoints<TypeDTO>> logger, string endRequestMessage, string warningMsg = "")
        {
            if (warningMsg == "") warningMsg = $"{_nameTypeEntity} not found";

            logger.LogError(warningMsg);
            logger.LogInformation(endRequestMessage);

            return new { message = warningMsg };
        }
    }
}
