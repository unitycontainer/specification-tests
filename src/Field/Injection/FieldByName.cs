using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unity.Specification.Field.Injection
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void None()
        {
            // Act
            Container.RegisterType<ObjectWithThreeFields>(
                Resolve.Field("Bogus Name"));
        }

        [TestMethod]
        public void ByName()
        {
            // Setup
            Container.RegisterType<ObjectWithThreeFields>(
                Resolve.Field(nameof(ObjectWithThreeFields.Field)));

            // Act
            var result = Container.Resolve<ObjectWithThreeFields>();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Field);
            Assert.IsInstanceOfType(result.Field, typeof(object));
            Assert.AreEqual(result.Name, Name);
            Assert.IsNotNull(result.Container);
        }

        [TestMethod]
        public void ByNameInDerived()
        {
            // Setup
            Container.RegisterType<ObjectWithFourFields>(
                Resolve.Field(nameof(ObjectWithFourFields.Field)));

            // Act
            var result = Container.Resolve<ObjectWithFourFields>();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Field);
            Assert.IsInstanceOfType(result.Field, typeof(object));
            Assert.AreEqual(result.Name, Name);
            Assert.IsNotNull(result.Container);
        }

        [TestMethod]
        public void ByNameOptional()
        {
            // Setup
            Container.RegisterType<ObjectWithOptionalFields>(
                Resolve.OptionalField(nameof(ObjectWithOptionalFields.Field)));

            // Act
            var result = Container.Resolve<ObjectWithOptionalFields>();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNull(result.Field);
            Assert.AreEqual(result.Name, Name);
            Assert.IsNotNull(result.Container);
        }

        [TestMethod]
        public void ByNameOptionalInDerived()
        {
            // Setup
            Container.RegisterType<ObjectWithOptionalFields>(
                Resolve.OptionalField(nameof(ObjectWithOptionalFields.Field)));

            // Act
            var result = Container.Resolve<ObjectWithOptionalFields>();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNull(result.Field);
            Assert.AreEqual(result.Name, Name);
            Assert.IsNotNull(result.Container);
        }

        [TestMethod]
        public void NoneAsDependency()
        {
            // Act
            var result = Container.Resolve<ObjectWithDependency>();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Dependency);
            Assert.IsNull(result.Dependency.Field);
            Assert.AreEqual(result.Dependency.Name, Name);
            Assert.IsNotNull(result.Dependency.Container);
        }
    }
}
