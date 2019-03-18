using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Constructor.Parameters
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void RefParameter()
        {
            Container.Resolve<TypeWithConstructorWithRefParameter>();
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void OutParameter()
        {
            Container.Resolve<TypeWithConstructorWithOutParameter>();
        }

        [TestMethod]
        public void NoParameters()
        {
            // Act
            var instance = Container.Resolve<Service>();

            // Validate
            Assert.AreEqual(0, instance.Ctor);
        }

        [TestMethod]
        public void WithString()
        {
            // Arrange
            Container.RegisterInstance(Name);

            // Act
            var instance = Container.Resolve<Service>();

            // Validate
            Assert.AreEqual(1, instance.Ctor);
        }

        [TestMethod]
        public void WithUnresolvable()
        {
            // Arrange
            Container.RegisterInstance(Unresolvable.Create());

            // Act
            var instance = Container.Resolve<Service>();

            // Validate
            Assert.AreEqual(2, instance.Ctor);
        }

        [TestMethod]
        public void Longest()
        {
            // Arrange
            Container.RegisterInstance<I1>(new B1())
                     .RegisterInstance(Unresolvable.Create());

            // Act
            var instance = Container.Resolve<Service>();

            // Validate
            Assert.AreEqual(3, instance.Ctor);
        }

    }
}
