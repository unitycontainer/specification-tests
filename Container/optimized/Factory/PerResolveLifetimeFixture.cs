using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Lifetime
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void PerResolveLifetimeIsHonoredWhenUsingFactory()
        {
            Container.RegisterFactory<SomeService>(c => new SomeService(), FactoryLifetime.PerResolve);

            var rootService = Container.Resolve<AService>();
            Assert.AreSame(rootService.SomeService, rootService.OtherService.SomeService);
        }
    }
}
