using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unity.Specification.Field.Injection
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void ByNameValue()
        {
            // Setup
            var test = "test";
            Container.RegisterType<ObjectWithThreeFields>(
                Inject.Field(nameof(ObjectWithThreeFields.Field), test));

            // Act
            var result = Container.Resolve<ObjectWithThreeFields>();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Field);
            Assert.AreSame(result.Field, test);
            Assert.AreEqual(result.Name, Name);
            Assert.IsNotNull(result.Container);
        }

        [TestMethod]
        public void ValueNull()
        {
            // Setup
            Container.RegisterType<ObjectWithThreeFields>(
                Inject.Field(nameof(ObjectWithThreeFields.Name), null));

            // Act
            var result = Container.Resolve<ObjectWithThreeFields>();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNull(result.Field);
            Assert.IsNull(result.Name);
            Assert.IsNotNull(result.Container);
        }

        [TestMethod]
        public void ByNameValueInDerived()
        {
            // Setup
            var test = "test";
            Container.RegisterType<ObjectWithFourFields>(
                Inject.Field(nameof(ObjectWithFourFields.Field), test));

            // Act
            var result = Container.Resolve<ObjectWithFourFields>();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Field);
            Assert.AreSame(result.Field, test);
            Assert.AreEqual(result.Name, Name);
            Assert.IsNotNull(result.Container);
        }
    }
}
