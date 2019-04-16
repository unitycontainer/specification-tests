using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Specification.TestData;

namespace Unity.Specification.Resolution.Array
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void Empty()
        {
            var results = new List<object>(UnityContainerExtensions.Resolve<object[]>(Container));

            Assert.IsNotNull(results);
            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void Registered()
        {
            // Act
            var array = UnityContainerExtensions.Resolve<IService[]>(Container);

            // Verify
            Assert.IsNotNull(array);
            Assert.AreEqual(3, Service.Instances);
            Assert.AreEqual(3, array.Length);
        }

        [TestMethod]
        public void SimplePoco()
        {
            // Act
            UnityContainerExtensions.RegisterType<SimpleClass[]>(Container, "Array");

            // Verify
            Assert.IsNotNull(UnityContainerExtensions.Resolve<SimpleClass[]>(Container, "Array"));
        }

        [TestMethod]
        public void Lazy()
        {
            // Act
            var array = UnityContainerExtensions.Resolve<Lazy<IService>[]>(Container);

            // Verify
            Assert.AreEqual(0, Service.Instances);
            Assert.IsNotNull(array);
            Assert.AreEqual(3, array.Length);
            Assert.IsNotNull(array[0].Value);
            Assert.IsNotNull(array[1].Value);
            Assert.IsNotNull(array[2].Value);
            Assert.AreEqual(3, Service.Instances);
        }

        [TestMethod]
        public void Func()
        {
            Container.RegisterType(typeof(Func<>), "0");
            Container.RegisterType(typeof(Func<>), "1");
            Container.RegisterType(typeof(Func<>), "2");

            // Act
            var array = UnityContainerExtensions.Resolve<Func<IService>[]>(Container);

            // Verify
            Assert.IsNotNull(array);
            Assert.AreEqual(3, array.Length);
        }

        [TestMethod]
        public void LazyFunc()
        {
            // Act
            var array = UnityContainerExtensions.Resolve<Lazy<Func<IService>>[]>(Container);

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
            Container.RegisterType(typeof(Func<>), "0");
            Container.RegisterType(typeof(Func<>), "1");
            Container.RegisterType(typeof(Func<>), "2");

            // Act
            var array = UnityContainerExtensions.Resolve<Func<Lazy<IService>>[]>(Container);

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
            Assert.AreEqual(3, Service.Instances);
        }


        [TestMethod]
        public void FuncLazyInstance()
        {
            // Setup
            UnityContainerExtensions.RegisterInstance(Container, null, "Instance", new Lazy<IService>(() => new Service()));

            // Act
            var array = UnityContainerExtensions.Resolve<Func<Lazy<IService>>[]>(Container);

            // Verify
            Assert.AreEqual(0, Service.Instances);
            Assert.IsNotNull(array);
            Assert.AreEqual(1, array.Length);
            Assert.IsNotNull(array[0]);
            Assert.AreEqual(0, Service.Instances);
            Assert.IsNotNull(array[0]().Value);
            Assert.AreEqual(1, Service.Instances);
        }
    }
}
