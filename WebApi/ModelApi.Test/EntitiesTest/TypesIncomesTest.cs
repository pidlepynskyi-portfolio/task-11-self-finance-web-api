using ModelApi.Entities;

namespace ModelApi.Test.EntitiesTest
{
    [TestClass]
    public class TypesIncomesTest
    {
        [TestMethod]
        public void WhenTheRequiredParamsAreNull_ShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new TypeIncome(null!, null));
        }
    }
}