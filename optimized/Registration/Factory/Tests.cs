using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Lifetime;

namespace Unity.Specification.Registration.Factory
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Null_Null_Null()
        {
            // Act
            Container.RegisterFactory(null, null, null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Null_Null_Factory()
        {
            // Act
            Container.RegisterFactory(null, null, (c,t,n)=> null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Type_Null_Null()
        {
            // Act
            Container.RegisterFactory(typeof(object), null, null, null);
        }

        [TestMethod]
        public void DefaultLifetime()
        {
            // Arrange
            var value = new object();
            Container.RegisterFactory(typeof(object), null, (c, t, n) => value, null);

            // Act
            var registration = Container.Registrations.First(r => typeof(object) == r.RegisteredType);

            // Validate
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(TransientLifetimeManager));
        }

        [TestMethod]
        public void CanSetLifetime()
        {
            // Arrange
            Container.RegisterFactory(typeof(object), null, (c, t, n) => null, FactoryLifetime.Singleton);

            // Act
            var registration = Container.Registrations.First(r => typeof(object) == r.RegisteredType);

            // Validate
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(SingletonLifetimeManager));
        }

        [TestMethod]
        public void Type_Null_Factory()
        {
            // Arrange
            var value = new object();
            Container.RegisterFactory(typeof(object), null, (c, t, n) => value, null);

            // Act
            var instance = Container.Resolve<object>();

            // Validate
            Assert.AreSame(value, instance);
        }

        [TestMethod]
        public void Type_Name_Factory()
        {
            // Arrange
            var value = new object();
            Container.RegisterFactory(typeof(object), Name, (c, t, n) => value, null);

            // Act
            var instance = Container.Resolve<object>(Name);

            // Validate
            Assert.AreSame(value, instance);
        }
    }
}
