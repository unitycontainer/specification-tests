using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unity.Specification.Diagnostic.Lifetime
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void Singleton_Instance_Null()
        {
            // Arrange
            Container.RegisterInstance(typeof(IService), null, null, InstanceLifetime.Singleton);

            // Act
            var instance = Container.Resolve<IService>();

            // Validate
            Assert.IsNull(instance);
            Assert.IsNull(Container.Resolve<IService>());
        }

        [TestMethod]
        public void Singleton_Factory_Null()
        {
            // Arrange
            Container.RegisterFactory<IService>(c => null, FactoryLifetime.Singleton);

            // Act
            var instance = Container.Resolve<IService>();

            // Validate
            Assert.IsNull(instance);
            Assert.IsNull(Container.Resolve<IService>());
        }
    }
}
