using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Field.Overrides
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void FieldOverrideAttribute()
        {
            // Act
            var result = Container.Resolve<ObjectWithAttributes>(
                Override.Field(nameof(ObjectWithAttributes.Dependency), null),
                Override.Field(nameof(ObjectWithAttributes.Optional), Name1));

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNull(result.Dependency);
            Assert.IsNotNull(result.Optional);
            Assert.AreEqual(result.Optional, Name1);
        }

        [TestMethod]
        public void DependencyOverrideFieldValue()
        {
            var other = "other";

            // Act
            var result = Container.Resolve<ObjectWithAttributes>(
                Override.Field(nameof(ObjectWithAttributes.Dependency), null),
                Override.Dependency(other, other));

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNull(result.Dependency);
            Assert.IsNotNull(result.Optional);
            Assert.AreEqual(result.Optional, other);
        }

        [TestMethod]
        public void ValueOverAttribute()
        {
            // Setup
            Container.RegisterType<ObjectWithAttributes>(
                Inject.Field(nameof(ObjectWithAttributes.Dependency), Name2));

            // Act
            var result = Container.Resolve<ObjectWithAttributes>(Override.Field(nameof(ObjectWithAttributes.Dependency), Name));

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Dependency);
            Assert.AreEqual(result.Dependency, Name);
            Assert.IsNull(result.Optional);
        }

        [TestMethod]
        public void NullOverAttribute()
        {
            // Setup
            Container.RegisterType<ObjectWithAttributes>(
                Inject.Field(nameof(ObjectWithAttributes.Dependency), Name2));

            // Act
            var result = Container.Resolve<ObjectWithAttributes>(Override.Field(nameof(ObjectWithAttributes.Dependency), null));

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNull(result.Dependency);
            Assert.IsNull(result.Optional);
        }

    }
}
