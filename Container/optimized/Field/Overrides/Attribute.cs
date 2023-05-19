using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Unity.Specification.Property.Overrides.SpecificationTests;

namespace Unity.Specification.Field.Overrides
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void CanOverrideValue()
        {
            Container.RegisterType<ObjectWithField>(Invoke.Constructor(),
                                                    Resolve.Field(nameof(ObjectWithField.MyField)))
                     .RegisterType<ISomething, Something1>()
                     .RegisterType<ISomething, Something2>(Name);

            // Act
            var result = Container.Resolve<ObjectWithField>(
                Override.Field(nameof(ObjectWithField.MyField), Resolve.Dependency<ISomething>(Name))
                        .OnType<ObjectWithField>());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.MyField);
            Assert.IsInstanceOfType(result.MyField, typeof(Something2));
        }

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
