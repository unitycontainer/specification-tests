using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Constructor.Overrides
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void CtorParameter()
        {
            // Arrange
            Container.RegisterType<Service>(Invoke.Constructor(_data));

            // Act
            var value = Container.Resolve<Service>(Override.Dependency<string>(_override));

            // Verify
            Assert.AreSame(_data, value.Data);
        }
    }
}
