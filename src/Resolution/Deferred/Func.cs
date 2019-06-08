using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Unity.Specification.Resolution.Deferred
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void Registered()
        {
            // Act
            var resolver = Container.Resolve<Func<IService>>();

            // Verify
            Assert.IsNotNull(resolver);
            Assert.IsInstanceOfType(resolver, typeof(Func<IService>));
        }

        [TestMethod]
        public void ResolvesThroughContainer()
        {
            // Act
            var resolver = Container.Resolve<Func<IService>>();

            // Verify
            var logger = resolver();

            Assert.IsInstanceOfType(resolver, typeof(Func<IService>));
            Assert.IsInstanceOfType(logger, typeof(Service));
        }

        [TestMethod]
        public void GetsInjectedAsADependency()
        {
            // Setup

            // Act
            var result = Container.Resolve<ObjectThatGetsAResolver>();

            // Verify
            Assert.IsNotNull(result.LoggerResolver);
            Assert.IsInstanceOfType(result, typeof(ObjectThatGetsAResolver));
            Assert.IsInstanceOfType(result.LoggerResolver(), typeof(Service));
        }

        [TestMethod]
        public void WithMatchingName()
        {
            // Act
            var resolver = Container.Resolve<Func<IService>>("1");

            // Verify
            Assert.IsNotNull(resolver);
            Assert.IsInstanceOfType(resolver, typeof(Func<IService>));
            Assert.IsInstanceOfType(resolver(), typeof(Service));
        }

        [TestMethod]
        public void WithOtherName()
        {
            // Act
            var resolver = Container.Resolve<Func<IService>>("3");

            // Verify
            Assert.IsNotNull(resolver);
            Assert.IsInstanceOfType(resolver, typeof(Func<IService>));
            Assert.IsInstanceOfType(resolver(), typeof(OtherService));
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void WithNotMatchingName()
        {
            // Act
            var resolver = Container.Resolve<Func<IService>>("10");

            // Verify
            Assert.IsNotNull(resolver);
            Assert.IsInstanceOfType(resolver, typeof(Func<IService>));

            // This must throw
            var instance = resolver();
            Assert.IsNotNull(instance);
            Assert.IsInstanceOfType(instance, typeof(IService));

            Assert.Fail($"Failed to throw and the instance is not null: {null != instance}");
        }

        [TestMethod]
        public void OfIEnumerableCallsResolveAll()
        {
            // Setup
            Container.RegisterInstance("one", "first")
                     .RegisterInstance("two", "second")
                     .RegisterInstance("three", "third");

            // Act
            var resolver = Container.Resolve<Func<IEnumerable<string>>>();

            // Verify
            Assert.IsInstanceOfType(resolver, typeof(Func<IEnumerable<string>>));
            AreEquivalent(new string[] { "first", "second", "third" }, resolver().ToArray() );
        }
    }
}
