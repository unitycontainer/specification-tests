using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Unity.Specification.Resolution.Array
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void ArrayOfRegistered()
        {
            // Arrange
            Container.RegisterType<IService, Service>("1");
            Container.RegisterType<IService, Service>("2");
            Container.RegisterType<IService, OtherService>("3");
            Container.RegisterType<IService, Service>();
            Service.Instances = 0;

            // Act
            var array = Container.Resolve<IService[]>();

            // Verify
            Assert.IsNotNull(array);
            Assert.AreEqual(2, Service.Instances);
            Assert.AreEqual(3, array.Length);
        }

        [TestMethod]
        public void ArrayOfRegisteredPoco()
        {
            // Arrange
            Container.RegisterType<Service>("1");

            // Act
            var array = Container.Resolve<Service[]>();

            // Verify
            Assert.IsNotNull(array);
            Assert.AreEqual(1, array.Length);
        }

        [TestMethod]
        public void UnregisteredPoco()
        {
            // Act
            var array = Container.Resolve<Service[]>();

            // Verify
            Assert.IsNotNull(array);
            Assert.AreEqual(0, array.Length);
        }

        [TestMethod]
        public void Lazy()
        {
            // Arrange
            Container.RegisterType<IService, Service>("1");
            Container.RegisterType<IService, Service>("2");
            Container.RegisterType<IService, OtherService>("3");
            Container.RegisterType<IService, Service>();
            Service.Instances = 0;

            // Act
            var array = Container.Resolve<Lazy<IService>[]>();

            // Verify
            Assert.AreEqual(0, Service.Instances);
            Assert.IsNotNull(array);
            Assert.AreEqual(3, array.Length);
            Assert.IsNotNull(array[0].Value);
            Assert.IsNotNull(array[1].Value);
            Assert.IsNotNull(array[2].Value);
            Assert.AreEqual(2, Service.Instances);
        }

        [TestMethod]
        public void Func()
        {
            // Arrange
            Container.RegisterType<IService, Service>("1");
            Container.RegisterType<IService, Service>("2");
            Container.RegisterType<IService, OtherService>("3");
            Container.RegisterType<IService, Service>();

            // Act
            var array = Container.Resolve<Func<IService>[]>();

            // Verify
            Assert.IsNotNull(array);
            Assert.AreEqual(3, array.Length);
        }

        [TestMethod]
        public void FuncNamed()
        {
            // Arrange
            Container.RegisterType(typeof(Func<>), "0");
            Container.RegisterType(typeof(Func<>), "1");
            Container.RegisterType(typeof(Func<>), "2");

            // Act
            var array = Container.Resolve<Func<IService>[]>();

            // Verify
            Assert.IsNotNull(array);
            Assert.IsInstanceOfType(array, typeof(Func<IService>[]));
            Assert.AreEqual(3, array.Length);
        }

        [TestMethod]
        public void LazyFunc()
        {
            // Arrange
            Container.RegisterType(typeof(IList<>), typeof(List<>), Invoke.Constructor());
            Container.RegisterType(typeof(IFoo<>), typeof(Foo<>));
            Container.RegisterType<IService, Service>("1");
            Container.RegisterType<IService, Service>("2");
            Container.RegisterType<IService, Service>("3");
            Container.RegisterType<IService, Service>();
            Service.Instances = 0;

            // Act
            var array = Container.Resolve<Lazy<Func<IService>>[]>();

            // Verify
            Assert.AreEqual(0, Service.Instances);
            Assert.IsNotNull(array);
            Assert.AreEqual(3, array.Length);
            Assert.IsNotNull(array[0].Value);
            Assert.IsNotNull(array[1].Value);
            Assert.IsNotNull(array[2].Value);
            Assert.AreEqual(0, Service.Instances);
            Assert.IsNotNull(array[0].Value());
            Assert.IsNotNull(array[1].Value());
            Assert.IsNotNull(array[2].Value());
            Assert.AreEqual(3, Service.Instances);
        }

        [TestMethod]
        public void FuncLazy()
        {
            // Arrange
            Container.RegisterType<IService, Service>("1");
            Container.RegisterType<IService, Service>("2");
            Container.RegisterType<IService, OtherService>("3");
            Container.RegisterType<IService, Service>();
            Service.Instances = 0;

            // Act
            var array = Container.Resolve<Func<Lazy<IService>>[]>();

            // Verify
            Assert.AreEqual(0, Service.Instances);
            Assert.IsNotNull(array);
            Assert.AreEqual(3, array.Length);
            Assert.IsNotNull(array[0]);
            Assert.IsNotNull(array[1]);
            Assert.IsNotNull(array[2]);
            Assert.AreEqual(0, Service.Instances);
            Assert.IsNotNull(array[0]().Value);
            Assert.IsNotNull(array[1]().Value);
            Assert.IsNotNull(array[2]().Value);
            Assert.AreEqual(2, Service.Instances);
        }

        [TestMethod]
        public void FuncLazyInstance()
        {
            // Arrange
            Container.RegisterInstance(null, "Instance", new Lazy<IService>(() => new Service()));
            Service.Instances = 0;

            // Act
            var array = Container.Resolve<Func<Lazy<IService>>[]>();

            // Verify
            Assert.AreEqual(0, Service.Instances);
            Assert.IsNotNull(array);
            Assert.AreEqual(1, array.Length);
            Assert.IsNotNull(array[0]);
            Assert.AreEqual(0, Service.Instances);
            Assert.IsNotNull(array[0]().Value);
            Assert.AreEqual(1, Service.Instances);
        }

        [TestMethod]
        public void ResolvesMixedOpenClosedFuncGenericsAsArray()
        {
            // Arrange
            var instance = new Foo<IService>(new OtherService());
            Container.RegisterType<IService, Service>();

            Container.RegisterType(typeof(IFoo<>), typeof(Foo<>), "open");
            Container.RegisterType<IFoo<IService>, Foo<IService>>("closed");
            Container.RegisterInstance<IFoo<IService>>("Instance", instance);

            // Act
            var enumerable = Container.Resolve<Func<IFoo<IService>>[]>();

            // Assert
            Assert.AreEqual(3, enumerable.Length);
            Assert.IsNotNull(enumerable[0]);
            Assert.IsNotNull(enumerable[1]);
            Assert.IsNotNull(enumerable[2]);
        }

        [TestMethod]
        public void ClosedTrumpsOpenGeneric()
        {
            // Arrange
            var instance = new Foo<IService>(new OtherService());

            Container.RegisterInstance<IFoo<IService>>(Name, instance)
                     .RegisterType(typeof(IFoo<>), typeof(Foo<>), Name)
                     .RegisterType<IFoo<IService>, Foo<IService>>("closed")
                     .RegisterType<IService, Service>();

            // Act
            var enumerable = Container.Resolve<IFoo<IService>[]>();

            // Assert
            Assert.AreEqual(2, enumerable.Length);
            Assert.IsNotNull(enumerable[0]);
            Assert.IsNotNull(enumerable[1]);
        }
    }
}
