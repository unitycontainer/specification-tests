using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Unity.Lifetime;

namespace Unity.Specification.Registration.Extended
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void Type()
        {
            // Setup
            Container.RegisterType<object>();

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
            Container.RegisterType<object>(Name);

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
            Container.RegisterType<object>(TypeLifetime.PerContainer);

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
            Container.RegisterType<object>(Name, TypeLifetime.PerContainer);

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
            Container.RegisterType<IService, Service>();

            // Act
            var registration = Container.Registrations
                                        .FirstOrDefault(r => typeof(IService) == r.RegisteredType);

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
            Container.RegisterType<IService, Service>(Name);

            // Act
            var registration = Container.Registrations
                                        .FirstOrDefault(r => typeof(IService) == r.RegisteredType);

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
            Container.RegisterType<IService, Service>(TypeLifetime.PerContainer);

            // Act
            var registration = Container.Registrations
                                        .FirstOrDefault(r => typeof(IService) == r.RegisteredType);

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
            Container.RegisterType<IService, Service>(Name, TypeLifetime.PerContainer);

            // Act
            var registration = Container.Registrations
                                        .FirstOrDefault(r => typeof(IService) == r.RegisteredType);

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
            Container.RegisterType(null, typeof(TypeWithAmbiguousCtors), null, null, Invoke.Constructor());

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
            Container.RegisterType(typeof(TypeWithAmbiguousCtors), Invoke.Constructor("1", "2", "3"));

            // Act
            var result = Container.Resolve<TypeWithAmbiguousCtors>();

            // Validate
            Assert.IsNotNull(result);
            Assert.AreEqual(TypeWithAmbiguousCtors.Four, result.Signature);
        }
    }
}
