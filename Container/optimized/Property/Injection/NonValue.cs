using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Unity.Specification.Utility;

namespace Unity.Specification.Property.Injection
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
            Assert.AreEqual(Other, result.Property);
        }

        [TestMethod]
        public void NamedDependencyInjected()
        {
            var injected = "injected";

            // Setup
            Container.RegisterType<ObjectWithNamedDependency>(
                Inject.Property(nameof(ObjectWithNamedDependency.Property), injected));

            // Act
            var result = Container.Resolve<ObjectWithNamedDependency>();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Container);
            Assert.AreEqual(injected, result.Property);
        }


        [TestMethod]
        public void NamedDependencyByResolver()
        {
            var injected = "injected";
            var resolver = new ValidatingResolver(injected);

            // Setup
            Container.RegisterType<ObjectWithNamedDependency>(
                Inject.Property(nameof(ObjectWithNamedDependency.Property), resolver));

            // Act
            var result = Container.Resolve<ObjectWithNamedDependency>();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Container);
            Assert.AreEqual(injected, result.Property);
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
                Inject.Property(nameof(ObjectWithNamedDependency.Property), resolver));

            // Act
            var result = Container.Resolve<ObjectWithNamedDependency>();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Container);
            Assert.AreEqual(injected, result.Property);
            Assert.AreEqual(typeof(string), resolver.Type);
            Assert.AreEqual(Name, resolver.Name);
        }
    }
}
