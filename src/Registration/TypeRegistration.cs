using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Specification.TestData;

namespace Unity.Specification.Registration
{
    public abstract partial class SpecificationTests 
    {
        [TestMethod]
        public void Specification_Registration_ListsItselfAsRegistered()
        {
            Assert.IsTrue(_container.IsRegistered(typeof(IUnityContainer)));
        }

        [TestMethod]
        public void Specification_Registration_ContainerDoesNotListItselfUnderNonDefaultName()
        {
            Assert.IsFalse(_container.IsRegistered(typeof(IUnityContainer), "other"));
        }

        [TestMethod]
        public void Specification_Registration_ContainerListsItselfAsRegisteredUsingGenericOverload()
        {
            Assert.IsTrue(_container.IsRegistered<IUnityContainer>());
        }

        [TestMethod]
        public void Specification_Registration_ContainerDoesNotListItselfUnderNonDefaultNameUsingGenericOverload()
        {
            Assert.IsFalse(_container.IsRegistered<IUnityContainer>("other"));
        }

        [TestMethod]
        public void Specification_Registration_IsRegisteredWorksForRegisteredType()
        {
            _container.RegisterType<ILogger, MockLogger>();

            Assert.IsTrue(_container.IsRegistered<ILogger>());
        }

        [TestMethod]
        public void Specification_Registration_ContainerIncludesItselfUnderRegistrations()
        {
            Assert.IsNotNull(_container.Registrations.Where(r => r.RegisteredType == typeof(IUnityContainer)).FirstOrDefault());
        }

        [TestMethod]
        public void Specification_Registration_NewRegistrationsShowUpInRegistrationsSequence()
        {
            _container.RegisterType<ILogger, MockLogger>()
                .RegisterType<ILogger, MockLogger>("second");

            var registrations = (from r in _container.Registrations
                                 where r.RegisteredType == typeof(ILogger)
                                 select r).ToList();

            Assert.AreEqual(2, registrations.Count);

            Assert.IsTrue(registrations.Any(r => r.Name == null));
            Assert.IsTrue(registrations.Any(r => r.Name == "second"));
        }

        [TestMethod]
        public void Specification_Registration_TypeMappingShowsUpInRegistrationsCorrectly()
        {
            _container.RegisterType<ILogger, MockLogger>();

            var registration =
                (from r in _container.Registrations where r.RegisteredType == typeof(ILogger) select r).First();
            Assert.AreSame(typeof(MockLogger), registration.MappedToType);
        }

        [TestMethod]
        public void Specification_Registration_NonMappingRegistrationShowsUpInRegistrationsSequence()
        {
            _container.RegisterType<MockLogger>();
            var registration = (from r in _container.Registrations
                                where r.RegisteredType == typeof(MockLogger)
                                select r).First();

            Assert.AreSame(registration.RegisteredType, registration.MappedToType);
            Assert.IsNull(registration.Name);
        }

        [TestMethod]
        public void Specification_Registration_RegistrationOfOpenGenericTypeShowsUpInRegistrationsSequence()
        {
            _container.RegisterType(typeof(IDictionary<,>), typeof(Dictionary<,>), "test");
            var registration = _container.Registrations.First(r => r.RegisteredType == typeof(IDictionary<,>));

            Assert.AreSame(typeof(Dictionary<,>), registration.MappedToType);
            Assert.AreEqual("test", registration.Name);
        }

        [TestMethod]
        public void Specification_Registration_RegistrationsInParentContainerAppearInChild()
        {
            _container.RegisterType<ILogger, MockLogger>();
            var child = _container.CreateChildContainer();

            var registration =
                (from r in child.Registrations where r.RegisteredType == typeof(ILogger) select r).First();

            Assert.AreSame(typeof(MockLogger), registration.MappedToType);
        }

        [TestMethod]
        public void Specification_Registration_RegistrationsInChildContainerDoNotAppearInParent()
        {
            var child = _container.CreateChildContainer()
                .RegisterType<ILogger, MockLogger>("named");

            var childRegistration = child.Registrations.First(r => r.RegisteredType == typeof(ILogger));
            var parentRegistration =
                _container.Registrations.FirstOrDefault(r => r.RegisteredType == typeof(ILogger));

            Assert.IsNull(parentRegistration);
            Assert.IsNotNull(childRegistration);
        }

        [TestMethod]
        public void Specification_Registration_DuplicateRegistrationsInParentAndChildOnlyShowUpOnceInChild()
        {
            _container.RegisterType<IService, Service>("one");

            var child = _container.CreateChildContainer();
            child.RegisterType<IService, OtherService>("one");

            var registrations = from r in child.Registrations
                                where r.RegisteredType == typeof(IService)
                                select r;

            Assert.AreEqual(1, registrations.Count());

            var childRegistration = registrations.First();
            Assert.AreSame(typeof(OtherService), childRegistration.MappedToType);
            Assert.AreEqual("one", childRegistration.Name);
        }
    }
}
