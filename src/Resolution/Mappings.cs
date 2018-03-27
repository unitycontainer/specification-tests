using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Lifetime;
using Unity.Specification.TestData;

namespace Unity.Specification.Resolution
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void Specification_Resolution_Enumerable_LastReplacesPrevious()
        {
            using (IUnityContainer provider = GetContainer())
            {
                // Arrange
                provider.RegisterType<IService, Service>();
                provider.RegisterType<IService, OtherService>();

                // Act
                var service = provider.Resolve<IService>();

                // Assert
                Assert.IsNotNull(service);
                Assert.IsInstanceOfType(service, typeof(OtherService));
            }
        }

        [TestMethod]
        public void Specification_Resolution_Enumerable_ScopedServiceCanBeResolved()
        {
            using (IUnityContainer provider = GetContainer())
            {
                // Arrange
                provider.RegisterType<IService, Service>(new HierarchicalLifetimeManager());

                // Act
                using (var scope = provider.CreateChildContainer())
                {
                    var providerScopedService = provider.Resolve<IService>();
                    var scopedService1 = scope.Resolve<IService>();
                    var scopedService2 = scope.Resolve<IService>();

                    // Assert
                    Assert.AreNotSame(providerScopedService, scopedService1);
                    Assert.AreSame(scopedService1, scopedService2);
                }
            }
        }

        [TestMethod]
        public void Specification_Resolution_Enumerable_NestedScopedServiceCanBeResolved()
        {
            using (IUnityContainer provider = GetContainer())
            {
                // Arrange
                provider.RegisterType<IService, Service>(new HierarchicalLifetimeManager());

                // Act
                using (var outerScope = provider.CreateChildContainer())
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
        }

        [TestMethod]
        public void Specification_Resolution_Enumerable_SingletonServicesComeFromRootProvider()
        {
            using (IUnityContainer provider = GetContainer())
            {
                // Arrange
                provider.RegisterType<IService, Service>(new ContainerControlledLifetimeManager());

                Service disposableService1;
                Service disposableService2;

                // Act and Assert
                using (var scope = provider.CreateChildContainer())
                {
                    var service = scope.Resolve<IService>();
                    disposableService1 = (Service)service;
                    Assert.IsFalse(disposableService1.Disposed);
                }

                Assert.IsFalse(disposableService1.Disposed);

                using (var scope = provider.CreateChildContainer())
                {
                    var service = scope.Resolve<IService>();
                    disposableService2 = (Service)service;
                    Assert.IsFalse(disposableService2.Disposed);
                }

                Assert.IsFalse(disposableService2.Disposed);
                Assert.AreSame(disposableService1, disposableService2);
            }
        }

        [TestMethod]
        public void Specification_Resolution_Enumerable_NestedScopedServiceCanBeResolvedWithNoFallbackProvider()
        {
            using (IUnityContainer provider = GetContainer())
            {
                // Arrange
                provider.RegisterType<IService, Service>(new HierarchicalLifetimeManager());
                // Act
                using (var outerScope = provider.CreateChildContainer())
                using (var innerScope = outerScope.CreateChildContainer())
                {
                    var outerScopedService = outerScope.Resolve<IService>();
                    var innerScopedService = innerScope.Resolve<IService>();

                    // Assert
                    Assert.AreNotSame(outerScopedService, innerScopedService);
                }
            }
        }

        [TestMethod]
        public void Specification_Resolution_Enumerable_OpenGenericServicesCanBeResolved()
        {
            using (IUnityContainer provider = GetContainer())
            {
                // Arrange
                provider.RegisterType<IService, Service>(new ContainerControlledLifetimeManager());
                provider.RegisterType(typeof(IFoo<>), typeof(Foo<>));

                // Act
                var genericService = provider.Resolve<IFoo<IService>>();
                var singletonService = provider.Resolve<IService>();

                // Assert
                Assert.AreSame(singletonService, genericService.Value);
            }
        }

        [TestMethod]
        public void Specification_Resolution_Enumerable_ClosedServicesPreferredOverOpenGenericServices()
        {
            using (IUnityContainer provider = GetContainer())
            {
                // Arrange
                provider.RegisterType<IService, Service>();
                provider.RegisterType(typeof(IFoo<>), typeof(Foo<>));
                provider.RegisterType(typeof(IFoo<IService>), typeof(Foo<IService>));

                // Act
                var service = provider.Resolve<IFoo<IService>>();

                // Assert
                Assert.IsInstanceOfType(service.Value, typeof(Service));
            }
        }



        [TestMethod]
        public void Enumerable_ServicesRegisteredWithImplementationTypeCanBeResolved()
        {
            using (IUnityContainer provider = GetContainer())
            {
                // Arrange
                provider.RegisterType<IService, Service>();

                // Act
                var service = provider.Resolve<IService>();

                // Assert
                Assert.IsNotNull(service);
                Assert.IsInstanceOfType(service, typeof(Service));
            }
        }

        [TestMethod]
        public void Enumerable_ServicesRegisteredWithImplementationType_ReturnDifferentInstancesPerResolution_ForTransientServices()
        {
            using (IUnityContainer provider = GetContainer())
            {
                // Arrange
                provider.RegisterType<IService, Service>();

                // Act
                var service1 = provider.Resolve<IService>();
                var service2 = provider.Resolve<IService>();

                // Assert
                Assert.IsInstanceOfType(service1, typeof(Service));
                Assert.IsInstanceOfType(service1, typeof(Service));

                Assert.AreNotSame(service1, service2);
            }

        }

        [TestMethod]
        public void Enumerable_ServicesRegisteredWithImplementationType_ReturnSameInstancesPerResolution_ForSingletons()
        {
            using (IUnityContainer provider = GetContainer())
            {
                // Arrange
                provider.RegisterType<IService, Service>(new ContainerControlledLifetimeManager());

                // Act
                var service1 = provider.Resolve<IService>();
                var service2 = provider.Resolve<IService>();

                // Assert
                Assert.IsInstanceOfType(service1, typeof(Service));
                Assert.IsInstanceOfType(service1, typeof(Service));

                Assert.AreSame(service1, service2);
            }
        }

        [TestMethod]
        public void Enumerable_ServiceInstanceCanBeResolved()
        {
            using (IUnityContainer provider = GetContainer())
            {
                // Arrange
                var instance = new Service();
                provider.RegisterInstance<IService>(instance);

                // Act
                var service = provider.Resolve<IService>();

                // Assert
                Assert.AreSame(instance, service);
            }
        }

        [TestMethod]
        public void Enumerable_TransientServiceCanBeResolvedFromProvider()
        {
            using (IUnityContainer provider = GetContainer())
            {
                // Arrange
                provider.RegisterType<IService, Service>();

                // Act
                var service1 = provider.Resolve<IService>();
                var service2 = provider.Resolve<IService>();

                // Assert
                Assert.IsNotNull(service1);
                Assert.AreNotSame(service1, service2);
            }
        }

        [TestMethod]
        public void Enumerable_TransientServiceCanBeResolvedFromScope()
        {
            using (IUnityContainer provider = GetContainer())
            {
                // Arrange
                provider.RegisterType<IService, Service>();

                // Act
                var service1 = provider.Resolve<IService>();
                using (var scope = provider.CreateChildContainer())
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
}
