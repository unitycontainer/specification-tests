using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Property.Injection
{
    public abstract partial class SpecificationTests
    {
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
        public void ValueNull()
        {
            // Setup
            Container.RegisterType<ObjectWithThreeProperties>(
                Inject.Property(nameof(ObjectWithThreeProperties.Name), null));

            // Act
            var result = Container.Resolve<ObjectWithThreeProperties>();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNull(result.Property);
            Assert.IsNull(result.Name);
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
    }
}
