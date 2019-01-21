using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Unity.Lifetime;
using Unity.Specification.TestData;

namespace Unity.Specification.Registration.Syntax
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void Type()
        {
            // Setup
            Container.Type<object>()
                     .Register();

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
            Container.Type<object>()
                     .RegisterAs(Name);

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
            Container.Type<object>()
                     .Lifetime.PerContainerLifetime()
                     .Register();

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
            Container.Type<object>()
                     .Lifetime.ContainerControlledLifetime()
                     .RegisterAs(Name);

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
            Container.Type(typeof(Service))
                     .RegisterAs(typeof(IService));

            // Act
            var registration = Container.Registrations
                                        .ElementAt(1);

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
            Container.Type<Service>()
                     .RegisterAs<IService>(Name);

            // Act
            var registration = Container.Registrations
                                        .ElementAt(1);

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
            Container.Type<Service>()
                     .Lifetime.PerContainerLifetime()
                     .RegisterAs(typeof(IService));

            // Act
            var registration = Container.Registrations
                                        .ElementAt(1);

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
            Container.Type(typeof(Service))
                     .Lifetime.ContainerControlledLifetime()
                     .RegisterAs(typeof(IService), Name);

            // Act
            var registration = Container.Registrations
                                        .ElementAt(1);

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
            Container.Type(typeof(TypeWithAmbiguousCtors))
                     .Constructor()
                     .Register();

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
            Container.Type(typeof(TypeWithAmbiguousCtors))
                     .Constructor("1", "2", "3")
                     .Register();

            // Act
            var result = Container.Resolve<TypeWithAmbiguousCtors>();

            // Validate
            Assert.IsNotNull(result);
            Assert.AreEqual(TypeWithAmbiguousCtors.Four, result.Signature);
        }
    }
}
