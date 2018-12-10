using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Injection.Factory
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void ShortSignature()
        {
            Container.RegisterType<IService>(Execute.Factory((c, t, n) => new Service()));

            var service = Container.Resolve<IService>();

            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void LongSignature()
        {
            Container.RegisterType<IService>(Execute.Factory(c => new Service()));

            var service = Container.Resolve<IService>();

            Assert.IsNotNull(service);
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void ShortSignatureThrowsOnResolvedNull()
        {
            Container.RegisterType<IService>(Execute.Factory(c => null));

            Container.Resolve<IService>();
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void LongSignatureThrowsOnResolvedNull()
        {
            Container.RegisterType<IService>(Execute.Factory((c, t, n) => null));

            Container.Resolve<IService>();
        }
    }
}
