using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Specification.Utility;

namespace Unity.Specification.Parameter.Overrides
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void TypePassedToFactory()
        {
            // Arrange
            var resolver = new ValidatingResolverFactory(1);
            Container.RegisterType<Service>(
                Invoke.Method(nameof(Service.NoAttributeParameter)));

            // Act
            var result = Container.Resolve<Service>(Override.Parameter(typeof(object), resolver));

            // Assert
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOfType(result.Value, typeof(object));
            Assert.AreEqual(1, result.Value);

            Assert.AreEqual(typeof(object), resolver.Type);
        }


        [TestMethod]
        public void TypePassedToFactoryAttr()
        {
            // Arrange
            var resolver = new ValidatingResolverFactory(1);
            Container.RegisterType<Service>(
                Invoke.Method(nameof(Service.DependencyAttribute)));

            // Act
            var result = Container.Resolve<Service>(Override.Parameter(typeof(object), resolver));

            // Assert
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOfType(result.Value, typeof(object));
            Assert.AreEqual(1, result.Value);

            Assert.AreEqual(typeof(object), resolver.Type);
        }

        [TestMethod]
        [Ignore]
        public void TypePassedToFactoryNamed()
        {
            // Arrange
            var value = "value";
            var resolver = new ValidatingResolverFactory(value);
            Container.RegisterType<Service>(
                Invoke.Method(nameof(Service.NamedDependencyAttribute)));

            // Act
            var result = Container.Resolve<Service>(Override.Parameter(typeof(string), resolver));

            // Assert
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOfType(result.Value, typeof(string));
            Assert.AreEqual(value, result.Value);

            Assert.AreEqual(typeof(string), resolver.Type);
            Assert.AreEqual(Name, resolver.Name);
        }


        [TestMethod]
        [Ignore]
        public void TypePassedToFactoryGeneric()
        {
            // Arrange
            var value = "value";
            var resolver = new ValidatingResolverFactory(value);
            Container.RegisterType<Service>(
                Invoke.Method(nameof(Service.NamedDependencyAttribute)));

            // Act
            var result = Container.Resolve<Service>(Override.Parameter<string>(resolver));

            // Assert
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOfType(result.Value, typeof(string));
            Assert.AreEqual(value, result.Value);

            Assert.AreEqual(typeof(string), resolver.Type);
            Assert.AreEqual(Name, resolver.Name);
        }
    }
}
