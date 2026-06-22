using InfrastructureApi.Services;

namespace InfrastructureApi.Test
{
    [TestClass]
    public class ModelServiceTest
    {
        [TestMethod]
        public void ConstructorModelService_ThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new TypeIncomeService(null!));
        }
    }
}
