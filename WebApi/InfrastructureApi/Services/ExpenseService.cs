using InfrastructureApi.Common;
using InfrastructureApi.DTO;
using InfrastructureApi.Interfaces;
using ModelApi.Entities;
using ModelApi.Interfaces;
using ModelApi.ValueObjects;

namespace InfrastructureApi.Services
{
    public class ExpenseService : ModelService, IEntityService<ExpenseDTO>, IBallanseService<ExpenseDTO>
    {
        private readonly IBallanseRepository<Expense>? _expenseRepository;
        
        public ExpenseService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _expenseRepository = unitOfWork.Expenses;
        }

        public async Task<IEnumerable<ExpenseDTO>> GetAllAsync()
        {
            var expenses = await _expenseRepository!.GetAllWithProjectionAsync(ExpenseDTO.ExpenseSelector);

            return expenses;
        }

        public async Task<ExpenseDTO?> GetByIdAsync(int? id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            var expense = await _expenseRepository!.GetByIdWithProjectionAsync(id, ExpenseDTO.ExpenseSelector);

            return expense;
        }

        public async Task<double> GetSumByEnterDateAsync(DateTime toDate)
        {
            double result = await _expenseRepository!.GetSumByEnterDateAsync(toDate) ?? 0;

            return result;
        }

        public async Task<double> GetSumByPeriodAsync(DateTime fromDate, DateTime toDate)
        {
            CheckDatePeriod(fromDate, toDate);

            double result = await _expenseRepository!.GetSumByPeriodAsync(fromDate, toDate) ?? 0;

            return result;
        }

        public async Task<IEnumerable<ExpenseDTO>> GetByEnterDateAsync(DateTime toDate)
        {
            var result = await _expenseRepository!.GetByEnterDateWithDetailAndProjectionAsync(toDate, ExpenseDTO.ExpenseSelector);

            return result;
        }

        public async Task<IEnumerable<ExpenseDTO>> GetByPeriodAsync(DateTime fromDate, DateTime toDate)
        {
            CheckDatePeriod(fromDate, toDate);

            var result = await _expenseRepository!.GetByPeriodWithDetailAndProjectionAsync(fromDate, toDate, ExpenseDTO.ExpenseSelector);

            return result;
        }

        public async Task CreateAsync(ExpenseDTO expenseDTO)
        {
            if (expenseDTO is null) throw new ArgumentNullException(nameof(expenseDTO));

            var expense = new Expense(new Amount(expenseDTO.Amount), expenseDTO.TypeId, new FreeText(expenseDTO.Comments));

            await _expenseRepository!.CreateAsync(expense);
            await _unitOfWork!.SaveAsync();
        }

        public async Task UpdateAsync(int? id, ExpenseDTO expenseDTO)
        {
            if (expenseDTO is null) throw new ArgumentNullException(nameof(expenseDTO));

            var expense = await _expenseRepository!.GetByIdAsync(id) ?? throw new InvalidOperationException($"Data by id({id}) not found!");

            expense.Change(new Amount(expenseDTO.Amount), expenseDTO.TypeId, new FreeText(expenseDTO.Comments));

            _expenseRepository.Update(expense);
            await _unitOfWork!.SaveAsync();
        }

        public async Task DeleteAsync(int? id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            var expense = await _expenseRepository!.GetByIdAsync(id) ?? throw new InvalidOperationException($"Data by id({id}) not found!"); ;

            _expenseRepository.Delete(expense);
            await _unitOfWork!.SaveAsync();
        }

        private void CheckDatePeriod(DateTime fromDate, DateTime toDate)
        {
            string errMsgArg = "The start date of the period must not exceed the end date of the period!";
            if (fromDate > toDate) throw new ArgumentException(errMsgArg, nameof(fromDate));
        }
    }
}
