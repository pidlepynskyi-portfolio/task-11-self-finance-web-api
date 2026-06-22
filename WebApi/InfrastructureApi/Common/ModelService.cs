using ModelApi.Interfaces;

namespace InfrastructureApi.Common
{
    public abstract class ModelService
    {
        protected readonly IUnitOfWork? _unitOfWork;

        public ModelService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
    }
}
