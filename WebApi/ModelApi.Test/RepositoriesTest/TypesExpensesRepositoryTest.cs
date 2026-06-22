using Microsoft.EntityFrameworkCore;
using ModelApi.Entities;
using ModelApi.Interfaces;
using ModelApi.Services.DataSource;
using ModelApi.Services.UnitOfWork;
using ModelApi.ValueObjects;

namespace ModelApi.Test.RepositoriesTest
{
    [TestClass]
    public class TypesExpensesRepositoryTest
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
        public void GetTypeExpenseById()
        {
            int id = 1;
            string name = "communal payments";
            string desc = "communal payments";
            DateTime createdDateTime = DateTime.Parse("2024-05-19 00:22:05.680");

            TypeExpense? typeExpense = _unitOfWork?.TypesExpenses.GetByIdAsync(id).Result;

            Assert.IsNotNull(typeExpense);
            Assert.AreEqual(id, typeExpense.Id);
            Assert.AreEqual(name, typeExpense.Name.Value);
            Assert.AreEqual(desc, typeExpense.Description!.Value);
            Assert.IsTrue(createdDateTime.ToString() == typeExpense.CreateDate.Value.ToString());
        }

        [TestMethod]
        public void GetNullTypeIncomeById()
        {
            int id = 200;

            TypeExpense? typeExpense = _unitOfWork?.TypesExpenses.GetByIdAsync(id).Result;

            Assert.IsNull(typeExpense);
        }

        [TestMethod]
        public void GetAllTypesExpenses()
        {
            IEnumerable<TypeExpense>? listTypesExpenses = _unitOfWork?.TypesExpenses.GetAllAsync().Result;

            Assert.IsNotNull(listTypesExpenses);
            Assert.IsTrue(listTypesExpenses.Count() >= 3);
        }

        [TestMethod]
        public void GetNullTypesExpensesWithFilter()
        {
            IEnumerable<TypeExpense>? listTypesExpenses = _unitOfWork?.TypesExpenses.GetWithFilterByIdAsync(100).Result;

            Assert.IsNotNull(listTypesExpenses);
            Assert.IsTrue(listTypesExpenses.Count() == 0);
        }

        [TestMethod]
        public void CreateTypeExpense()
        {
            int? lastId = _unitOfWork?.TypesExpenses.GetAllAsync().Result.Last().Id;
            TypeExpense? createTypeExpense = new TypeExpense(new Name("test"), null);

            _unitOfWork?.TypesExpenses.CreateAsync(createTypeExpense);
            _unitOfWork?.SaveAsync();
            TypeExpense? insertedTypeExpense = _unitOfWork?.TypesExpenses.GetByIdAsync(lastId + 1).Result;

            Assert.IsNotNull(insertedTypeExpense);
            Assert.IsTrue(insertedTypeExpense == createTypeExpense);
        }

        [TestMethod]
        public void UpdateTypeExpense()
        {
            TypeExpense? editingTypeExpense = _unitOfWork?.TypesExpenses.GetByIdAsync(6).Result;
            FreeText? descBeforeEdit = editingTypeExpense?.Description;

            editingTypeExpense?.Change(new Name("name3_edit1"), new FreeText("desc3_edit1"));
            _unitOfWork?.TypesExpenses.Update(editingTypeExpense!);
            _unitOfWork?.SaveAsync().Wait();
            TypeExpense? editedTypeExpense = _unitOfWork?.TypesExpenses.GetByIdAsync(3).Result;
            FreeText? descAfterEdit = editingTypeExpense?.Description;

            Assert.IsNotNull(editedTypeExpense);
            Assert.IsTrue(descAfterEdit! != descBeforeEdit!);
        }

        [TestMethod]
        public void DeleteTypeExpense()
        {
            TypeExpense? deletingTypeExpense = _unitOfWork?.TypesExpenses.GetByIdAsync(6).Result;

            _unitOfWork?.TypesExpenses.Delete(deletingTypeExpense!);
            _unitOfWork?.SaveAsync().Wait();
            TypeExpense? deletedTypeExpense = _unitOfWork?.TypesExpenses.GetByIdAsync(6).Result;

            Assert.IsNull(deletedTypeExpense);
        }
    }
}
