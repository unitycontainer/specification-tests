using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Exceptions;
using Unity.Lifetime;
using Unity.Specification.TestData;

namespace Unity.Specification.Registration
{
    public abstract partial class SpecificationTests : TestFixtureBase
    {
        [TestMethod]
        public void RegisterInstance_IUC_SimpleObject()
        {
            var instance = Guid.NewGuid().ToString();

            _container.RegisterInstance(null, null, instance, null);
            Assert.AreEqual(_container.Resolve<string>(), instance);
        }

        [TestMethod]
        public void RegisterInstance_IUC_NamedObject()
        {
            var instance = Guid.NewGuid().ToString();

            _container.RegisterInstance(null, instance, instance, null);

            Assert.AreEqual(_container.Resolve<string>(instance), instance);
            Assert.ThrowsException<ResolutionFailedException>(() => _container.Resolve<string>());
        }

        [TestMethod]
        public void RegisterInstance_IUC_InterfacedObject()
        {
            var instance = new Service();

            _container.RegisterInstance(typeof(IService), null, instance, null);

            Assert.AreEqual(_container.Resolve<IService>(), instance);
            Assert.AreNotEqual(_container.Resolve<Service>(), instance);
        }

        [TestMethod]
        public void RegisterInstance_IUC_NamedInterfacedObject()
        {
            var instance = new Service();
            var name = Guid.NewGuid().ToString();

            _container.RegisterInstance(typeof(IService), name, instance, null);

            Assert.AreEqual(_container.Resolve<IService>(name), instance);
            Assert.AreNotEqual(_container.Resolve<Service>(), instance);
            Assert.ThrowsException<ResolutionFailedException>(() => _container.Resolve<IService>());
        }

        [TestMethod]
        public void RegisterInstance_SimpleObject()
        {
            var instance = Guid.NewGuid().ToString();

            _container.RegisterInstance(instance);
            
            Assert.AreEqual(_container.Resolve<string>(), instance);
        }

        [TestMethod]
        public void RegisterInstance_NamedObject()
        {
            var instance = Guid.NewGuid().ToString();

            _container.RegisterInstance(instance, instance);

            Assert.AreEqual(_container.Resolve<string>(instance), instance);
            Assert.ThrowsException<ResolutionFailedException>(() => _container.Resolve<string>());
        }

        [TestMethod]
        public void RegisterInstance_InterfacedObject()
        {
            var instance = new Service();

            _container.RegisterInstance<IService>(instance);

            Assert.AreEqual(_container.Resolve<IService>(), instance);
            Assert.AreNotEqual(_container.Resolve<Service>(), instance);
        }

        [TestMethod]
        public void RegisterInstance_NamedInterfacedObject()
        {
            var instance = new Service();
            var name = Guid.NewGuid().ToString();

            _container.RegisterInstance<IService>(name, instance);

            Assert.AreEqual(_container.Resolve<IService>(name), instance);
            Assert.AreNotEqual(_container.Resolve<Service>(), instance);
            Assert.ThrowsException<ResolutionFailedException>(() => _container.Resolve<IService>());
        }

        [TestMethod]
        public void RegisterInstance_ExternallyControlledLifetimeManager()
        {
            var instance = Guid.NewGuid().ToString();

            _container.RegisterInstance(null, null, instance, new ExternallyControlledLifetimeManager());
            Assert.AreEqual(_container.Resolve<string>(), instance);
        }


        [TestMethod]
        public void RegisterInstance_ChainRegistrations()
        {
            var instance = new Service();

            _container.RegisterInstance(instance);
            _container.RegisterType<IService, Service>();

            Assert.AreEqual(_container.Resolve<IService>(), instance);
        }

        [TestMethod]
        public void RegisterInstance_RegisterWithParentAndChild()
        {
            //create unity container
            var parent = GetContainer();
            parent.RegisterInstance(null, null, Guid.NewGuid().ToString(), new ContainerControlledLifetimeManager());

            var child = parent.CreateChildContainer();
            child.RegisterInstance(null, null, Guid.NewGuid().ToString(), new ContainerControlledLifetimeManager());

            Assert.AreSame(parent.Resolve<string>(), parent.Resolve<string>());
            Assert.AreSame(child.Resolve<string>(), child.Resolve<string>());
            Assert.AreNotSame(parent.Resolve<string>(), child.Resolve<string>());
        }


        [TestMethod]
        public void RegisterInstance_HierarchicalLifetimeManager()
        {
//            Assert.ThrowsException<Exception>(() => GetContainer().RegisterInstance(null, null, Guid.NewGuid().ToString(), new HierarchicalLifetimeManager()));
        }
    }
}
