using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Specification.Utility;

namespace Unity.Specification.Field.Injection
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void NamedDependencyBaseline()
        {
            // Act
            var result = Container.Resolve<ObjectWithNamedDependency>();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Container);
            Assert.AreEqual(Name, result.Field);
        }
        
        [TestMethod]
        public void NamedDependencyInjected()
        {
            var injected = "injected";

            // Setup
            Container.RegisterType<ObjectWithNamedDependency>(
                Inject.Field(nameof(ObjectWithNamedDependency.Field), injected));

            // Act
            var result = Container.Resolve<ObjectWithNamedDependency>();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Container);
            Assert.AreEqual(injected, result.Field);
        }

        [TestMethod]
        public void NamedDependencyByResolver()
        {
            var injected = "injected";
            var resolver = new ValidatingResolver(injected);

            // Setup
            Container.RegisterType<ObjectWithNamedDependency>(
                Inject.Field(nameof(ObjectWithNamedDependency.Field), resolver));

            // Act
            var result = Container.Resolve<ObjectWithNamedDependency>();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Container);
            Assert.AreEqual(injected, result.Field);
            Assert.AreEqual(typeof(string), resolver.Type);
            Assert.AreEqual(Name, resolver.Name);
        }

        [TestMethod]
        public void NamedDependencyByFactory()
        {
            // Setup
            var injected = "injected";
            var resolver = new ValidatingResolverFactory(injected);
            Container.RegisterType<ObjectWithNamedDependency>(
                Inject.Field(nameof(ObjectWithNamedDependency.Field), resolver));

            // Act
            var result = Container.Resolve<ObjectWithNamedDependency>();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Container);
            Assert.AreEqual(injected, result.Field);
            Assert.AreEqual(typeof(string), resolver.Type);
            Assert.AreEqual(Name, resolver.Name);
        }
    }
}
