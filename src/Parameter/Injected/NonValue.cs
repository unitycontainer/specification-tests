using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Specification.Utility;

namespace Unity.Specification.Parameter.Injected
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void NamedDependencyBaseline()
        {
            // Arrange
            Container.RegisterType<Service>(
                Invoke.Method(nameof(Service.NamedDependencyAttribute)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(Name, result.Value);
        }

        [TestMethod]
        public void NamedDependencyInjected()
        {
            var injected = "injected";

            // Arrange
            Container.RegisterType<Service>(
                Invoke.Method(nameof(Service.NamedDependencyAttribute), Inject.Parameter(typeof(string), injected)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(injected, result.Value);
        }

        [TestMethod]
        [Ignore]
        public void NamedDependencyByResolver()
        {
            var injected = "injected";
            var resolver = new ValidatingResolver(injected);

            // Arrange
            Container.RegisterType<Service>(
                Invoke.Method(nameof(Service.NamedDependencyAttribute), 
                    Inject.Parameter(typeof(string), resolver)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(injected, result.Value);
            Assert.AreEqual(typeof(string), resolver.Type);
            Assert.AreEqual(Name, resolver.Name);
        }

        [TestMethod]
        [Ignore]
        public void NamedDependencyByFactory()
        {
            var injected = "injected";
            var resolver = new ValidatingResolverFactory(injected);

            // Arrange
            Container.RegisterType<Service>(
                Invoke.Method(nameof(Service.NamedDependencyAttribute), 
                    Inject.Parameter(typeof(string), resolver)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(injected, result.Value);
            Assert.AreEqual(typeof(string), resolver.Type);
            Assert.AreEqual(Name, resolver.Name);
        }
    }
}
