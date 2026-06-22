using Microsoft.EntityFrameworkCore;
using ModelApi.Entities;
using ModelApi.Interfaces;
using ModelApi.Services.DataSource;
using ModelApi.Services.UnitOfWork;
using ModelApi.ValueObjects;

namespace ModelApi.Test.RepositoriesTest
{
    [TestClass]
    public class ExpenseRepositoryTest
    {
        private static SelfFinanceDbContext? _dbContext;
        private static IUnitOfWork? _unitOfWork;

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SelfFinanceDbContext>();
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=SelfFinance;Trusted_Connection=True;";
            optionsBuilder.UseSqlServer(connectionString);
            _dbContext = new SelfFinanceDbContext(optionsBuilder.Options);
            _unitOfWork = new EFUnitOfWork(_dbContext);
        }

        [TestMethod]
        public void GetNullExpenseById()
        {
            int id = 200;

            Expense? expense = _unitOfWork?.Expenses.GetByIdAsync(id).Result;

            Assert.IsNull(expense);
        }

        [TestMethod]
        public void GetAllExpenses()
        {
            IEnumerable<Expense>? listExpenses = _unitOfWork?.Expenses.GetAllAsync().Result;

            Assert.IsNotNull(listExpenses);
            Assert.IsTrue(listExpenses.Count() >= 0);
        }

        [TestMethod]
        public void GetExpenseById()
        {
            int id = 1;
            double amount = 2500;
            int typeId = 1;
            DateTime createdDateTime = DateTime.Parse("2024-03-15 00:00:00.000");

            Expense? expense = _unitOfWork?.Expenses.GetByIdAsync(id).Result;

            Assert.IsNotNull(expense);
            Assert.AreEqual(id, expense.Id);
            Assert.AreEqual(amount, expense.Amount.Value);
            Assert.AreEqual(typeId, expense.TypeId);
            Assert.IsTrue(createdDateTime.ToString() == expense.CreateDate.Value.ToString());
        }

        [TestMethod]
        public void CreateExpense()
        {
            int? lastId = _unitOfWork?.Expenses.GetAllAsync().Result.Last().Id;
            Expense? createExpense = new Expense(new Amount(1458.54), 4, null);

            _unitOfWork?.Expenses.CreateAsync(createExpense);
            _unitOfWork?.SaveAsync();
            Expense? insertedExpense = _unitOfWork?.Expenses.GetByIdAsync(lastId + 1).Result;

            Assert.IsNotNull(insertedExpense);
            Assert.IsTrue(insertedExpense == createExpense);
        }

        [TestMethod]
        public void UpdateExpense()
        {
            Expense? editingExpense = _unitOfWork?.Expenses.GetByIdAsync(13).Result;
            FreeText? commentBeforeEdit = editingExpense?.Comments;

            editingExpense?.Change(null!, null, new FreeText("comment2_update1"));
            _unitOfWork?.Expenses.Update(editingExpense!);
            _unitOfWork?.SaveAsync().Wait();
            Expense? editedExpense = _unitOfWork?.Expenses.GetByIdAsync(13).Result;
            FreeText? commentAfterEdit = editedExpense?.Comments;

            Assert.IsNotNull(editedExpense);
            Assert.IsTrue(commentAfterEdit! != commentBeforeEdit!);
        }

        [TestMethod]
        public void DeleteExpense()
        {
            Expense? deletingExpense = _unitOfWork?.Expenses.GetByIdAsync(13).Result;

            _unitOfWork?.Expenses.Delete(deletingExpense!);
            _unitOfWork?.SaveAsync().Wait();
            Expense? deletedExpense = _unitOfWork?.Expenses.GetByIdAsync(13).Result;

            Assert.IsNull(deletedExpense);
        }

        [TestMethod]
        public void GetSymByEnterDate_Test()
        {
            double? expected = 2001;

            double? actual = _unitOfWork?.Expenses.GetSumByEnterDateAsync(DateTime.Parse("2024-07-24")).Result;

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetSymByPeriod_Test()
        {
            double? expected = 5200;

            double? actual = _unitOfWork?.Expenses.GetSumByPeriodAsync(DateTime.Parse("2024-03-01"), DateTime.Parse("2024-03-31")).Result;

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }
    }
}
