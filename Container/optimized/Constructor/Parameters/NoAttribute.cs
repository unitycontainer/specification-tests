using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Constructor.Parameters
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void NoParameters()
        {
            // Act
            var result = Container.Resolve<NoParametersCtor>();

            // Assert
            Assert.AreEqual("none", result.Value);
        }


        [TestMethod]
        public void NoAttributeParameter()
        {
            // Arrange
            Container.RegisterInstance(Name);

            // Act
            var result = Container.Resolve<NoAttributeParameterCtor>();

            // Assert
            Assert.AreEqual(Name, result.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void NoAttributeParameterNegative()
        {
            // Act
            var result = Container.Resolve<NoAttributeParameterCtor>();

            // Assert
            Assert.AreEqual(Name, result.Value);
        }


        [TestMethod]
        public void NoAttributeWithDefault()
        {
            // Arrange
            Container.RegisterInstance(Name);

            // Act
            var result = Container.Resolve<NoAttributeWithDefaultCtor>();

            // Assert
            Assert.AreEqual(Name, result.Value);
        }

        [TestMethod]
        public void NoAttributeWithDefaultNegative()
        {
            // Act
            var result = Container.Resolve<NoAttributeWithDefaultCtor>();

            // Assert
            Assert.AreEqual(DefaultString, result.Value);
        }


        [TestMethod]
        public void NoAttributeWithDefaultValue()
        {
            // Arrange
            Container.RegisterInstance(typeof(int), 1);

            // Act
            var result = Container.Resolve<NoAttributeWithDefaultValueCtor>();

            // Assert
            Assert.AreEqual(1, result.Value);
        }

        [TestMethod]
        public void NoAttributeWithDefaultValueNegative()
        {
            // Act
            var result = Container.Resolve<NoAttributeWithDefaultValueCtor>();

            // Assert
            Assert.AreEqual(DefaultInt, result.Value);
        }


        [TestMethod]
        public void NoAttributeWithDefaultNull()
        {
            // Arrange
            Container.RegisterInstance(typeof(string), null);

            // Act
            var result = Container.Resolve<NoAttributeWithDefaultNullCtor>();

            // Assert
            Assert.AreEqual(null, result.Value);
        }

        [TestMethod]
        public void NoAttributeWithDefaultNullNegative()
        {
            // Act
            var result = Container.Resolve<NoAttributeWithDefaultNullCtor>();

            // Assert
            Assert.AreEqual(null, result.Value);
        }
    }
}
