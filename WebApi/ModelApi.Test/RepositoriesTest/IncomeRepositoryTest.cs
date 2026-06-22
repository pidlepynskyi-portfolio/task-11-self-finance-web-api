using Microsoft.EntityFrameworkCore;
using ModelApi.Entities;
using ModelApi.Interfaces;
using ModelApi.Services.DataSource;
using ModelApi.Services.UnitOfWork;
using ModelApi.ValueObjects;

namespace ModelApi.Test.RepositoriesTest
{
    [TestClass]
    public class IncomeRepositoryTest
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
        public void GetNullAllIncomes()
        {
            IEnumerable<Income> listIncomes = _unitOfWork!.Incomes.GetAllAsync().Result;

            Assert.IsNotNull(listIncomes);
            Assert.IsTrue(listIncomes.Count() == 0);
        }

        [TestMethod]
        public void GetNullIncomeById()
        {
            int id = 200;

            Income income = _unitOfWork!.Incomes.GetByIdAsync(id).Result;

            Assert.IsNull(income);
        }

        [TestMethod]
        public void GetAllIncomes()
        {
            IEnumerable<Income> listIncomes = _unitOfWork!.Incomes.GetAllAsync().Result;

            Assert.IsNotNull(listIncomes);
            Assert.IsTrue(listIncomes.Count() >= 0);
        }

        [TestMethod]
        public void GetIncomeById()
        {
            int id = 1;
            double amount = 4364.39;
            int typeId = 1;
            DateTime createdDateTime = DateTime.Parse("2024-03-10 00:00:00.000");

            Income income = _unitOfWork!.Incomes.GetByIdAsync(id).Result;

            Assert.IsNotNull(income);
            Assert.AreEqual(id, income.Id);
            Assert.AreEqual(amount, income.Amount.Value);
            Assert.AreEqual(typeId, income.TypeId);
            Assert.IsTrue(createdDateTime.ToString() == income.CreateDate.Value.ToString());
        }

        [TestMethod]
        public void CreateTypeIncome()
        {
            Income insertedIncome = null!;
            int? lastId = _unitOfWork!.Incomes.GetAllAsync().Result.Last().Id;
            Income createIncome = new Income(new Amount(3256.54), 2, null);

            _unitOfWork.Incomes.CreateAsync(createIncome);
            _unitOfWork.SaveAsync();
            insertedIncome = _unitOfWork.Incomes.GetByIdAsync(lastId + 1).Result;

            Assert.IsNotNull(insertedIncome);
            Assert.IsTrue(insertedIncome == createIncome);
        }

        [TestMethod]
        public void UpdateIncome()
        {
            Income editingIncome = _unitOfWork!.Incomes.GetByIdAsync(2).Result;
            FreeText commentBeforeEdit = editingIncome.Comments!;

            editingIncome.Change(null!, null, new FreeText("comment2_update1"));
            _unitOfWork.Incomes.Update(editingIncome);
            _unitOfWork.SaveAsync().Wait();
            Income editedIncome = _unitOfWork!.Incomes.GetByIdAsync(2).Result;
            FreeText commentAfterEdit = editedIncome.Comments!;

            Assert.IsNotNull(editedIncome);
            Assert.IsTrue(commentAfterEdit != commentBeforeEdit);
        }

        [TestMethod]
        public void DeleteIncome()
        {
            Income deletingIncome = _unitOfWork!.Incomes.GetByIdAsync(2).Result;

            _unitOfWork.Incomes.Delete(deletingIncome);
            _unitOfWork.SaveAsync().Wait();
            Income deletedIncome = _unitOfWork.Incomes.GetByIdAsync(2).Result;

            Assert.IsNull(deletedIncome);
        }

        [TestMethod]
        public void GetSymByEnterDate_Test()
        {
            double? expected = 2500.55;

            double? actual = _unitOfWork?.Incomes.GetSumByEnterDateAsync(DateTime.Parse("2024-07-24")).Result;

            Assert.IsNotNull(actual); 
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetSymByPeriod_Test()
        {
            double? expected = 7869.69;

            double? actual = _unitOfWork?.Incomes.GetSumByPeriodAsync(DateTime.Parse("2024-03-01"), DateTime.Parse("2024-03-31")).Result;

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }
    }
}
