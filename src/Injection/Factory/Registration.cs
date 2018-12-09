using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Injection.Factory
{
    public abstract partial class SpecificationTests
    {

        [TestMethod]
        public void Factory_IsNotNull()
        {
            Container.RegisterType<IService>(Unity.Injection.Factory((c, t, n) => new Service()));

            Assert.IsNotNull(Container.Resolve<IService>());
        }
    }
}
