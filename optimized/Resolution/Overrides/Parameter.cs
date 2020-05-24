using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Resolution;

namespace Unity.Specification.Resolution.Overrides
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void OptionalViaParameter()
        {
            // Setup
            IService x = new Service1();
            IService y = new Service2();

            // Act
            var result = Container.Resolve<Foo>(
                    Override.Parameter("x", x),
                    Override.Parameter("y", y));

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Fred);
            Assert.IsNotNull(result.George);
        }

        [TestMethod]
        public void CanProvideConstructorParameterViaResolveCall()
        {
            // Setup
            const int configuredValue = 15; // Just need a number, value has no significance.
            const int expectedValue = 42; // Just need a number, value has no significance.
            Container.RegisterType<SimpleTestObject>(Invoke.Constructor(configuredValue));

            // Act
            var result = Container.Resolve<SimpleTestObject>(Override.Parameter("x", expectedValue));

            // Verify
            Assert.AreEqual(expectedValue, result.X);
        }

        [TestMethod]
        public void OverrideDoeNotLastAfterResolveCall()
        {
            // Setup
            const int configuredValue = 15; // Just need a number, value has no significance.
            const int overrideValue = 42; // Just need a number, value has no significance.
            Container.RegisterType<SimpleTestObject>(Invoke.Constructor(configuredValue));

            // Act
            Container.Resolve<SimpleTestObject>(Override.Parameter("x", overrideValue)
                                                        .OnType<SimpleTestObject>());
            var result = Container.Resolve<SimpleTestObject>();

            // Verify
            Assert.AreEqual(configuredValue, result.X);
        }

        [TestMethod]
        public void OverrideIsUsedInRecursiveBuilds()
        {
            // Setup
            const int expectedValue = 42; // Just need a number, value has no significance.

            // Act
            var result = Container.Resolve<ObjectThatDependsOnSimpleObject>(
                Override.Parameter("x", expectedValue));

            // Verify
            Assert.AreEqual(expectedValue, result.TestObject.X);
        }

        [TestMethod]
        public void NonMatchingOverridesAreIgnored()
        {
            // Setup
            const int expectedValue = 42; // Just need a number, value has no significance.

            // Act
            var result = Container.Resolve<SimpleTestObject>(
                new ParameterOverrides
                {
                    { "y", expectedValue * 2 },
                    { "x", expectedValue }
                }.OnType<SimpleTestObject>());

            // Verify
            Assert.AreEqual(expectedValue, result.X);
        }

        [TestMethod]
        public void NonMatchingOverridesAreIgnoredAlternative()
        {
            // Setup
            const int expectedValue = 42; // Just need a number, value has no significance.

            // Act
            var result = Container.Resolve<SimpleTestObject>(
                Override.Parameter("x", expectedValue),
                Override.Parameter("y", expectedValue * 2));

            // Verify
            Assert.AreEqual(expectedValue, result.X);
        }

        [TestMethod]
        public void InjectedParameterWithParameterOverride()
        {
            // Setup
            var noOverride = "default";
            var parOverride = "custom-via-parameteroverride";

            Container.RegisterType<TestType>(Invoke.Constructor(noOverride));
            // Act
            var defaultValue = Container.Resolve<TestType>().ToString();
            var parValue = Container.Resolve<TestType>(Override.Parameter<string>(parOverride))
                                    .ToString();
            // Verify
            Assert.AreSame(noOverride, defaultValue);
            Assert.AreSame(parOverride, parValue);
        }
    }
}
