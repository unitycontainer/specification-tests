using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Specification.TestData;

namespace Unity.Specification.Resolution
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void Specification_Resolution_Array()
        {
            // Act
            var array = _container.Resolve<IService[]>();

            // Verify
            Assert.AreEqual(3, Service.Instances);
            Assert.IsNotNull(array);
            Assert.AreEqual(3, array.Length);
        }

        [TestMethod]
        public void Specification_Resolution_Array_Lazy()
        {
            // Act
            var array = _container.Resolve<Lazy<IService>[]>();

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
        public void Specification_Resolution_Array_Func()
        {
            // Act
            var array = _container.Resolve<Func<IService>[]>();

            // Verify
            Assert.IsNotNull(array);
            Assert.AreEqual(3, array.Length);
        }

        [TestMethod]
        public void Specification_Resolution_Array_Lazy_Func()
        {
            // Act
            var array = _container.Resolve<Lazy<Func<IService>>[]>();

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
        public void Specification_Resolution_Array_Func_Lazy()
        {
            // Act
            var array = _container.Resolve<Func<Lazy<IService>>[]>();

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
        public void Specification_Resolution_Array_Func_Lazy_Instance()
        {
            // Setup
            _container.RegisterInstance(null, "Instance", new Lazy<IService>(() => new Service()));

            // Act
            var array = _container.Resolve<Func<Lazy<IService>>[]>();

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
