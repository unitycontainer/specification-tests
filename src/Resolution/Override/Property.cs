using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Resolution.Override
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void Property()
        {
            // Arrange
            Container.RegisterType(typeof(object));

            // Act
            var result = Container.Resolve<object>();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Property_CanOverrideValue()
        {
            // Act
            var result = Container.Resolve<ObjectTakingASomething>(
                new PropertyOverride("MySomething",
                        new ResolvedParameter<ISomething>("other"))
                    .OnType<ObjectTakingASomething>());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.MySomething);
            Assert.IsInstanceOfType(result.MySomething, typeof(Something2));
        }

        [TestMethod]
        public void Property_CanOverrideAttributedValue()
        {
            var other = "other";

            // Act
            var result = Container.Resolve<ObjectWithThreeProperties>(
                new PropertyOverride(nameof(ObjectWithThreeProperties.Name), other)
                    .OnType<ObjectWithThreeProperties>());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Container);
            Assert.AreEqual(result.Name, other);
        }

        [TestMethod]
        [Ignore]
        public void Property_CanOverridePropOnAttributed()
        {
            // Arrange
            Container.RegisterType<ObjectWithThreeProperties>(
                new InjectionProperty(nameof(ObjectWithThreeProperties.Property), Name));

            // Act
            var other = "other";
            var result = Container.Resolve<ObjectWithThreeProperties>(
                new PropertyOverride(nameof(ObjectWithThreeProperties.Property), other)
                    .OnType<ObjectWithThreeProperties>());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Name);
            Assert.IsNotNull(result.Container);
            Assert.IsNotNull(result.Property);
            Assert.AreEqual(other, result.Property);
        }


        [TestMethod]
        public void Property_ValueOverrideForTypeDifferentThanResolvedTypeIsIgnored()
        {
            // Act
            var result = Container.Resolve<ObjectTakingASomething>(
                new PropertyOverride("MySomething",
                        new ResolvedParameter<ISomething>("other"))
                    .OnType<ObjectThatDependsOnSimpleObject>());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.MySomething);
            Assert.IsInstanceOfType(result.MySomething, typeof(Something1));
        }

    }
}
