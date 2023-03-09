using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Lifetime;

namespace Unity.Specification.Resolution.Mapping
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void PocoTypeRegistered()
        {
            // Arrange
            Container.RegisterType(typeof(Foo));

            // Act
            var service = Container.Resolve<Foo>();

            // Assert
            Assert.IsNotNull(service);
            Assert.IsInstanceOfType(service, typeof(Foo));
        }

        [TestMethod]
        public void PocoTypeUnregistered()
        {
            // Act
            var service = Container.Resolve<Foo>();

            // Assert
            Assert.IsNotNull(service);
            Assert.IsInstanceOfType(service, typeof(Foo));
        }

        [TestMethod]
        public void ServiceItself()
        {
            // Arrange
            var instance = new Foo();
            var factory = new Foo();

            Container.RegisterType<Foo>();
            Container.RegisterInstance(Name, instance, InstanceLifetime.Singleton);
            Container.RegisterFactory<Foo>(Legacy, (c, t, n) => factory);

            // Act
            var service1 = Container.Resolve<Foo>();
            var service2 = Container.Resolve<Foo>(Name);
            var service3 = Container.Resolve<Foo>(Legacy);


            // Assert
            Assert.IsNotNull(service1);
            Assert.IsNotNull(service2);
            Assert.IsNotNull(service3);

            Assert.IsInstanceOfType(service1, typeof(Foo));
            Assert.IsInstanceOfType(service2, typeof(Foo));
            Assert.IsInstanceOfType(service3, typeof(Foo));

            Assert.AreSame(instance, service2);
            Assert.AreSame(factory, service3);
        }

        [TestMethod]
        public void ServiceToImplementation()
        {
            // Arrange
            Container.RegisterType(typeof(IFoo), typeof(Foo));

            // Act
            var service = Container.Resolve<IFoo>();

            // Assert
            Assert.IsNotNull(service);
            Assert.IsInstanceOfType(service, typeof(Foo));
        }

        [TestMethod]
        public void MappingToInstance()
        {
            // Arrange
            var service = new Foo();
            Container.RegisterInstance(service);
            Container.RegisterType(typeof(IFoo), typeof(Foo));

            // Act
            var service1 = Container.Resolve<IFoo>();

            // Assert
            Assert.IsNotNull(service1);
            Assert.AreSame(service, service1);
        }

        [TestMethod]
        public void MappingToInstanceInChild()
        {
            // Arrange
            var service = new Foo();
            Container.RegisterInstance(service);
            Container.RegisterType(typeof(IFoo), typeof(Foo));

            // Act
            var service1 = Container.CreateChildContainer()
                                    .Resolve<IFoo>();
            // Assert
            Assert.IsNotNull(service1);
            Assert.AreSame(service, service1);
        }

        [TestMethod]
        public void MappingOverriddenToInstance()
        {
            // Arrange
            var service = new Foo();
            Container.RegisterInstance(service);
            Container.RegisterType(typeof(IFoo), typeof(Foo));

            // Act / Validate
            var service1 = Container.Resolve<IFoo>();
            Assert.IsNotNull(service1);
            Assert.AreSame(service, service1);

            // Act / Validate
            Container.RegisterInstance(new Foo());
            var service2 = Container.Resolve<IFoo>();
            Assert.IsNotNull(service2);
            Assert.AreNotSame(service1, service2);
        }

        [TestMethod]
        public void Mapping()
        {
            // Arrange
            Container.RegisterType(typeof(Foo), new ContainerControlledLifetimeManager());
            Container.RegisterType(typeof(IFoo1), typeof(Foo));
            Container.RegisterType(typeof(IFoo2), typeof(Foo));

            // Act
            var service1 = Container.Resolve<IFoo1>();
            var service2 = Container.Resolve<IFoo2>();

            // Assert
            Assert.IsNotNull(service1);
            Assert.IsNotNull(service2);

            Assert.AreSame<object>(service1, service2);
        }

        [TestMethod]
        public void NamedMappedInterfaceInstanceRegistrationCanBeResolved()
        {
            Container.RegisterInstance<IFoo1>("ATest", new Foo());
            var iTest = (IFoo1)Container.Resolve(typeof(IFoo1), "ATest");

            Assert.IsNotNull(iTest);
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void ThrowsExceptionOnNagative()
        {
            Container.RegisterType<IFoo1, IFoo1>();
            Container.Resolve<IFoo1>("ATest");
        }

        [TestMethod]
        public void LastReplacesPrevious()
        {
            // Arrange
            Container.RegisterType<IService, Service>();
            Container.RegisterType<IService, OtherService>();

            // Act
            var service = Container.Resolve<IService>();

            // Assert
            Assert.IsNotNull(service);
            Assert.IsInstanceOfType(service, typeof(OtherService));
        }

        [TestMethod]
        public void ScopedServiceCanBeResolved()
        {
            // Arrange
            Container.RegisterType<IService, Service>(new HierarchicalLifetimeManager());

            // Act
            using (var scope = Container.CreateChildContainer())
            {
                var ContainerScopedService = Container.Resolve<IService>();
                var scopedService1 = scope.Resolve<IService>();
                var scopedService2 = scope.Resolve<IService>();

                // Assert
                Assert.AreNotSame(ContainerScopedService, scopedService1);
                Assert.AreSame(scopedService1, scopedService2);
            }
        }

        [TestMethod]
        public void NestedScopedServiceCanBeResolved()
        {
            // Arrange
            Container.RegisterType<IService, Service>(new HierarchicalLifetimeManager());

            // Act
            using (var outerScope = Container.CreateChildContainer())
            using (var innerScope = outerScope.CreateChildContainer())
            {
                var outerScopedService = outerScope.Resolve<IService>();
                var innerScopedService = innerScope.Resolve<IService>();

                // Assert
                Assert.IsNotNull(outerScopedService);
                Assert.IsNotNull(innerScopedService);
                Assert.AreNotSame(outerScopedService, innerScopedService);
            }
        }

        [TestMethod]
        public void SingletonServicesComeFromRootContainer()
        {
            // Arrange
            Container.RegisterType<IService, Service>(new ContainerControlledLifetimeManager());

            Service disposableService1;
            Service disposableService2;

            // Act and Assert
            using (var scope = Container.CreateChildContainer())
            {
                var service = scope.Resolve<IService>();
                disposableService1 = (Service)service;
                Assert.IsFalse(disposableService1.Disposed);
            }

            Assert.IsFalse(disposableService1.Disposed);

            using (var scope = Container.CreateChildContainer())
            {
                var service = scope.Resolve<IService>();
                disposableService2 = (Service)service;
                Assert.IsFalse(disposableService2.Disposed);
            }

            Assert.IsFalse(disposableService2.Disposed);
            Assert.AreSame(disposableService1, disposableService2);
        }

        [TestMethod]
        public void NestedScopedServiceCanBeResolvedWithNoFallbackContainer()
        {
            // Arrange
            Container.RegisterType<IService, Service>(new HierarchicalLifetimeManager());
            // Act
            using (var outerScope = Container.CreateChildContainer())
            using (var innerScope = outerScope.CreateChildContainer())
            {
                var outerScopedService = outerScope.Resolve<IService>();
                var innerScopedService = innerScope.Resolve<IService>();

                // Assert
                Assert.AreNotSame(outerScopedService, innerScopedService);
            }
        }



        [TestMethod]
        public void ServicesRegisteredWithImplementationTypeCanBeResolved()
        {
            // Arrange
            Container.RegisterType<IService, Service>();

            // Act
            var service = Container.Resolve<IService>();

            // Assert
            Assert.IsNotNull(service);
            Assert.IsInstanceOfType(service, typeof(Service));
        }

        [TestMethod]
        public void ServicesRegisteredWithImplementationType_ReturnDifferentInstancesPerResolution_ForTransientServices()
        {
            // Arrange
            Container.RegisterType<IService, Service>();

            // Act
            var service1 = Container.Resolve<IService>();
            var service2 = Container.Resolve<IService>();

            // Assert
            Assert.IsInstanceOfType(service1, typeof(Service));
            Assert.IsInstanceOfType(service1, typeof(Service));

            Assert.AreNotSame(service1, service2);
        }

        [TestMethod]
        public void ServicesRegisteredWithImplementationType_ReturnSameInstancesPerResolution_ForSingletons()
        {
            // Arrange
            Container.RegisterType<IService, Service>(new ContainerControlledLifetimeManager());

            // Act
            var service1 = Container.Resolve<IService>();
            var service2 = Container.Resolve<IService>();

            // Assert
            Assert.IsInstanceOfType(service1, typeof(Service));
            Assert.IsInstanceOfType(service1, typeof(Service));

            Assert.AreSame(service1, service2);
        }

        [TestMethod]
        public void ServiceInstanceCanBeResolved()
        {
            // Arrange
            var instance = new Service();
            Container.RegisterInstance<IService>(instance);

            // Act
            var service = Container.Resolve<IService>();

            // Assert
            Assert.AreSame(instance, service);
        }

        [TestMethod]
        public void TransientServiceCanBeResolvedFromContainer()
        {
            // Arrange
            Container.RegisterType<IService, Service>();

            // Act
            var service1 = Container.Resolve<IService>();
            var service2 = Container.Resolve<IService>();

            // Assert
            Assert.IsNotNull(service1);
            Assert.AreNotSame(service1, service2);
        }

        [TestMethod]
        public void TransientServiceCanBeResolvedFromScope()
        {
            // Arrange
            Container.RegisterType<IService, Service>();

            // Act
            var service1 = Container.Resolve<IService>();
            using (var scope = Container.CreateChildContainer())
            {
                var scopedService1 = scope.Resolve<IService>();
                var scopedService2 = scope.Resolve<IService>();

                // Assert
                Assert.AreNotSame(service1, scopedService1);
                Assert.AreNotSame(service1, scopedService2);
                Assert.AreNotSame(scopedService1, scopedService2);
            }
        }

    }
}
