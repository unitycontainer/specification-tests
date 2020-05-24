using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Resolution.Overrides
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void InjectedPropertyWithPropertyOverride()
        {
            // Setup
            var noOverride = "default";
            var propOverride = "custom-via-propertyoverride";

            Container.RegisterType<TestType>(Inject.Property(nameof(TestType.DependencyProperty), noOverride));
            // Act
            var defaultValue = Container.Resolve<TestType>().DependencyProperty;
            var propValue = Container.Resolve<TestType>(Override.Property(nameof(TestType.DependencyProperty), propOverride))
                                    .DependencyProperty;
            // Verify
            Assert.AreSame(noOverride, defaultValue);
            Assert.AreSame(propOverride, propValue);
        }
    }
}
