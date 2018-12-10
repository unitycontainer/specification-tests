using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Resolution;

namespace Unity.Specification.Resolution.Overrides
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void CanProvideConstructorParameterViaResolveCall()
        {
            // Setup
            const int configuredValue = 15; // Just need a number, value has no significance.
            const int expectedValue = 42; // Just need a number, value has no significance.
            Container.RegisterType<SimpleTestObject>(Execute.Constructor(configuredValue));

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
            Container.RegisterType<SimpleTestObject>(Execute.Constructor(configuredValue));

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
        public void DependencyOverrideOccursEverywhereTypeMatches()
        {
            // Setup
            Container
                .RegisterType<ObjectThatDependsOnSimpleObject>(Resolve.Property("OtherTestObject"))
                .RegisterType<SimpleTestObject>(Execute.Constructor());

            // Act
            var overrideValue = new SimpleTestObject(15); // arbitrary value

            var result = Container.Resolve<ObjectThatDependsOnSimpleObject>(
                new DependencyOverride<SimpleTestObject>(overrideValue));

            // Verify
            Assert.AreSame(overrideValue, result.TestObject);
            Assert.AreSame(overrideValue, result.OtherTestObject);
        }

        [TestMethod]
        public void ParameterOverrideCanResolveOverride()
        {
            // Setup
            Container.RegisterType<ISomething, Something1>()
                     .RegisterType<ISomething, Something2>("other");

            // Act
            var result = Container.Resolve<ObjectTakingASomething>(
                Override.Parameter("something", Resolve.Dependency<ISomething>("other")));

            // Verify
            Assert.IsInstanceOfType(result.MySomething, typeof(Something2));
        }

        [TestMethod]
        public void CanOverridePropertyValueWithNullWithExplicitInjectionParameter()
        {
            // Setup
            Container
                .RegisterType<ObjectTakingASomething>(Execute.Constructor(),
                                                      Resolve.Property("MySomething"))
                .RegisterType<ISomething, Something1>()
                .RegisterType<ISomething, Something2>("other");

            // Act
            var result = Container.Resolve<ObjectTakingASomething>(
                Override.Property(nameof(ObjectTakingASomething.MySomething), Inject.Parameter<ISomething>(null))
                        .OnType<ObjectTakingASomething>());

            // Verify
            Assert.IsNull(result.MySomething);
        }

        [TestMethod]
        public void CanOverrideDependencyWithExplicitInjectionParameterValue()
        {
            // Setup
            Container
                .RegisterType<Outer>(Execute.Constructor(typeof(Inner), 10))
                .RegisterType<Inner>(Execute.Constructor(20, "ignored"));

            // resolves overriding only the parameter for the Bar instance

            // Act
            var instance = Container.Resolve<Outer>(new DependencyOverride<int>(Inject.Parameter(50)).OnType<Inner>());

            // Verify
            Assert.AreEqual(10, instance.LogLevel);
            Assert.AreEqual(50, instance.Inner.LogLevel);
        }

        public class ObjectTakingASomething
        {
            public ISomething MySomething { get; set; }
            public ObjectTakingASomething()
            {
            }

            public ObjectTakingASomething(ISomething something)
            {
                MySomething = something;
            }
        }
    }
}
