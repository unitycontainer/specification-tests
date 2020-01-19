using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Resolution;

namespace Unity.Specification.Constructor.Overrides
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void OverrideDependencyLegacy()
        {
            // Arrange
            Container.RegisterInstance(_data)
                     .RegisterInstance(Name, Name);

            // Act
            var instance = Container.Resolve<CtorWithDependency>(new DependencyOverride(typeof(string), _override));

            // Validate
            Assert.AreEqual(_override, instance.Data);
        }


        [TestMethod]
        public void OverrideNamedDependencyLegacy()
        {
            // Arrange
            Container.RegisterInstance(_data)
                     .RegisterInstance(Name, Name);

            // Act
            var instance = Container.Resolve<CtorWithNamedDependency>(new DependencyOverride(typeof(string), Name, _override));

            // Validate
            Assert.AreEqual(_override, instance.Data);
        }

        [TestMethod]
        public void OverrideDependency()
        {
            // Arrange
            Container.RegisterInstance(_data)
                     .RegisterInstance(Name, Name);

            // Act
            var instance = Container.Resolve<CtorWithDependency>(Override.Dependency<string>(_override));

            // Validate
            Assert.AreEqual(_override, instance.Data);
        }


        [TestMethod]
        public void OverrideNamedDependency()
        {
            // Arrange
            Container.RegisterInstance(_data)
                     .RegisterInstance(Name, Name);

            // Act
            var instance = Container.Resolve<CtorWithNamedDependency>(Override.Dependency<string>(Name, _override));

            // Validate
            Assert.AreEqual(_override, instance.Data);
        }
    }
}
