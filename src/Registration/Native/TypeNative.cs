using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Unity.Injection;
using Unity.Lifetime;
using Unity.Specification.TestData;

namespace Unity.Specification.Registration.Native
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void RegistrationsToArray()
        {
            // Setup
            Container.RegisterType(null, typeof(object), null, null);

            // Act
            var registrations = Container.Registrations.ToArray();

            // Validate
            Assert.IsNotNull(registrations);
        }

        [TestMethod]
        public void Type()
        {
            // Setup
            Container.RegisterType(null, typeof(object), null, null);

            // Act
            var registration = Container.Registrations
                                        .FirstOrDefault(r => typeof(object) == r.RegisteredType);

            // Validate
            Assert.IsNotNull(registration);
            Assert.IsNull(registration.Name);
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(TransientLifetimeManager));
        }

        [TestMethod]
        public void Named()
        {
            // Setup
            Container.RegisterType(null, typeof(object), Name, null);

            // Act
            var registration = Container.Registrations
                                        .FirstOrDefault(r => typeof(object) == r.RegisteredType);

            // Validate
            Assert.IsNotNull(registration);
            Assert.AreEqual(Name, registration.Name);
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(TransientLifetimeManager));
        }

        [TestMethod]
        public void WithLifetime()
        {
            // Setup
            Container.RegisterType(null, typeof(object), null, new ContainerControlledLifetimeManager());

            // Act
            var registration = Container.Registrations
                                        .FirstOrDefault(r => typeof(object) == r.RegisteredType);

            // Validate
            Assert.IsNotNull(registration);
            Assert.IsNull(registration.Name);
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(ContainerControlledLifetimeManager));
        }

        [TestMethod]
        public void NamedWithLifetime()
        {
            // Setup
            Container.RegisterType(null, typeof(object), Name, new ContainerControlledLifetimeManager());

            // Act
            var registration = Container.Registrations
                                        .FirstOrDefault(r => typeof(object) == r.RegisteredType);

            // Validate
            Assert.IsNotNull(registration);
            Assert.AreEqual(Name, registration.Name);
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(ContainerControlledLifetimeManager));
        }


        [TestMethod]
        public void MappedType()
        {
            // Setup
            Container.RegisterType(typeof(IService), typeof(Service), null, null);

            // Act
            var registration = Container.Registrations
                                        .Where(r => r.RegisteredType == typeof(IService))
                                        .First();

            // Validate
            Assert.IsNotNull(registration);
            Assert.IsNull(registration.Name);
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(TransientLifetimeManager));
            Assert.AreEqual(registration.RegisteredType, typeof(IService));
            Assert.AreEqual(registration.MappedToType, typeof(Service));
        }

        [TestMethod]
        public void MappedNamed()
        {
            // Setup
            Container.RegisterType(typeof(IService), typeof(Service), Name, null);

            // Act
            var registration = Container.Registrations
                                        .Where(r => r.RegisteredType == typeof(IService))
                                        .First();

            // Validate
            Assert.IsNotNull(registration);
            Assert.AreEqual(Name, registration.Name);
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(TransientLifetimeManager));
            Assert.AreEqual(registration.RegisteredType, typeof(IService));
            Assert.AreEqual(registration.MappedToType, typeof(Service));
        }

        [TestMethod]
        public void MappedWithLifetime()
        {
            // Setup
            Container.RegisterType(typeof(IService), typeof(Service), null, new ContainerControlledLifetimeManager());

            // Act
            var registration = Container.Registrations
                                        .Where(r => r.RegisteredType == typeof(IService))
                                        .First();

            // Validate
            Assert.IsNotNull(registration);
            Assert.IsNull(registration.Name);
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(ContainerControlledLifetimeManager));
            Assert.AreEqual(registration.RegisteredType, typeof(IService));
            Assert.AreEqual(registration.MappedToType, typeof(Service));
        }

        [TestMethod]
        public void MappedNamedWithLifetime()
        {
            // Setup
            Container.RegisterType(typeof(IService), typeof(Service), Name, new ContainerControlledLifetimeManager());

            // Act
            var registration = Container.Registrations
                                        .Where(r => r.RegisteredType == typeof(IService))
                                        .First();

            // Validate
            Assert.IsNotNull(registration);
            Assert.AreEqual(Name, registration.Name);
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(ContainerControlledLifetimeManager));
            Assert.AreEqual(registration.RegisteredType, typeof(IService));
            Assert.AreEqual(registration.MappedToType, typeof(Service));
        }

        [TestMethod]
        public void Constructor()
        {
            // Setup
            Container.RegisterType(null, typeof(TypeWithAmbiguousCtors), null, null, new InjectionConstructor());

            // Act
            var result = Container.Resolve<TypeWithAmbiguousCtors>();

            // Validate
            Assert.IsNotNull(result);
            Assert.AreEqual(TypeWithAmbiguousCtors.One, result.Signature);
        }

        [TestMethod]
        public void ConstructorWithData()
        {
            // Setup
            Container.RegisterType(null, typeof(TypeWithAmbiguousCtors), null, null, new InjectionConstructor("1", "2", "3"));

            // Act
            var result = Container.Resolve<TypeWithAmbiguousCtors>();

            // Validate
            Assert.IsNotNull(result);
            Assert.AreEqual(TypeWithAmbiguousCtors.Four, result.Signature);
        }
    }
}
