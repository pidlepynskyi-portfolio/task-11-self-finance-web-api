using InfrastructureApi.DTO;
using InfrastructureApi.Services;
using Microsoft.EntityFrameworkCore;
using ModelApi.Services.DataSource;
using ModelApi.Services.UnitOfWork;

namespace InfrastructureApi.Test
{
    [TestClass]
    public class IncomeServiceTest
    {
        private static IncomeService? _service;

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SelfFinanceDbContext>();
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=SelfFinance;Trusted_Connection=True;";
            optionsBuilder.UseSqlServer(connectionString);
            var dbContext = new SelfFinanceDbContext(optionsBuilder.Options);
            var unitOfWork = new EFUnitOfWork(dbContext);
            _service = new IncomeService(unitOfWork);
        }

        [TestMethod]
        public void GetAllAsync_ReturnObjectIEnumerableOfIncomeDTO()
        {
            var listIncomeDTO = _service?.GetAllAsync().Result;

            Assert.IsNotNull(listIncomeDTO);
            Assert.IsTrue(listIncomeDTO.Count() >= 1);
        }

        [TestMethod]
        public void GetByIdAsync_CheckParamForNull_ThrowArgumentNullException()
        {
            Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _service?.GetByIdAsync(null));
        }

        [TestMethod]
        public void GetByIdAsync_ReturnObjectIncome()
        {
            int expectedId = 1;
            double expectedAmount = 4364.39;
            DateTime expectedCreateDate = DateTime.Parse("2024-03-10 00:00:00.000");
            int expectedTypeIncomeId = 1;

            var incomeDTO = _service?.GetByIdAsync(1).Result;

            Assert.IsNotNull(incomeDTO);
            Assert.AreEqual(expectedId, incomeDTO.Id);
            Assert.AreEqual(expectedAmount, incomeDTO.Amount);
            Assert.AreEqual(expectedTypeIncomeId, incomeDTO.TypeId);
            Assert.AreEqual(expectedCreateDate, incomeDTO.CreateDate);
        }

        [TestMethod]
        public void GetSumByEnterDateAsync_ReturnSumAmount()
        {
            double expectedSumAmount = 2500.55;

            var actualSumAmount = _service?.GetSumByEnterDateAsync(DateTime.Parse("2024-07-24")).Result;

            Assert.IsNotNull(actualSumAmount);
            Assert.AreEqual(expectedSumAmount, actualSumAmount);
        }

        [TestMethod]
        public void GetSumByPeriodAsync_ReturnSumAmount()
        {
            double expectedSumAmount = 7869.69;
            DateTime fromDate = DateTime.Parse("2024-03-01");
            DateTime toDate = DateTime.Parse("2024-03-31");

            var actualSumAmount = _service?.GetSumByPeriodAsync(fromDate, toDate).Result;

            Assert.IsNotNull(actualSumAmount);
            Assert.AreEqual(expectedSumAmount, actualSumAmount);
        }

        [TestMethod]
        public void GetByEnterDateAsync_ReturnObjectIEnumerableOfIncomeDTO()
        {
            int expectedFirsId = 17;
            int expectedLastId = 18;

            var listIncomeDTO = _service?.GetByEnterDateAsync(DateTime.Parse("2024-07-24")).Result;
            var firstIncomeDTO = listIncomeDTO?.FirstOrDefault();
            var lastIncomeDTO = listIncomeDTO?.LastOrDefault();

            Assert.IsNotNull(listIncomeDTO);
            Assert.AreEqual(expectedFirsId, firstIncomeDTO?.Id);
            Assert.AreEqual(expectedLastId, lastIncomeDTO?.Id);
        }

        [TestMethod]
        public void GetByPeriodAsync_ReturnObjectIEnumerableOfIncomeDTO()
        {
            int expectedFirsId = 1;
            int expectedLastId = 5;
            DateTime fromDate = DateTime.Parse("2024-03-01");
            DateTime toDate = DateTime.Parse("2024-03-31");

            var listIncomeDTO = _service?.GetByPeriodAsync(fromDate, toDate).Result;
            var firstIncomeDTO = listIncomeDTO?.FirstOrDefault();
            var lastIncomeDTO = listIncomeDTO?.LastOrDefault();

            Assert.IsNotNull(listIncomeDTO);
            Assert.AreEqual(expectedFirsId, firstIncomeDTO?.Id);
            Assert.AreEqual(expectedLastId, lastIncomeDTO?.Id);
        }

        [TestMethod]
        public void CreateAsync_CheckParamForNull_ThrowArgumentNullException()
        {
            Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _service?.CreateAsync(null!));
        }

        [TestMethod]
        public void CreateAsync_ResultCreatedAndInsertObjectIncome()
        {
            IncomeDTO insertedObject = null!;
            int? lastId = _service?.GetAllAsync().Result.LastOrDefault()?.Id;
            IncomeDTO createingIncomeDTO = new IncomeDTO()
            {
                Amount = 1000,
                TypeId = 4,
                Comments = "test_comments"
            };

            _service?.CreateAsync(createingIncomeDTO).Wait();

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
        public void UpdateAsync_ResultEditedObjectIncome()
        {
            IncomeDTO? editingIncomeDTO = _service?.GetAllAsync().Result.LastOrDefault();
            double? amountBeforeEdit = editingIncomeDTO?.Amount;
            string? commentsBeforeEdit = editingIncomeDTO?.Comments;

            editingIncomeDTO!.Amount = 2000;
            editingIncomeDTO!.Comments = "test_edit";

            _service?.UpdateAsync(editingIncomeDTO.Id, editingIncomeDTO).Wait();

            IncomeDTO? editedIncomeDTO = _service?.GetByIdAsync(editingIncomeDTO.Id).Result;

            Assert.IsNotNull(editedIncomeDTO);
            Assert.AreNotEqual(amountBeforeEdit, editedIncomeDTO.Amount);
            Assert.AreNotEqual(commentsBeforeEdit, editedIncomeDTO.Comments);
        }

        [TestMethod]
        public void DeleteAsync_CheckParamForNull_ThrowArgumentNullException()
        {
            Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _service?.DeleteAsync(null!));
        }

        [TestMethod]
        public void DeleteAsync_ResultNull()
        {
            IncomeDTO? deletingIncomeDTO = _service?.GetAllAsync().Result.LastOrDefault();
            int? id = deletingIncomeDTO?.Id;

            _service?.DeleteAsync(deletingIncomeDTO?.Id).Wait();

            IncomeDTO? deletedIncomDTO = _service?.GetByIdAsync(id).Result;

            Assert.IsNull(deletedIncomDTO);
        }
    }
}
