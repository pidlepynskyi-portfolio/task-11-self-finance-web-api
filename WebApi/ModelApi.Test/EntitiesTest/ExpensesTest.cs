using ModelApi.Entities;
using ModelApi.ValueObjects;

namespace ModelApi.Test.EntitiesTest
{
    [TestClass]
    public class ExpensesTest
    {
        [TestMethod]
        public void WhenTheRequiredConstructorParamsAreNull_ShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new Expense(null!, null, null));
            Assert.ThrowsException<ArgumentNullException>(() => new Expense(null!, 1, null));
            Assert.ThrowsException<ArgumentNullException>(() => new Expense(new Amount(3200), null, new FreeText("sdfs")));
        }

        [TestMethod]
        public void WhenTheRequiredConstructorParamsAreNotValid_ShouldThrowArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() => new Expense(new Amount(3200), 0, null));
            Assert.ThrowsException<ArgumentException>(() => new Expense(new Amount(3200), -1, null));
        }

        [TestMethod]
        public void WhenTheRequiredChangeMethodParamsAreNotValid_ShouldThrowArgumentException()
        {
            var dataExpenses = new Expense(new Amount(3200), 1, null);

            Assert.ThrowsException<ArgumentException>(() => dataExpenses.Change(new Amount(3200), 0, null));
            Assert.ThrowsException<ArgumentException>(() => dataExpenses.Change(new Amount(3200), -1, null));
        }
    }
}
