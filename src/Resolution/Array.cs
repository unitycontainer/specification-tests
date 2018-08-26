using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Specification.TestData;

namespace Unity.Specification.Resolution
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void Array()
        {
            // Act
            var array = Container.Resolve<IService[]>();

            // Verify
            Assert.AreEqual(3, Service.Instances);
            Assert.IsNotNull(array);
            Assert.AreEqual(3, array.Length);
        }


        [TestMethod]
        public void Array_OfSimpleClass()
        {
            // Act
            Container.RegisterType<SimpleClass[]>("Array");

            // Verify
            Assert.IsNotNull(Container.Resolve<SimpleClass[]>("Array"));
        }

        [TestMethod]
        public void Array_Lazy()
        {
            // Act
            var array = Container.Resolve<Lazy<IService>[]>();

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
        public void Array_Func()
        {
            // Act
            var array = Container.Resolve<Func<IService>[]>();

            // Verify
            Assert.IsNotNull(array);
            Assert.AreEqual(3, array.Length);
        }

        [TestMethod]
        public void Array_LazyFunc()
        {
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
        public void Array_FuncLazy()
        {
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
            Assert.AreEqual(3, Service.Instances);
        }


        [TestMethod]
        public void Array_FuncLazyInstance()
        {
            // Setup
            Container.RegisterInstance(null, "Instance", new Lazy<IService>(() => new Service()));

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
        public void Array_EmptyIfNoObjectsRegistered()
        {
            var results = new List<object>(Container.ResolveAll<object>());

            Assert.IsNotNull(results);
            Assert.AreEqual(0, results.Count);
        }
    }
}
