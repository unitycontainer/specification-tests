using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Unity.Specification.Resolution.Deferred
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void Func()
        {
            // Act
            var resolver = Container.Resolve<Func<IService>>();

            // Verify

            Assert.IsNotNull(resolver);
        }

        [TestMethod]
        public void Func_ResolvesThroughContainer()
        {
            // Act
            var resolver = Container.Resolve<Func<IService>>();

            // Verify
            var logger = resolver();

            Assert.IsInstanceOfType(logger, typeof(Service));
        }

        [TestMethod]
        public void Func_GetsInjectedAsADependency()
        {
            // Setup

            // Act
            var result = Container.Resolve<ObjectThatGetsAResolver>();

            // Verify
            Assert.IsNotNull(result.LoggerResolver);
            Assert.IsInstanceOfType(result.LoggerResolver(), typeof(Service));
        }

        [TestMethod]
        public void Func_WithName()
        {
            // Act
            var resolver = Container.Resolve<Func<IService>>("1");

            // Verify
            Assert.IsNotNull(resolver);
        }

        [TestMethod]
        public void Func_WithNameResolvedThroughContainerWithName()
        {
            // Setup

            // Act
            var resolver = Container.Resolve<Func<IService>>("1");
            var result = resolver();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Service));
        }

        [TestMethod]
        public void Func_OfIEnumerableCallsResolveAll()
        {
            // Setup
            Container.RegisterInstance("one", "first")
                     .RegisterInstance("two", "second")
                     .RegisterInstance("three", "third");

            // Act
            var resolver = Container.Resolve<Func<IEnumerable<string>>>();

            // Verify
            AreEquivalent(new string[] { "first", "second", "third" }, resolver().ToArray() );
        }
    }
}
