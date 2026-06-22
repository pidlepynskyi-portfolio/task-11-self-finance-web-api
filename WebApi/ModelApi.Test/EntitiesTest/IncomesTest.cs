using ModelApi.Entities;
using ModelApi.ValueObjects;

namespace ModelApi.Test.EntitiesTest
{
    [TestClass]
    public class IncomesTest
    {
        [TestMethod]
        public void WhenTheRequiredConstructorParamsAreNull_ShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new Income(null!, null, null));
            Assert.ThrowsException<ArgumentNullException>(() => new Income(null!, 1, null));
            Assert.ThrowsException<ArgumentNullException>(() => new Income(new Amount(3200), null, new FreeText("sdfs")));
        }

        [TestMethod]
        public void WhenTheRequiredConstructorParamsAreNotValid_ShouldThrowArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() => new Income(new Amount(3200), 0, null));
            Assert.ThrowsException<ArgumentException>(() => new Income(new Amount(3200), -1, null));
        }

        [TestMethod]
        public void WhenTheRequiredChangeMethodParamsAreNotValid_ShouldThrowArgumentException()
        {
            var dataIncomes = new Income(new Amount(3200), 1, null);

            Assert.ThrowsException<ArgumentException>(() => dataIncomes.Change(new Amount(3200), 0, null));
            Assert.ThrowsException<ArgumentException>(() => dataIncomes.Change(new Amount(3200), -1, null));
        }
    }
}
