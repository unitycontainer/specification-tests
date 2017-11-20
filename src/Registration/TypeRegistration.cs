// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Specification.TestData;

namespace Unity.Specification.Registration
{
    public abstract partial class SpecificationTests : TestFixtureBase
    {
        private IUnityContainer _container;

        [TestInitialize]
        public void Setup()
        {
            _container = GetContainer();
        }

        [TestMethod]
        public void ContainerListsItselfAsRegistered()
        {
            Assert.IsTrue(_container.IsRegistered(typeof(IUnityContainer)));
        }

        [TestMethod]
        public void ContainerDoesNotListItselfUnderNonDefaultName()
        {
            Assert.IsFalse(_container.IsRegistered(typeof(IUnityContainer), "other"));
        }

        [TestMethod]
        public void ContainerListsItselfAsRegisteredUsingGenericOverload()
        {
            Assert.IsTrue(_container.IsRegistered<IUnityContainer>());
        }

        [TestMethod]
        public void ContainerDoesNotListItselfUnderNonDefaultNameUsingGenericOverload()
        {
            Assert.IsFalse(_container.IsRegistered<IUnityContainer>("other"));
        }

        [TestMethod]
        public void IsRegisteredWorksForRegisteredType()
        {
            _container.RegisterType<ILogger, MockLogger>();

            Assert.IsTrue(_container.IsRegistered<ILogger>());
        }

        [TestMethod]
        public void ContainerIncludesItselfUnderRegistrations()
        {
            Assert.IsNotNull(_container.Registrations.Where(r => r.RegisteredType == typeof(IUnityContainer)).FirstOrDefault());
        }

        [TestMethod]
        public void NewRegistrationsShowUpInRegistrationsSequence()
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
        public void TypeMappingShowsUpInRegistrationsCorrectly()
        {
            _container.RegisterType<ILogger, MockLogger>();

            var registration =
                (from r in _container.Registrations where r.RegisteredType == typeof(ILogger) select r).First();
            Assert.AreSame(typeof(MockLogger), registration.MappedToType);
        }

        [TestMethod]
        public void NonMappingRegistrationShowsUpInRegistrationsSequence()
        {
            _container.RegisterType<MockLogger>();
            var registration = (from r in _container.Registrations
                                where r.RegisteredType == typeof(MockLogger)
                                select r).First();

            Assert.AreSame(registration.RegisteredType, registration.MappedToType);
            Assert.IsNull(registration.Name);
        }

        [TestMethod]
        public void RegistrationOfOpenGenericTypeShowsUpInRegistrationsSequence()
        {
            _container.RegisterType(typeof(IDictionary<,>), typeof(Dictionary<,>), "test");
            var registration = _container.Registrations.First(r => r.RegisteredType == typeof(IDictionary<,>));

            Assert.AreSame(typeof(Dictionary<,>), registration.MappedToType);
            Assert.AreEqual("test", registration.Name);
        }

        [TestMethod]
        public void RegistrationsInParentContainerAppearInChild()
        {
            _container.RegisterType<ILogger, MockLogger>();
            var child = _container.CreateChildContainer();

            var registration =
                (from r in child.Registrations where r.RegisteredType == typeof(ILogger) select r).First();

            Assert.AreSame(typeof(MockLogger), registration.MappedToType);
        }

        [TestMethod]
        public void RegistrationsInChildContainerDoNotAppearInParent()
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
        public void DuplicateRegistrationsInParentAndChildOnlyShowUpOnceInChild()
        {
            _container.RegisterType<IService, Service>("one");

            var child = _container.CreateChildContainer()
                .RegisterType<IService, OtherService>("one");

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
