using InfrastructureApi.Common;
using InfrastructureApi.DTO;
using ModelApi.Entities;
using ModelApi.Interfaces;
using ModelApi.ValueObjects;

namespace InfrastructureApi.Services
{
    public class TypeIncomeService : ModelService, IEntityService<TypeIncomesDTO>
    {
        private readonly ITypesBaseRepository<TypeIncome>? _typeIncomeRepository;

        public TypeIncomeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _typeIncomeRepository = unitOfWork.TypesIncomes;
        }

        public async Task<IEnumerable<TypeIncomesDTO>> GetAllAsync()
        {
            var typesIncomes = await _typeIncomeRepository!.GetAllWithProjectionAsync(TypeIncomesDTO.TypeIncomeSelector);

            return typesIncomes;
        }

        public async Task<TypeIncomesDTO?> GetByIdAsync(int? id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            var typeIncome = await _typeIncomeRepository!.GetByIdWithProjectionAsync(id, TypeIncomesDTO.TypeIncomeSelector);

            return typeIncome;
        }

        public async Task CreateAsync(TypeIncomesDTO typeIncomeDTO)
        {
            if (typeIncomeDTO is null) throw new ArgumentNullException(nameof(typeIncomeDTO));
            await CheckDuplicateByName(typeIncomeDTO.Name);

            var typeIncome = new TypeIncome(new Name(typeIncomeDTO.Name), new FreeText(typeIncomeDTO.Description));

            await _typeIncomeRepository!.CreateAsync(typeIncome);
            await _unitOfWork!.SaveAsync();
        }

        public async Task UpdateAsync(int? id, TypeIncomesDTO typeIncomeDTO)
        {
            if (typeIncomeDTO is null) throw new ArgumentNullException(nameof(typeIncomeDTO));
            await CheckDuplicateByNameAndNotId(id, typeIncomeDTO.Name);

            var typeIncome = await _typeIncomeRepository!.GetByIdAsync(id) ?? throw new InvalidOperationException($"Data by id({id}) not found!");
            
            typeIncome.Change(new Name(typeIncomeDTO.Name), new FreeText(typeIncomeDTO.Description));

            _typeIncomeRepository.Update(typeIncome);
            await _unitOfWork!.SaveAsync();
        }

        public async Task DeleteAsync(int? id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            var typeIncome = await _typeIncomeRepository!.GetByIdWithDetailAsync(id) ?? throw new InvalidOperationException($"Data by id({id}) not found!");

            _typeIncomeRepository.Delete(typeIncome);
            await _unitOfWork!.SaveAsync();
        }

        private async Task CheckDuplicateByName(string name)
        {
            var checkItem = await _typeIncomeRepository!.IsFoundByFilterAsync(typeIncome => typeIncome.Name.Value == name);

            if (!checkItem)
                throw new InvalidOperationException($"A record with this name - {name} already exists");
        }

        private async Task CheckDuplicateByNameAndNotId(int? id, string? name)
        {
            var checkItem = await _typeIncomeRepository!.IsFoundByFilterAsync(typeIncome => typeIncome.Id != id && typeIncome.Name.Value == name);

            if (!checkItem)
                throw new InvalidOperationException($"A record with the specified name - {name} already exists");
        }
    }
}
