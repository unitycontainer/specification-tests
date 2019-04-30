using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Unity.Lifetime;

namespace Unity.Specification.Registration.Types
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Null_Null_Null()
        {
            // Act
            Container.RegisterType(null, null, null, null);
        }

        [TestMethod]
        public void Type_Null_Null()
        {
            // Act
            Container.RegisterType(typeof(object), null, null, null);
        }

        [TestMethod]
        public void Type_Type_Null()
        {
            // Act
            Container.RegisterType(typeof(object), typeof(object), null, null);
        }

        [TestMethod]
        public void Null_Type_Null()
        {
            // Act
            Container.RegisterType(typeof(object), null, null, null);
        }

        [TestMethod]
        public void Type_Type_Name()
        {
            // Act
            Container.RegisterType(typeof(object), typeof(object), Name, null);
        }


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
