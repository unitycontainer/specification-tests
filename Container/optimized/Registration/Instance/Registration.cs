﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Unity.Lifetime;

namespace Unity.Specification.Registration.Instance
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void IsNotNull()
        {
            // Arrange
            Container.RegisterInstance(typeof(IService), null, Unresolvable.Create(), InstanceLifetime.Singleton);
            
            // Validate
            Assert.IsNotNull(Container.Resolve<IService>());
        }

        [TestMethod]
        public void IsNull()
        {
            // Arrange
            Container.RegisterInstance(typeof(IService), null, null, InstanceLifetime.Singleton);

            // Validate
            Assert.IsNull(Container.Resolve<IService>());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowsOnNullNull()
        {
            Container.RegisterInstance(null, null, null, InstanceLifetime.Singleton);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Null_Null_Null()
        {
            // Act
            Container.RegisterInstance(null, null, null, null);
        }

        [TestMethod]
        public void Null_Null_Instance()
        {
            // Arrange
            var value = new object();
            Container.RegisterInstance(null, null, value, null);

            // Act
            var instance = Container.Resolve<object>();

            // Validate
            Assert.AreSame(value, instance);
        }

        [TestMethod]
        public void Null_Name_Instance()
        {
            // Arrange
            var value = new object();
            Container.RegisterInstance(null, Name, value, null);

            // Act
            var instance = Container.Resolve<object>(Name);

            // Validate
            Assert.AreSame(value, instance);
        }

        [TestMethod]
        public void Type_Null_Null()
        {
            // Arrange
            Container.RegisterInstance(typeof(object), null, null, null);

            // Act
            var instance = Container.Resolve<object>();

            // Validate
            Assert.IsNull(instance);
        }

        [TestMethod]
        public void Type_Null_Instance()
        {
            // Arrange
            Container.RegisterInstance(typeof(object), null, Name, null);

            // Act
            var instance = Container.Resolve<object>();

            // Validate
            Assert.AreSame(Name, instance);
        }

        [TestMethod]
        public void Type_Name_Instance()
        {
            // Arrange
            Container.RegisterInstance(typeof(object), Name, Name, null);

            // Act
            var instance = Container.Resolve<object>(Name);

            // Validate
            Assert.AreSame(Name, instance);
        }

        [TestMethod]
        public void DefaultLifetime()
        {
            // Arrange
            var value = new object();
            Container.RegisterInstance(typeof(object), null, value, null);

            // Act
            var registration = Container.Registrations.First(r => typeof(object) == r.RegisteredType);

            // Validate
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(ContainerControlledLifetimeManager));
        }

        // TODO: UNITY_8
        //[TestMethod]
        //public void CanSetLifetime()
        //{
        //    // Arrange
        //    var value = new object();
        //    Container.RegisterInstance(typeof(object), null, value, InstanceLifetime.Singleton);

        //    // Act
        //    var registration = Container.Registrations.First(r => typeof(object) == r.RegisteredType);

        //    // Validate
        //    Assert.IsInstanceOfType(registration.LifetimeManager, typeof(SingletonLifetimeManager));
        //}

    }
}
