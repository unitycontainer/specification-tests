using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Unity.Specification.Registration.Types
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void ChainRegistrations()
        {
            // Arrange
            var instance = new Service();

            Container.RegisterInstance(instance);
            Container.RegisterType<IService, Service>();

            // Act/Validate
            Assert.AreEqual(Container.Resolve<IService>(), instance);
        }


        [TestMethod]
        public void Registration_ShowsUpInRegistrationsSequence()
        {
            // Arrange
            Container.RegisterInstance(Name);
            Container.RegisterType<ILogger, MockLogger>();
            Container.RegisterType<ILogger, MockLogger>(Name);

            var service = new Service();
            Container.RegisterInstance<IService>(service);
            Container.RegisterInstance<IService>(Name, service);

            Container.RegisterType(typeof(IFoo<>), typeof(Foo<>));
            Container.RegisterType(typeof(IFoo<>), typeof(Foo<>), Name);

            var registrations = (from r in Container.Registrations
                                 where r.RegisteredType == typeof(ILogger)
                                 select r).ToList();

            Assert.AreEqual(2, registrations.Count);

            Assert.IsTrue(registrations.Any(r => r.Name == null));
            Assert.IsTrue(registrations.Any(r => r.Name == Name));
        }

        [TestMethod]
        public void TypeMappingShowsUpInRegistrationsCorrectly()
        {
            // Arrange
            Container.RegisterInstance(Name);

            Container.RegisterType<ILogger, MockLogger>();
            Container.RegisterType<ILogger, MockLogger>(Name);

            var service = new Service();
            Container.RegisterInstance<IService>(service);
            Container.RegisterInstance<IService>(Name, service);

            Container.RegisterType(typeof(IFoo<>), typeof(Foo<>));
            Container.RegisterType(typeof(IFoo<>), typeof(Foo<>), Name);

            var registration =
                (from r in Container.Registrations
                 where r.RegisteredType == typeof(ILogger)
                 select r).First();

            Assert.AreSame(typeof(MockLogger), registration.MappedToType);
        }

        [TestMethod]
        public void NonMappingRegistrationShowsUpInRegistrationsSequence()
        {
            Container.RegisterType<MockLogger>();
            var registration = (from r in Container.Registrations
                                where r.RegisteredType == typeof(MockLogger)
                                select r).First();

            Assert.AreSame(registration.RegisteredType, registration.MappedToType);
            Assert.IsNull(registration.Name);
        }

        [TestMethod]
        public void Registration_OfOpenGenericTypeShowsUpInRegistrationsSequence()
        {
            Container.RegisterType(typeof(IDictionary<,>), typeof(Dictionary<,>), "test");
            var registration = Container.Registrations.First(r => r.RegisteredType == typeof(IDictionary<,>));

            Assert.AreSame(typeof(Dictionary<,>), registration.MappedToType);
            Assert.AreEqual("test", registration.Name);
        }

        [TestMethod]
        public void Registration_InParentContainerAppearInChild()
        {
            Container.RegisterType<ILogger, MockLogger>();
            var child = Container.CreateChildContainer();

            var registration =
                (from r in child.Registrations where r.RegisteredType == typeof(ILogger) select r).First();

            Assert.AreSame(typeof(MockLogger), registration.MappedToType);
        }

        [TestMethod]
        public void Registration_InChildContainerDoNotAppearInParent()
        {
            var local = "local";
            var child = Container.CreateChildContainer()
                .RegisterType<ILogger, MockLogger>(local);

            var childRegistration = child.Registrations
                                         .First(r => r.RegisteredType == typeof(ILogger) &&
                                                     r.Name == local);

            var parentRegistration = Container.Registrations
                                               .FirstOrDefault(r => r.RegisteredType == typeof(ILogger) &&
                                                                    r.Name == local);
            Assert.IsNull(parentRegistration);
            Assert.IsNotNull(childRegistration);
        }

        [TestMethod]
        public void Registration_InParentAndChildOnlyShowUpOnceInChild()
        {
            var child = Container.CreateChildContainer();
            child.RegisterType<IService, OtherService>(Name);

            var registrations = from r in child.Registrations
                                where r.RegisteredType == typeof(IService) & r.Name == Name
                                select r;

            var containerRegistrations = registrations.ToArray();

            Assert.AreEqual(1, containerRegistrations.Count());

            var childRegistration = containerRegistrations.First();
            Assert.AreSame(typeof(OtherService), childRegistration.MappedToType);
            Assert.AreEqual(Name, childRegistration.Name);
        }
    }
}
