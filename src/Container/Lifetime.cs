using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Container
{
    public abstract partial class SpecificationTests : TestFixtureBase
    {
        [TestMethod]
        public void SpecificationTests_Lifetiem_IUnityContainer()
        {
            IUnityContainer container = GetContainer();
            Assert.IsNotNull(container.Resolve<IUnityContainer>());
        }
    }
}
