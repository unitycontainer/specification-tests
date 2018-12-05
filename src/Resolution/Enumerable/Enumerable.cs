using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Specification.TestData;

namespace Unity.Specification.Resolution.Enumerable
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void Enumerable()
        {
            // Act
            var enumerable = Container.Resolve<IEnumerable<IService>>();

            // Verify
            Assert.AreEqual(4, Service.Instances);

            var array = enumerable.ToArray();
            Assert.IsNotNull(array);
            Assert.AreEqual(4, array.Length);
        }

        [TestMethod]
        public void Enumerable_Empty()
        {
            // Act
            var enumerable = Container.Resolve<IEnumerable<IDictionary<IService, Service>>>();

            // Verify
            var array = enumerable.ToArray();
            Assert.IsNotNull(array);
            Assert.AreEqual(0, array.Length);
        }

        [TestMethod]
        public void Enumerable_Lazy()
        {
            // Act
            var enumerable = Container.Resolve<IEnumerable<Lazy<IService>>>();

            // Verify
            var array = enumerable.ToArray();
            Assert.AreEqual(0, Service.Instances);
            Assert.IsNotNull(array);
            Assert.AreEqual(4, array.Length);
            Assert.IsNotNull(array[0].Value);
            Assert.IsNotNull(array[1].Value);
            Assert.IsNotNull(array[2].Value);
            Assert.IsNotNull(array[3].Value);
            Assert.AreEqual(4, Service.Instances);
        }

        [TestMethod]
        public void Enumerable_Func()
        {
            // Act
            var enumerable = Container.Resolve<IEnumerable<Func<IService>>>();

            // Verify
            var array = enumerable.ToArray();
            Assert.IsNotNull(array);
            Assert.AreEqual(4, array.Length);
        }

        [TestMethod]
        public void Enumerable_Lazy_Func()
        {
            // Act
            var enumerable = Container.Resolve<IEnumerable<Lazy<Func<IService>>>>();

            // Verify
            var array = enumerable.ToArray();
            Assert.AreEqual(0, Service.Instances);
            Assert.IsNotNull(array);
            Assert.AreEqual(4, array.Length);
            Assert.IsNotNull(array[0].Value);
            Assert.IsNotNull(array[1].Value);
            Assert.IsNotNull(array[2].Value);
            Assert.IsNotNull(array[3].Value);
            Assert.AreEqual(0, Service.Instances);
            Assert.IsNotNull(array[0].Value());
            Assert.IsNotNull(array[1].Value());
            Assert.IsNotNull(array[2].Value());
            Assert.IsNotNull(array[3].Value());
            Assert.AreEqual(4, Service.Instances);
        }

        [TestMethod]
        public void Enumerable_Func_Lazy()
        {
            // Act
            var enumerable = Container.Resolve<IEnumerable<Func<Lazy<IService>>>>();

            // Verify
            var array = enumerable.ToArray();
            Assert.AreEqual(0, Service.Instances);
            Assert.IsNotNull(array);
            Assert.AreEqual(4, array.Length);
            Assert.IsNotNull(array[0]);
            Assert.IsNotNull(array[1]);
            Assert.IsNotNull(array[2]);
            Assert.IsNotNull(array[3]);
            Assert.AreEqual(0, Service.Instances);
            Assert.IsNotNull(array[0]().Value);
            Assert.IsNotNull(array[1]().Value);
            Assert.IsNotNull(array[2]().Value);
            Assert.IsNotNull(array[3]().Value);
            Assert.AreEqual(4, Service.Instances);
        }

        [TestMethod]
        public void Enumerable_Func_Lazy_Instance()
        {
            // Setup
            Container.RegisterInstance(new Lazy<IService>(() => new Service()));

            // Act
            var enumerable = Container.Resolve<IEnumerable<Func<Lazy<IService>>>>();

            // Verify
            var array = enumerable.ToArray();
            Assert.AreEqual(0, Service.Instances);
            Assert.IsNotNull(array);
            Assert.AreEqual(1, array.Length);
            Assert.IsNotNull(array[0]);
            Assert.AreEqual(0, Service.Instances);
            Assert.IsNotNull(array[0]().Value);
            Assert.AreEqual(1, Service.Instances);
        }

        [TestMethod]
        public void Enumerable_SingleService()
        {
            using (IUnityContainer provider = GetContainer())
            {
                // Arrange
                provider.RegisterType<IService, Service>();

                // Act
                var services = provider.Resolve<IEnumerable<IService>>();
                var service = services.Single();

                // Assert
                Assert.IsNotNull(service);
                Assert.IsInstanceOfType(service, typeof(Service));
            }
        }

        [TestMethod]
        public void Enumerable_MixedServices()
        {
            using (IUnityContainer provider = GetContainer())
            {
                // Arrange
                provider.RegisterType<IService, Service>(typeof(Service).FullName);
                provider.RegisterType<IService, Service>();
                provider.RegisterType<IService, OtherService>(typeof(OtherService).FullName);

                // Act
                var services = provider.Resolve<IEnumerable<IService>>();

                // Assert
                using (var enumerator = services.GetEnumerator())
                {
                    Assert.IsTrue(enumerator.MoveNext());
                    Assert.IsInstanceOfType(enumerator.Current, typeof(Service));
                    Assert.IsTrue(enumerator.MoveNext());
                    Assert.IsInstanceOfType(enumerator.Current, typeof(Service));
                    Assert.IsTrue(enumerator.MoveNext());
                    Assert.IsInstanceOfType(enumerator.Current, typeof(OtherService));
                }
            }
        }

        [TestMethod]
        public void Enumerable_MultipleServices()
        {
            using (IUnityContainer provider = GetContainer())
            {
                // Arrange
                provider.RegisterType<IService, Service>(typeof(Service).FullName);
                provider.RegisterType<IService, OtherService>(typeof(OtherService).FullName);

                // Act
                var services = provider.Resolve<IEnumerable<IService>>();

                // Assert
                using (var enumerator = services.GetEnumerator())
                {
                    Assert.IsTrue(enumerator.MoveNext());
                    Assert.IsInstanceOfType(enumerator.Current, typeof(Service));
                    Assert.IsTrue(enumerator.MoveNext());
                    Assert.IsInstanceOfType(enumerator.Current, typeof(OtherService));
                }
            }
        }

        [TestMethod]
        public void Enumerable_RegistrationOrderIsPreserved()
        {
            using (IUnityContainer provider = GetContainer())
            {
                // Arrange
                provider.RegisterType<IService, OtherService>(typeof(OtherService).FullName);
                provider.RegisterType<IService, Service>(typeof(Service).FullName);

                // Act
                var services = provider.Resolve<IEnumerable<IService>>();

                // Assert
                using (var enumerator = services.GetEnumerator())
                {
                    Assert.IsTrue(enumerator.MoveNext());
                    Assert.IsInstanceOfType(enumerator.Current, typeof(OtherService));
                    Assert.IsTrue(enumerator.MoveNext());
                    Assert.IsInstanceOfType(enumerator.Current, typeof(Service));
                }
            }
        }

        [TestMethod]
        public void Enumerable_NonexistentService()
        {
            using (IUnityContainer provider = GetContainer())
            {
                var services = provider.Resolve<IEnumerable<IService>>();

                // Assert
                Assert.AreEqual(0, services.Count());
            }
        }

        [TestMethod]
        public void Enumerable_ResolvesMixedOpenClosedGenericsAsEnumerable()
        {
            using (IUnityContainer provider = GetContainer())
            {
                // Arrange
                var instance = new Foo<IService>(new OtherService());

                provider.RegisterType<IService>();
                provider.RegisterType<IService, Service>();
                provider.RegisterType(typeof(IFoo<IService>), typeof(Foo<IService>), "Instance", new ContainerControlledLifetimeManager());
                provider.RegisterType(typeof(IFoo<>), typeof(Foo<>), "fa", new ContainerControlledLifetimeManager());
                provider.RegisterInstance<IFoo<IService>>(instance);

                // Act
                var enumerable = provider.Resolve<IEnumerable<IFoo<IService>>>().ToArray();

                // Assert
                Assert.AreEqual(3, enumerable.Length);
                Assert.IsNotNull(enumerable[0]);
                Assert.IsNotNull(enumerable[1]);
                Assert.IsNotNull(enumerable[2]);

            }
        }

        [TestMethod]
        public void Enumerable_ResolvesDifferentInstances()
        {
            using (IUnityContainer provider = GetContainer())
            {
                // Arrange
                provider.RegisterType<IService, Service>("1", new ContainerControlledLifetimeManager());
                provider.RegisterType<IService, Service>("2", new ContainerControlledLifetimeManager());
                provider.RegisterType<IService, Service>("3", new ContainerControlledLifetimeManager());

                using (var scope = provider.CreateChildContainer())
                {
                    var enumerable = scope.Resolve<IEnumerable<IService>>().ToArray();
                    var service = scope.Resolve<IService>("3");

                    // Assert
                    Assert.AreEqual(3, enumerable.Length);
                    Assert.IsNotNull(enumerable[0]);
                    Assert.IsNotNull(enumerable[1]);
                    Assert.IsNotNull(enumerable[2]);

                    Assert.AreNotSame(enumerable[0], enumerable[1]);
                    Assert.AreNotSame(enumerable[1], enumerable[2]);
                    Assert.AreSame(service, enumerable[2]);
                }
            }
        }

        [TestMethod]
        public void Enumerable_ResolvesDifferentInstancesForOpenGenerics()
        {
            using (IUnityContainer provider = GetContainer())
            {
                // Arrange
                provider.RegisterType<IService, Service>();
                provider.RegisterType(typeof(IFoo<>), typeof(Foo<>), "1", new ContainerControlledLifetimeManager());
                provider.RegisterType(typeof(IFoo<>), typeof(Foo<>), "2", new ContainerControlledLifetimeManager());
                provider.RegisterType(typeof(IFoo<>), typeof(Foo<>), "3", new ContainerControlledLifetimeManager());

                using (var scope = provider.CreateChildContainer())
                {
                    var enumerable = scope.Resolve<IEnumerable<IFoo<IService>>>().ToArray();
                    var service = scope.Resolve<IFoo<IService>>("3");

                    // Assert
                    Assert.AreEqual(3, enumerable.Length);
                    Assert.IsNotNull(enumerable[0]);
                    Assert.IsNotNull(enumerable[1]);
                    Assert.IsNotNull(enumerable[2]);

                    Assert.AreNotSame(enumerable[0], enumerable[1]);
                    Assert.AreNotSame(enumerable[1], enumerable[2]);
                    Assert.AreSame(service, enumerable[2]);
                }
            }
        }

    }
}
