using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Constructor.Attribute
{
    public abstract partial class SpecificationTests
    {

        [TestMethod]
        public void WithOptionalDependency()
        {
            // Arrange
            Container.RegisterInstance(_data)
                     .RegisterInstance(Name, Name);

            // Act
            var instance = Container.Resolve<CtorWithOptionalDependency>();

            // Validate
            Assert.AreEqual(_data, instance.Data);
        }


        [TestMethod]
        public void WithOptionalNamedDependency()
        {
            // Arrange
            Container.RegisterInstance(_data)
                     .RegisterInstance(Name, Name);

            // Act
            var instance = Container.Resolve<CtorWithOptionalNamedDependency>();

            // Validate
            Assert.AreEqual(Name, instance.Data);
        }
    }
}
