using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Property.Injection
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void BaseLine()
        {
            // Act
            var result = Container.Resolve<ObjectWithThreeProperties>();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNull(result.Property);
            Assert.AreEqual(result.Name, Name);
            Assert.IsNotNull(result.Container);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void None()
        {
            // Act
            Container.RegisterType<ObjectWithThreeProperties>(
                Inject.Property("Bogus Name"));
        }

        [TestMethod]
        public void ByName()
        {
            // Setup
            Container.RegisterType<ObjectWithThreeProperties>(
                Inject.Property(nameof(ObjectWithThreeProperties.Property)));

            // Act
            var result = Container.Resolve<ObjectWithThreeProperties>();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Property);
            Assert.IsInstanceOfType(result.Property, typeof(object));
            Assert.AreEqual(result.Name, Name);
            Assert.IsNotNull(result.Container);
        }

        [TestMethod]
        public void ByNameValue()
        {
            // Setup
            var test = "test";
            Container.RegisterType<ObjectWithThreeProperties>(
                Inject.Property(nameof(ObjectWithThreeProperties.Property), test));

            // Act
            var result = Container.Resolve<ObjectWithThreeProperties>();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Property);
            Assert.AreSame(result.Property, test);
            Assert.AreEqual(result.Name, Name);
            Assert.IsNotNull(result.Container);
        }

        [TestMethod]
        public void ByNameInDerived()
        {
            // Setup
            Container.RegisterType<ObjectWithFourProperties>(
                Inject.Property(nameof(ObjectWithFourProperties.Property)));

            // Act
            var result = Container.Resolve<ObjectWithFourProperties>();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Property);
            Assert.IsInstanceOfType(result.Property, typeof(object));
            Assert.AreEqual(result.Name, Name);
            Assert.IsNotNull(result.Container);
        }

        [TestMethod]
        public void ByNameValueInDerived()
        {
            // Setup
            var test = "test";
            Container.RegisterType<ObjectWithFourProperties>(
                Inject.Property(nameof(ObjectWithFourProperties.Property), test));

            // Act
            var result = Container.Resolve<ObjectWithFourProperties>();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Property);
            Assert.AreSame(result.Property, test);
            Assert.AreEqual(result.Name, Name);
            Assert.IsNotNull(result.Container);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ReadOnlyProperty()
        {
            // Act
            Container.RegisterType<ObjectWithFourProperties>(
                Inject.Property(nameof(ObjectWithFourProperties.ReadOnlyProperty), "test"));
        }

        [TestMethod]
        public void NoneAsDependency()
        {
            // Act
            var result = Container.Resolve<ObjectWithDependency>();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Dependency);
            Assert.IsNull(result.Dependency.Property);
            Assert.AreEqual(result.Dependency.Name, Name);
            Assert.IsNotNull(result.Dependency.Container);
        }

    }
}
