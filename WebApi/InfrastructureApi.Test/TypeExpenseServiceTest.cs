using InfrastructureApi.DTO;
using InfrastructureApi.Services;
using Microsoft.EntityFrameworkCore;
using ModelApi.Services.DataSource;
using ModelApi.Services.UnitOfWork;

namespace InfrastructureApi.Test
{
    [TestClass]
    public class TypeExpenseServiceTest
    {
        private static TypeExpenseService? _service;

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SelfFinanceDbContext>();
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=SelfFinance;Trusted_Connection=True;";
            optionsBuilder.UseSqlServer(connectionString);
            var dbContext = new SelfFinanceDbContext(optionsBuilder.Options);
            var unitOfWork = new EFUnitOfWork(dbContext);
            _service = new TypeExpenseService(unitOfWork);
        }

        [TestMethod]
        public void GetAllAsync_ResultListTypeExpense()
        {
            var typesExpensesDTO = _service?.GetAllAsync().Result;

            Assert.IsNotNull(typesExpensesDTO);
            Assert.IsTrue(typesExpensesDTO.Count() >= 1);
        }

        [TestMethod]
        public void GetByIdAsync_ResultTypeExpenseById()
        {
            int expectedId = 5;
            string expectedName = "money transfer";
            string expectedDescription = "money transfer to ";
            DateTime expectedCreateDate = DateTime.Parse("2024-05-19 00:22:05.680");

            var typeExpenseDTO = _service?.GetByIdAsync(5).Result;

            Assert.IsNotNull(typeExpenseDTO);
            Assert.AreEqual(expectedId, typeExpenseDTO.Id);
            Assert.AreEqual(expectedName, typeExpenseDTO.Name);
            Assert.AreEqual(expectedDescription, typeExpenseDTO.Description);
            Assert.IsTrue(expectedCreateDate == typeExpenseDTO.CreateDate);
        }

        [TestMethod]
        public void GetByIdAsync_CheckParamForNull_ThrowArgumentNullException()
        {
            Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _service?.GetByIdAsync(null));
        }

        [TestMethod]
        public void CreateAsync_CheckParamForNull_ThrowArgumentNullException()
        {
            Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _service?.CreateAsync(null!));
        }

        [TestMethod]
        public void CreateAsync_ResultCreatedTypeExpense()
        {
            TypeExpensesDTO insertedObject = null!;
            int? lastId = _service?.GetAllAsync().Result.LastOrDefault()?.Id;
            TypeExpensesDTO createingTypeExpenseDTO = new TypeExpensesDTO()
            {
                Name = "test",
                Description = "test",
            };

            _service?.CreateAsync(createingTypeExpenseDTO).Wait();

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
        public void UpdateAsync_ResultEditedTypeExpense()
        {
            TypeExpensesDTO? editingTypeExpenseDTO = _service?.GetAllAsync().Result.LastOrDefault();
            string? nameBeforeEdit = editingTypeExpenseDTO?.Name;
            string? descriptionBeforeEdit = editingTypeExpenseDTO?.Description;

            editingTypeExpenseDTO!.Name = "test_edit";
            editingTypeExpenseDTO!.Description = "test_edit";

            _service?.UpdateAsync(editingTypeExpenseDTO.Id, editingTypeExpenseDTO).Wait();

            TypeExpensesDTO? editedTypeExpenseDTO = _service?.GetByIdAsync(editingTypeExpenseDTO.Id).Result;

            Assert.IsNotNull(editedTypeExpenseDTO);
            Assert.AreNotEqual(nameBeforeEdit, editedTypeExpenseDTO.Name);
            Assert.AreNotEqual(descriptionBeforeEdit, editedTypeExpenseDTO.Description);
        }

        [TestMethod]
        public void DeleteAsync_CheckParamForNull_ThrowArgumentNullException()
        {
            Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _service?.DeleteAsync(null!));
        }

        [TestMethod]
        public void DeleteAsync_ResultNull()
        {
            TypeExpensesDTO? deletingTypeExpenseDTO = _service?.GetAllAsync().Result.LastOrDefault();
            int? id = deletingTypeExpenseDTO?.Id;

            _service?.DeleteAsync(deletingTypeExpenseDTO?.Id).Wait();

            TypeExpensesDTO? deletedTypeIncomDTO = _service?.GetByIdAsync(id).Result;

            Assert.IsNull(deletedTypeIncomDTO);
        }
    }
}
