using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Specification.Utility;

namespace Unity.Specification.Property.Overrides
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void CanOverrideValueResolver()
        {
            // Act
            var resolver = new ValidatingResolver(new Something2());
            var result = Container.Resolve<ObjectWithProperty>(
                Override.Property(nameof(ObjectWithProperty.MyProperty), resolver));

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.MyProperty);
            Assert.IsInstanceOfType(result.MyProperty, typeof(Something2));
            Assert.AreEqual(typeof(ISomething), resolver.Type);
        }

        [TestMethod]
        public void CanOverrideAttributedValueResolver()
        {
            var other = "other";
            var resolver = new ValidatingResolver(other);

            // Act
            var result = Container.Resolve<ObjectWithThreeProperties>(
                Override.Property(nameof(ObjectWithThreeProperties.Name), resolver));

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Container);
            Assert.AreEqual(result.Name, other);
            Assert.AreEqual(typeof(string), resolver.Type);
        }

        [TestMethod]
        public void CanOverridePropOnAttributedResolver()
        {
            // Arrange
            Container.RegisterType<ObjectWithThreeProperties>(
                new Unity.Injection.InjectionProperty(nameof(ObjectWithThreeProperties.Property), Name));

            // Act
            var other = "other";
            var resolver = new ValidatingResolver(other);
            var result = Container.Resolve<ObjectWithThreeProperties>(
                Override.Property(nameof(ObjectWithThreeProperties.Property), resolver)
                        .OnType<ObjectWithThreeProperties>());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Name);
            Assert.IsNotNull(result.Container);
            Assert.IsNotNull(result.Property);
            Assert.AreEqual(other, result.Property);
            Assert.AreEqual(typeof(object), resolver.Type);
        }
    }
}
