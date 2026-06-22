using InfrastructureApi.DTO;
using InfrastructureApi.Services;
using Microsoft.EntityFrameworkCore;
using ModelApi.Services.DataSource;
using ModelApi.Services.UnitOfWork;

namespace InfrastructureApi.Test
{
    [TestClass]
    public class ExpenseServiceTest
    {
        private static ExpenseService? _service;

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SelfFinanceDbContext>();
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=SelfFinance;Trusted_Connection=True;";
            optionsBuilder.UseSqlServer(connectionString);
            var dbContext = new SelfFinanceDbContext(optionsBuilder.Options);
            var unitOfWork = new EFUnitOfWork(dbContext);
            _service = new ExpenseService(unitOfWork);
        }

        [TestMethod]
        public void GetAllAsync_ReturnObjectIEnumerableOfExpenseDTO()
        {
            var listExpenseDTO = _service?.GetAllAsync().Result;

            Assert.IsNotNull(listExpenseDTO);
            Assert.IsTrue(listExpenseDTO.Count() >= 1);
        }

        [TestMethod]
        public void GetByIdAsync_CheckParamForNull_ThrowArgumentNullException()
        {
            Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _service?.GetByIdAsync(null));
        }

        [TestMethod]
        public void GetByIdAsync_ReturnObjectExpense()
        {
            int expectedId = 1;
            double expectedAmount = 2500.00;
            DateTime expectedCreateDate = DateTime.Parse("2024-03-15 00:00:00.000");
            int expectedTypeExpenseId = 1;

            var expenseDTO = _service?.GetByIdAsync(1).Result;

            Assert.IsNotNull(expenseDTO);
            Assert.AreEqual(expectedId, expenseDTO.Id);
            Assert.AreEqual(expectedAmount, expenseDTO.Amount);
            Assert.AreEqual(expectedTypeExpenseId, expenseDTO.TypeId);
            Assert.AreEqual(expectedCreateDate, expenseDTO.CreateDate);
        }

        [TestMethod]
        public void GetSumByEnterDateAsync_ReturnSumAmount()
        {
            double expectedSumAmount = 2001;

            var actualSumAmount = _service?.GetSumByEnterDateAsync(DateTime.Parse("2024-07-24")).Result;

            Assert.IsNotNull(actualSumAmount);
            Assert.AreEqual(expectedSumAmount, actualSumAmount);
        }

        [TestMethod]
        public void GetSumByPeriodAsync_ReturnSumAmount()
        {
            double expectedSumAmount = 5200.00;
            DateTime fromDate = DateTime.Parse("2024-03-01");
            DateTime toDate = DateTime.Parse("2024-03-31");

            var actualSumAmount = _service?.GetSumByPeriodAsync(fromDate, toDate).Result;

            Assert.IsNotNull(actualSumAmount);
            Assert.AreEqual(expectedSumAmount, actualSumAmount);
        }

        [TestMethod]
        public void GetByEnterDateAsync_ReturnObjectIEnumerableOfExpenseDTO()
        {
            int expectedFirsId = 11;
            int expectedLastId = 12;

            var listExpenseDTO = _service?.GetByEnterDateAsync(DateTime.Parse("2024-07-24")).Result;
            var firstExpenseDTO = listExpenseDTO?.FirstOrDefault();
            var lastExpenseDTO = listExpenseDTO?.LastOrDefault();

            Assert.IsNotNull(listExpenseDTO);
            Assert.AreEqual(expectedFirsId, firstExpenseDTO?.Id);
            Assert.AreEqual(expectedLastId, lastExpenseDTO?.Id);
        }

        [TestMethod]
        public void GetByPeriodAsync_ReturnObjectIEnumerableOfExpenseDTO()
        {
            int expectedFirsId = 1;
            int expectedLastId = 5;
            DateTime fromDate = DateTime.Parse("2024-03-01");
            DateTime toDate = DateTime.Parse("2024-03-31");

            var listExpenseDTO = _service?.GetByPeriodAsync(fromDate, toDate).Result;
            var firstExpenseDTO = listExpenseDTO?.FirstOrDefault();
            var lastExpenseDTO = listExpenseDTO?.LastOrDefault();

            Assert.IsNotNull(listExpenseDTO);
            Assert.AreEqual(expectedFirsId, firstExpenseDTO?.Id);
            Assert.AreEqual(expectedLastId, lastExpenseDTO?.Id);
        }

        [TestMethod]
        public void CreateAsync_CheckParamForNull_ThrowArgumentNullException()
        {
            Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _service?.CreateAsync(null!));
        }

        [TestMethod]
        public void CreateAsync_ResultCreatedAndInsertObjectExpense()
        {
            ExpenseDTO insertedObject = null!;
            int? lastId = _service?.GetAllAsync().Result.LastOrDefault()?.Id;
            ExpenseDTO createingExpenseDTO = new ExpenseDTO()
            {
                Amount = 1000,
                TypeId = 4,
                Comments = "test_comments"
            };

            _service?.CreateAsync(createingExpenseDTO).Wait();

            insertedObject = _service?.GetAllAsync().Result.LastOrDefault()!;

            Assert.IsNotNull(insertedObject);
            Assert.IsTrue(insertedObject.Id > lastId);
        }

        [TestMethod]
        public void UpdateAsync_CheckParamForNull_ThrowArgumentNullException()
        {
            Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _service?.UpdateAsync(null, null!));
        }

        [TestMethod]
        public void UpdateAsync_ResultEditedObjectExpense()
        {
            ExpenseDTO? editingExpenseDTO = _service?.GetAllAsync().Result.LastOrDefault();
            double? amountBeforeEdit = editingExpenseDTO?.Amount;
            string? commentsBeforeEdit = editingExpenseDTO?.Comments;

            editingExpenseDTO!.Amount = 2000;
            editingExpenseDTO!.Comments = "test_edit";

            _service?.UpdateAsync(editingExpenseDTO.Id, editingExpenseDTO).Wait();

            ExpenseDTO? editedExpenseDTO = _service?.GetByIdAsync(editingExpenseDTO.Id).Result;

            Assert.IsNotNull(editedExpenseDTO);
            Assert.AreNotEqual(amountBeforeEdit, editedExpenseDTO.Amount);
            Assert.AreNotEqual(commentsBeforeEdit, editedExpenseDTO.Comments);
        }

        [TestMethod]
        public void DeleteAsync_CheckParamForNull_ThrowArgumentNullException()
        {
            Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _service?.DeleteAsync(null!));
        }

        [TestMethod]
        public void DeleteAsync_ResultNull()
        {
            ExpenseDTO? deletingExpenseDTO = _service?.GetAllAsync().Result.LastOrDefault();
            int? id = deletingExpenseDTO?.Id;

            _service?.DeleteAsync(deletingExpenseDTO?.Id).Wait();

            ExpenseDTO? deletedIncomDTO = _service?.GetByIdAsync(id).Result;

            Assert.IsNull(deletedIncomDTO);
        }
    }
}
