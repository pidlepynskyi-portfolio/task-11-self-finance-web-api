using InfrastructureApi.DTO;
using InfrastructureApi.Services;
using Microsoft.EntityFrameworkCore;
using ModelApi.Services.DataSource;
using ModelApi.Services.UnitOfWork;

namespace InfrastructureApi.Test
{
    [TestClass]
    public class TypeIncomeServiceTest
    {
        private static TypeIncomeService? _service;

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SelfFinanceDbContext>();
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=SelfFinance;Trusted_Connection=True;";
            optionsBuilder.UseSqlServer(connectionString);
            var dbContext = new SelfFinanceDbContext(optionsBuilder.Options);
            var unitOfWork = new EFUnitOfWork(dbContext);
            _service = new TypeIncomeService(unitOfWork);
        }

        [TestMethod]
        public void GetAllAsync_ResultListTypeIncome()
        {
            var typesIncomesDTO = _service?.GetAllAsync().Result;

            Assert.IsNotNull(typesIncomesDTO);
            Assert.IsTrue(typesIncomesDTO.Count() >= 1);
        }

        [TestMethod]
        public void GetByIdAsync_ResultTypeIncomeById()
        {
            int expectedId = 4;
            string expectedName = "money transfer";
            string expectedDescription = "money transfer of ";
            DateTime expectedCreateDate = DateTime.Parse("2024-05-18 22:23:09.880");

            var typeIncomeDTO = _service?.GetByIdAsync(4).Result;

            Assert.IsNotNull(typeIncomeDTO);
            Assert.AreEqual(expectedId, typeIncomeDTO.Id);
            Assert.AreEqual(expectedName, typeIncomeDTO.Name);
            Assert.AreEqual(expectedDescription, typeIncomeDTO.Description);
            Assert.IsTrue(expectedCreateDate == typeIncomeDTO.CreateDate);
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
        public void CreateAsync_ResultCreatedTypeIncome()
        {
            TypeIncomesDTO insertedObject = null!;
            int? lastId = _service?.GetAllAsync().Result.LastOrDefault()?.Id;
            TypeIncomesDTO createingTypeIncomeDTO = new TypeIncomesDTO()
            {
                Name = "test",
                Description = "test",
            };

            _service?.CreateAsync(createingTypeIncomeDTO).Wait();

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
        public void UpdateAsync_ResultEditedTypeIncome()
        {
            TypeIncomesDTO? editingTypeIncomeDTO = _service?.GetAllAsync().Result.LastOrDefault();
            string? nameBeforeEdit = editingTypeIncomeDTO?.Name;
            string? descriptionBeforeEdit = editingTypeIncomeDTO?.Description;

            editingTypeIncomeDTO!.Name = "test_edit";
            editingTypeIncomeDTO!.Description = "test_edit";

            _service?.UpdateAsync(editingTypeIncomeDTO.Id, editingTypeIncomeDTO).Wait();

            TypeIncomesDTO? editedTypeIncomeDTO = _service?.GetByIdAsync(editingTypeIncomeDTO.Id).Result;

            Assert.IsNotNull(editedTypeIncomeDTO);
            Assert.AreNotEqual(nameBeforeEdit, editedTypeIncomeDTO.Name);
            Assert.AreNotEqual(descriptionBeforeEdit, editedTypeIncomeDTO.Description);
        }

        [TestMethod]
        public void DeleteAsync_CheckParamForNull_ThrowArgumentNullException()
        {
            Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _service?.DeleteAsync(null!));
        }

        [TestMethod]
        public void DeleteAsync_ResultNull()
        {
            TypeIncomesDTO? deletingTypeIncomeDTO = _service?.GetAllAsync().Result.LastOrDefault();
            int? id = deletingTypeIncomeDTO?.Id;

            _service?.DeleteAsync(deletingTypeIncomeDTO?.Id).Wait();

            TypeIncomesDTO? deletedTypeIncomDTO = _service?.GetByIdAsync(id).Result;

            Assert.IsNull(deletedTypeIncomDTO);
        }
    }
}