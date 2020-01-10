using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Constructor.Parameters
{
    public abstract partial class SpecificationTests
    {
        #region Optional

        [TestMethod]
        public void OptionalParameter()
        {
            // Arrange
            Container.RegisterInstance(Name);

            // Act
            var result = Container.Resolve<OptionalParameterCtor>();

            // Assert
            Assert.AreEqual(Name, result.Value);
        }

        [TestMethod]
        public void OptionalParameterNegative()
        {
            // Act
            var result = Container.Resolve<OptionalParameterCtor>();

            // Assert
            Assert.AreEqual(null, result.Value);
        }

        [TestMethod]
        public void OptionalWithDefault()
        {
            // Arrange
            Container.RegisterInstance(Name);

            // Act
            var result = Container.Resolve<OptionalWithDefaultValueCtor>();

            // Assert
            Assert.AreEqual(Name, result.Value);
        }

        [TestMethod]
        public void OptionalWithDefaultNegative()
        {
            // Act
            var result = Container.Resolve<OptionalWithDefaultValueCtor>();

            // Assert
            Assert.AreEqual(DefaultString, result.Value);
        }

        [TestMethod]
        public void OptionalWithDefaultValue()
        {
            // Arrange
            Container.RegisterInstance(typeof(int), 1);

            // Act
            var result = Container.Resolve<OptionalWithDefaultValueCtor>();

            // Assert
            Assert.AreEqual(DefaultString, result.Value);
        }

        [TestMethod]
        public void OptionalWithDefaultValueNegative()
        {
            // Act
            var result = Container.Resolve<OptionalWithDefaultValueCtor>();

            // Assert
            Assert.AreEqual(DefaultString, result.Value);
        }

        [TestMethod]
        public void OptionalWithDefaultNull()
        {
            // Arrange
            Container.RegisterInstance(typeof(string), null);

            // Act
            var result = Container.Resolve<OptionalWithDefaultNullCtor>();

            // Assert
            Assert.AreEqual(null, result.Value);
        }

        [TestMethod]
        public void OptionalWithDefaultNullNegative()
        {
            // Act
            var result = Container.Resolve<OptionalWithDefaultNullCtor>();

            // Assert
            Assert.AreEqual(null, result.Value);
        }

        #endregion

        #region Named

        [TestMethod]
        public void OptionalNamedParameter()
        {
            // Arrange
            Container.RegisterInstance(Name, Name);

            // Act
            var result = Container.Resolve<OptionalNamedParameterCtor>();

            // Assert
            Assert.AreEqual(Name, result.Value);
        }

        [TestMethod]
        public void OptionalUnNamedParameter()
        {
            // Arrange
            Container.RegisterInstance(Name);

            // Act
            var result = Container.Resolve<OptionalNamedParameterCtor>();

            // Assert
            Assert.AreEqual(null, result.Value);
        }

        [TestMethod]
        public void OptionalNamedParameterNegative()
        {
            // Act
            var result = Container.Resolve<OptionalNamedParameterCtor>();

            // Assert
            Assert.AreEqual(null, result.Value);
        }

        [TestMethod]
        public void OptionalNamedWithDefault()
        {
            // Arrange
            Container.RegisterInstance(Name, Name);

            // Act
            var result = Container.Resolve<OptionalNamedWithDefaultCtor>();

            // Assert
            Assert.AreEqual(Name, result.Value);
        }

        [TestMethod]
        public void OptionalUnNamedWithDefault()
        {
            // Arrange
            Container.RegisterInstance(Name);

            // Act
            var result = Container.Resolve<OptionalNamedWithDefaultCtor>();

            // Assert
            Assert.AreEqual(DefaultString, result.Value);
        }

        [TestMethod]
        public void OptionalNamedWithDefaultNegative()
        {
            // Act
            var result = Container.Resolve<OptionalNamedWithDefaultCtor>();

            // Assert
            Assert.AreEqual(DefaultString, result.Value);
        }

        #endregion
    }
}
