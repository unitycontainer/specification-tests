using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Specification.TestData;

namespace Unity.Specification.Resolution
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void Specification_Resolution_Lazy()
        {
            // Setup
            _container.RegisterType<IService, Service>();
            Service.Instances = 0;

            // Act
            var lazy = _container.Resolve<Lazy<IService>>();

            // Verify
            Assert.AreEqual(0, Service.Instances);
            Assert.IsNotNull(lazy);
            Assert.IsNotNull(lazy.Value);
            Assert.AreEqual(1, Service.Instances);
        }

        [TestMethod]
        public void Specification_Resolution_Lazy_Enumerable()
        {
            // Setup
            _container.RegisterType<IService, Service>("1");
            _container.RegisterType<IService, Service>("2");
            _container.RegisterType<IService, Service>("3");
            _container.RegisterType<IService, OtherService>();
            Service.Instances = 0;

            // Act
            var lazy = _container.Resolve<Lazy<IEnumerable<IService>>>();

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
        public void Specification_Resolution_Lazy_Array()
        {
            // Setup
            _container.RegisterType<IService, Service>("1");
            _container.RegisterType<IService, Service>("2");
            _container.RegisterType<IService, Service>("3");
            _container.RegisterType<IService, OtherService>();
            Service.Instances = 0;

            // Act
            var lazy = _container.Resolve<Lazy<IService[]>>();

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
