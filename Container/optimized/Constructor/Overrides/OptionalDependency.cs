using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Resolution;

namespace Unity.Specification.Constructor.Overrides
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void OverrideOptionalDependencyLegacy()
        {
            // Arrange
            Container.RegisterInstance(_data)
                     .RegisterInstance(Name, Name);

            // Act
            var instance = Container.Resolve<CtorWithOptionalDependency>(new DependencyOverride(typeof(string), _override));

            // Validate
            Assert.AreEqual(_override, instance.Data);
        }


        [TestMethod]
        public void OverrideOptionalNamedDependencyLegacy()
        {
            // Arrange
            Container.RegisterInstance(_data)
                     .RegisterInstance(Name, Name);

            // Act
            var instance = Container.Resolve<CtorWithOptionalNamedDependency>(new DependencyOverride(typeof(string), Name, _override));

            // Validate
            Assert.AreEqual(_override, instance.Data);
        }

        [TestMethod]
        public void OverrideOptionalDependency()
        {
            // Arrange
            Container.RegisterInstance(_data)
                     .RegisterInstance(Name, Name);

            // Act
            var instance = Container.Resolve<CtorWithOptionalDependency>(Override.Dependency<string>(_override));

            // Validate
            Assert.AreEqual(_override, instance.Data);
        }


        [TestMethod]
        public void OverrideOptionalNamedDependency()
        {
            // Arrange
            Container.RegisterInstance(_data)
                     .RegisterInstance(Name, Name);

            // Act
            var instance = Container.Resolve<CtorWithOptionalNamedDependency>(Override.Dependency<string>(Name, _override));

            // Validate
            Assert.AreEqual(_override, instance.Data);
        }
    }
}
