using InfrastructureApi.Common;
using InfrastructureApi.DTO;
using InfrastructureApi.Interfaces;
using ModelApi.Entities;
using ModelApi.Interfaces;
using ModelApi.ValueObjects;

namespace InfrastructureApi.Services
{
    public class IncomeService : ModelService, IEntityService<IncomeDTO>, IBallanseService<IncomeDTO>
    {
        private readonly IBallanseRepository<Income>? _incomeRepository;

        public IncomeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _incomeRepository = unitOfWork.Incomes;
        }

        public async Task<IEnumerable<IncomeDTO>> GetAllAsync()
        {
            var incomes = await _incomeRepository!.GetAllWithProjectionAsync(IncomeDTO.IncomeSelector);

            return incomes;
        }

        public async Task<IncomeDTO?> GetByIdAsync(int? id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            var income = await _incomeRepository!.GetByIdWithProjectionAsync(id, IncomeDTO.IncomeSelector);

            return income;
        }

        public async Task<double> GetSumByEnterDateAsync(DateTime toDate)
        {
            var result = await _incomeRepository!.GetSumByEnterDateAsync(toDate) ?? 0;

            return result;
        }

        public async Task<double> GetSumByPeriodAsync(DateTime fromDate, DateTime toDate)
        {
            CheckDatePeriod(fromDate, toDate);

            var result = await _incomeRepository!.GetSumByPeriodAsync(fromDate, toDate) ?? 0;

            return result;
        }

        public async Task<IEnumerable<IncomeDTO>> GetByEnterDateAsync(DateTime toDate)
        {
            var result = await _incomeRepository!.GetByEnterDateWithDetailAndProjectionAsync(toDate, IncomeDTO.IncomeSelector);

            return result;
        }

        public async Task<IEnumerable<IncomeDTO>> GetByPeriodAsync(DateTime fromDate, DateTime toDate)
        {
            CheckDatePeriod(fromDate, toDate);

            var result = await _incomeRepository!.GetByPeriodWithDetailAndProjectionAsync(fromDate, toDate, IncomeDTO.IncomeSelector);

            return result;
        }

        public async Task CreateAsync(IncomeDTO incomeDTO)
        {
            if (incomeDTO is null) throw new ArgumentNullException(nameof(incomeDTO));

            var income = new Income(new Amount(incomeDTO.Amount), incomeDTO.TypeId, new FreeText(incomeDTO.Comments));

            await _incomeRepository!.CreateAsync(income);
            await _unitOfWork!.SaveAsync();
        }

        public async Task UpdateAsync(int? id, IncomeDTO incomeDTO)
        {
            if (incomeDTO is null) throw new ArgumentNullException(nameof(incomeDTO));
            if (id is null) throw new ArgumentNullException(nameof(id));

            var income = await _incomeRepository!.GetByIdAsync(id) ?? throw new InvalidOperationException($"Data by id({id}) not found!");
            income.Change(new Amount(incomeDTO.Amount), incomeDTO.TypeId, new FreeText(incomeDTO.Comments));

            _incomeRepository.Update(income);
            await _unitOfWork!.SaveAsync();
        }

        public async Task DeleteAsync(int? id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            var income = await _incomeRepository!.GetByIdAsync(id) ?? throw new InvalidOperationException($"Data by id({id}) not found!");

            _incomeRepository.Delete(income);
            await _unitOfWork!.SaveAsync();
        }

        private void CheckDatePeriod(DateTime fromDate, DateTime toDate)
        {
            string errMsgArg = "The start date of the period must not exceed the end date of the period!";
            if (fromDate > toDate) throw new ArgumentException(errMsgArg, nameof(fromDate));
        }
    }
}
