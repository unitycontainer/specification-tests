using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Specification.TestData;

namespace Unity.Specification.Resolution.Lazy
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void Lazy()
        {
            // Setup
            Container.RegisterType<IService, Service>();
            Service.Instances = 0;

            // Act
            var lazy = Container.Resolve<Lazy<IService>>();

            // Verify
            Assert.AreEqual(0, Service.Instances);
            Assert.IsNotNull(lazy);
            Assert.IsNotNull(lazy.Value);
            Assert.AreEqual(1, Service.Instances);
        }

        [TestMethod]
        public void Lazy_Enumerable()
        {
            // Setup
            Container.RegisterType<IService, Service>("1");
            Container.RegisterType<IService, Service>("2");
            Container.RegisterType<IService, Service>("3");
            Container.RegisterType<IService, OtherService>();
            Service.Instances = 0;

            // Act
            var lazy = Container.Resolve<Lazy<IEnumerable<IService>>>();

            // Verify
            Assert.AreEqual(0, Service.Instances);
            Assert.IsNotNull(lazy);
            Assert.IsNotNull(lazy.Value);
            Assert.AreEqual(3, Service.Instances);

            var array = lazy.Value.ToArray();
            Assert.IsNotNull(array);
            Assert.AreEqual(4, array.Length);
        }

        [TestMethod]
        public void Lazy_Array()
        {
            // Setup
            Container.RegisterType<IService, Service>("1");
            Container.RegisterType<IService, Service>("2");
            Container.RegisterType<IService, Service>("3");
            Container.RegisterType<IService, OtherService>();
            Service.Instances = 0;

            // Act
            var lazy = Container.Resolve<Lazy<IService[]>>();

            // Verify
            Assert.AreEqual(0, Service.Instances);
            Assert.IsNotNull(lazy);
            Assert.IsNotNull(lazy.Value);
            Assert.AreEqual(3, Service.Instances);

            var array = lazy.Value.ToArray();
            Assert.IsNotNull(array);
            Assert.AreEqual(3, array.Length);
        }

    }
}
