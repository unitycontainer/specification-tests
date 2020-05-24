using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Constructor.Attribute
{
    public abstract partial class SpecificationTests
    {

        [TestMethod]
        public void WithDependency()
        {
            // Arrange
            Container.RegisterInstance(_data)
                     .RegisterInstance(Name, Name);

            // Act
            var instance = Container.Resolve<CtorWithDependency>();

            // Validate
            Assert.AreEqual(_data, instance.Data);
        }

        [TestMethod]
        public void WithNamedDependency()
        {
            // Arrange
            Container.RegisterInstance(_data)
                     .RegisterInstance(Name, Name);

            // Act
            var instance = Container.Resolve<CtorWithNamedDependency>();

            // Validate
            Assert.AreEqual(Name, instance.Data);
        }


        [TestMethod]
        public void ChecksForDependencyName()
        {
            // Arrange
            Container.RegisterInstance(_data)
                     .RegisterInstance(null, Name)
                     .RegisterInstance("OtherName", Name);

            // Act
            var instance = Container.Resolve<CtorWithNamedDependency>();

            // Validate
            Assert.AreEqual(null, instance.Data);
        }

    }
}
