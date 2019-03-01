using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Resolution.Overrides
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void InjectedFieldWithFieldOverride()
        {
            // Setup
            var noOverride = "default";
            var fieldOverride = "custom-via-fieldoverride";

            Container.RegisterType<TestType>(Inject.Field(nameof(TestType.DependencyField), noOverride));
            // Act
            var defaultValue = Container.Resolve<TestType>().DependencyField;
            var fieldValue = Container.Resolve<TestType>(Override.Field(nameof(TestType.DependencyField), fieldOverride))
                                    .DependencyField;
            // Verify
            Assert.AreSame(noOverride, defaultValue);
            Assert.AreSame(fieldOverride, fieldValue);
        }
    }
}
