using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Exceptions;

namespace Unity.Specification.Injection.Factory
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void ShortSignature()
        {
            Container.RegisterType<IService>(Unity.Injection.Factory((c, t, n) => new Service()));

            var service = Container.Resolve<IService>();

            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void LongSignature()
        {
            Container.RegisterType<IService>(Unity.Injection.Factory(c => new Service()));

            var service = Container.Resolve<IService>();

            Assert.IsNotNull(service);
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void ShortSignatureThrowsOnNull()
        {
            Container.RegisterType<IService>(Unity.Injection.Factory(c => null));

            Container.Resolve<IService>();
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void LongSignatureThrowsOnNull()
        {
            Container.RegisterType<IService>(Unity.Injection.Factory((c, t, n) => null));

            Container.Resolve<IService>();
        }
    }
}
