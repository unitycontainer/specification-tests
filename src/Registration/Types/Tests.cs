using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Unity.Lifetime;

namespace Unity.Specification.Registration.Types
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void DefaultLifetime()
        {
            // Arrange
            Container.RegisterType(typeof(object), null, null, null);

            // Act
            var registration = Container.Registrations.First(r => typeof(object) == r.RegisteredType);

            // Validate
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(TransientLifetimeManager));
        }

        [TestMethod]
        public void CanSetLifetime()
        {
            // Arrange
            Container.RegisterType(typeof(object), null, null, TypeLifetime.Singleton);

            // Act
            var registration = Container.Registrations.First(r => typeof(object) == r.RegisteredType);

            // Validate
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(SingletonLifetimeManager));
        }

    }
}
