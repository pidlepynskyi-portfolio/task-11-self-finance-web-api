using ModelApi.ValueObjects;

namespace ModelApi.Test.ValueObjectsTest
{
    [TestClass]
    public class AmountTest
    {
        [TestMethod]
        public void WhenTheRequiredConstructorParamsAreNull_ShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new Amount(null));
        }

        [TestMethod]
        public void WhenTheRequiredConstructorParamsAreNotValid_ShouldThrowArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() => new Amount(0));
            Assert.ThrowsException<ArgumentException>(() => new Amount(-1));
        }
    }
}
