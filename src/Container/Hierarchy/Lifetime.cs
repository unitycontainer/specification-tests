using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Container.Hierarchy
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void Lifetieme_IUnityContainer()
        {
            IUnityContainer container = GetContainer();
            Assert.IsNotNull(container.Resolve<IUnityContainer>());
        }
    }
}
