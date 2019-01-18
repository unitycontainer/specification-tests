using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Factory.Resolution
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void ShortSignature()
        {
            Container.RegisterFactory<IService>((c, t, n) => new Service());

            var service = Container.Resolve<IService>();

            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void LongSignature()
        {
            Container.RegisterFactory<IService>(c => new Service());

            var service = Container.Resolve<IService>();

            Assert.IsNotNull(service);
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void ShortSignatureThrowsOnResolvedNull()
        {
            Container.RegisterFactory<IService>(c => null);

            Container.Resolve<IService>();
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void LongSignatureThrowsOnResolvedNull()
        {
            Container.RegisterFactory<IService>((c, t, n) => null);

            Container.Resolve<IService>();
        }
    }
}
