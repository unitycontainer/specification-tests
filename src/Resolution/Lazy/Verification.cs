using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Lifetime;

namespace Unity.Specification.Resolution.Lazy
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void CanResolveALazy()
        {
            // Setup
            Container.RegisterType<ILogger, MockLogger>();

            // Act
            var lazy = Container.Resolve<Lazy<ILogger>>();

            // Verify
            Assert.IsNotNull(lazy);
        }

        [TestMethod]
        public void ResolvedLazyHasNoValue()
        {
            // Setup
            Container.RegisterType<ILogger, MockLogger>();

            // Act
            var lazy = Container.Resolve<Lazy<ILogger>>();

            // Verify
            Assert.IsFalse(lazy.IsValueCreated);
        }

        [TestMethod]
        public void ResolvedLazyResolvesThroughContainer()
        {
            // Setup
            Container.RegisterType<ILogger, MockLogger>();

            // Act
            var lazy = Container.Resolve<Lazy<ILogger>>();
            var logger = lazy.Value;

            // Verify
            Assert.IsInstanceOfType(logger, typeof(MockLogger));
        }

        [TestMethod]
        public void ResolvedLazyGetsInjectedAsADependency()
        {
            // Setup
            Container.RegisterType<ILogger, MockLogger>();

            // Act
            var result = Container.Resolve<ObjectThatGetsALazy>();

            // Verify
            Assert.IsNotNull(result.LoggerLazy);
            Assert.IsInstanceOfType(result.LoggerLazy.Value, typeof(MockLogger));
        }

        [TestMethod]
        public void CanResolveLazyWithName()
        {
            // Setup
            Container.RegisterType<ILogger, MockLogger>()
                     .RegisterType<ILogger, SpecialLogger>("special");

            // Act
            var lazy = Container.Resolve<Lazy<ILogger>>("special");

            // Verify
            Assert.IsNotNull(lazy);
        }

        [TestMethod]
        public void ResolvedLazyWithNameResolvedThroughContainerWithName()
        {
            // Setup
            Container
                .RegisterType<ILogger, MockLogger>()
                .RegisterType<ILogger, SpecialLogger>("special");

            // Act
            var lazy = Container.Resolve<Lazy<ILogger>>("special");
            var result = lazy.Value;

            // Verify
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(SpecialLogger));
        }

        [TestMethod]
        public void DifferentResolveCallsReturnDifferentLazyInstances()
        {
            // Setup
            Container
                .RegisterType<ILogger, MockLogger>();

            // Act
            var lazy1 = Container.Resolve<Lazy<ILogger>>();
            var lazy2 = Container.Resolve<Lazy<ILogger>>();

            // Verify
            Assert.AreNotSame(lazy1.Value, lazy2.Value);
        }

        [TestMethod]
        public void DifferentLazyGenericsGetTheirOwnBuildPlan()
        {
            // Setup
            Container
                .RegisterType<ILogger, MockLogger>()
                .RegisterInstance<string>("the instance");

            // Act
            var lazy1 = Container.Resolve<Lazy<ILogger>>();
            var lazy2 = Container.Resolve<Lazy<string>>();

            // Verify
            Assert.IsInstanceOfType(lazy1.Value, typeof(ILogger));
            Assert.AreEqual("the instance", lazy2.Value);
        }

        [TestMethod]
        public void ObservesPerResolveSingleton()
        {
            // Setup
            Container
                .RegisterType<ILogger, MockLogger>()
                .RegisterType(typeof(Lazy<>), new PerResolveLifetimeManager());

            // Act
            var result = Container.Resolve<ObjectThatGetsMultipleLazy>();

            // Verify
            Assert.IsNotNull(result.LoggerLazy1);
            Assert.IsNotNull(result.LoggerLazy2);
            Assert.AreSame(result.LoggerLazy1, result.LoggerLazy2);
            Assert.IsInstanceOfType(result.LoggerLazy1.Value, typeof(MockLogger));
            Assert.IsInstanceOfType(result.LoggerLazy2.Value, typeof(MockLogger));
            Assert.AreSame(result.LoggerLazy1.Value, result.LoggerLazy2.Value);

            var value1 = result.LoggerLazy1.Value;
            var value2 = Container.Resolve<Lazy<ILogger>>().Value;

            Assert.AreNotSame(value1, value2);
        }

        [TestMethod]
        public void ResolvingLazyOfIEnumerableCallsResolveAll()
        {
            // Setup
            Container
                .RegisterInstance("one", "first")
                .RegisterInstance("two", "second")
                .RegisterInstance("three", "third");

            // Act
            var lazy = Container.Resolve<Lazy<IEnumerable<string>>>();
            var result = lazy.Value;

            // Verify
            result.SequenceEqual(new[] {"first", "second", "third"});
        }
    }
}
