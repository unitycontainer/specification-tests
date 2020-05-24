using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Unity.Injection;

namespace Unity.Specification.Factory.Registration
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void FactoryOpenGeneric()
        {
            // Arrange
            Container.RegisterFactory(typeof(IFoo<>), (c, t, n) => new Foo<object>());

            // Act
            var result = Container.Resolve(typeof(IFoo<object>));

            // Verify
            Assert.IsNotNull(result);
        }

        [Obsolete]
        [TestMethod]
        public void FactoryOpenGenericLegacy()
        {
            // Arrange
            Container.RegisterType(typeof(IFoo<>), new InjectionFactory((c, t, n) => new Foo<object>()));

            // Act
            var result = Container.Resolve(typeof(IFoo<object>));

            // Verify
            Assert.IsNotNull(result);
        }
    }
}
