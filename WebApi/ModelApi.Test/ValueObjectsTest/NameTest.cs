using ModelApi.ValueObjects;

namespace ModelApi.Test.ValueObjectsTest
{
    [TestClass]
    public class NameTest
    {
        [TestMethod]
        public void WhenTheRequiredConstructorParamsAreNull_ShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new Name(null));
            Assert.ThrowsException<ArgumentNullException>(() => new Name(""));
            Assert.ThrowsException<ArgumentNullException>(() => new Name(" "));
        }

        [TestMethod]
        public void WhenTheRequiredConstructorParamsAreNotValid_ShouldThrowArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() => new Name("kjh$"));
        }
    }
}
