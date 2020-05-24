using Microsoft.VisualStudio.TestTools.UnitTesting;
using  Unity;

namespace Unity.Specification.Property.Overrides
{
    public abstract partial class SpecificationTests
    {

        [TestMethod]
        public void CanOverrideValue()
        {
            // Act
            var result = Container.Resolve<ObjectWithProperty>(
                Override.Property(nameof(ObjectWithProperty.MyProperty), Resolve.Dependency<ISomething>(Name))
                        .OnType<ObjectWithProperty>());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.MyProperty);
            Assert.IsInstanceOfType(result.MyProperty, typeof(Something2));
        }

        [TestMethod]
        public void CanOverrideAttributedValue()
        {
            var other = "other";

            // Act
            var result = Container.Resolve<ObjectWithThreeProperties>(
                Override.Property(nameof(ObjectWithThreeProperties.Name), other)
                    .OnType<ObjectWithThreeProperties>());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Container);
            Assert.AreEqual<string>(result.Name, other);
        }

        [TestMethod]
        public void CanOverridePropOnAttributed()
        {
            // Arrange
            Container.RegisterType<ObjectWithThreeProperties>(
                new Unity.Injection.InjectionProperty(nameof(ObjectWithThreeProperties.Property), Name));

            // Act
            var other = "other";
            var result = Container.Resolve<ObjectWithThreeProperties>(
                Override.Property(nameof(ObjectWithThreeProperties.Property), other)
                        .OnType<ObjectWithThreeProperties>());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Name);
            Assert.IsNotNull(result.Container);
            Assert.IsNotNull(result.Property);
            Assert.AreEqual((object) other, result.Property);
        }


        [TestMethod]
        public void ValueOverrideForTypeDifferentThanResolvedTypeIsIgnored()
        {
            // Act
            var result = Container.Resolve<ObjectWithProperty>(
                Override.Property(nameof(ObjectWithProperty.MyProperty), Resolve.Dependency<ISomething>(Name))
                        .OnType<ObjectThatDependsOnSimpleObject>());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.MyProperty);
            Assert.IsInstanceOfType(result.MyProperty, typeof(Something1));
        }

    }
}
