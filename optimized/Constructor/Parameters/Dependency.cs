using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Constructor.Parameters
{
    public abstract partial class SpecificationTests
    {
        #region Dependency

        [TestMethod]
        public void DependencyParameter()
        {
            // Arrange
            Container.RegisterInstance(Name);

            // Act
            var result = Container.Resolve<DependencyParameterCtor>();

            // Assert
            Assert.AreEqual(Name, result.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void DependencyParameterNegative()
        {
            // Act
            var result = Container.Resolve<DependencyParameterCtor>();

            // Assert
            Assert.AreEqual(Name, result.Value);
        }

        [TestMethod]
        public void DependencyWithDefault()
        {
            // Arrange
            Container.RegisterInstance(Name);

            // Act
            var result = Container.Resolve<DependencyWithDefaultCtor>();

            // Assert
            Assert.AreEqual(Name, result.Value);
        }

        [TestMethod]
        public void DependencyWithDefaultNegative()
        {
            // Act
            var result = Container.Resolve<DependencyWithDefaultCtor>();

            // Assert
            Assert.AreEqual(DefaultString, result.Value);
        }

        [TestMethod]
        public void DependencyWithDefaultValue()
        {
            // Arrange
            Container.RegisterInstance(typeof(int), 1);

            // Act
            var result = Container.Resolve<DependencyWithDefaultValueCtor>();

            // Assert
            Assert.AreEqual(1, result.Value);
        }

        [TestMethod]
        public void DependencyWithDefaultValueNegative()
        {
            // Act
            var result = Container.Resolve<DependencyWithDefaultValueCtor>();

            // Assert
            Assert.AreEqual(DefaultInt, result.Value);
        }

        [TestMethod]
        public void DependencyWithDefaultNull()
        {
            // Arrange
            Container.RegisterInstance(typeof(string), null);

            // Act
            var result = Container.Resolve<DependencyWithDefaultNullCtor>();

            // Assert
            Assert.AreEqual(null, result.Value);
        }

        [TestMethod]
        public void DependencyWithDefaultNullNegative()
        {
            // Act
            var result = Container.Resolve<DependencyWithDefaultNullCtor>();

            // Assert
            Assert.AreEqual(null, result.Value);
        }

        #endregion

        #region Named

        [TestMethod]
        public void DependencyNamedParameter()
        {
            // Arrange
            Container.RegisterInstance(Name, Name);

            // Act
            var result = Container.Resolve<DependencyNamedParameterCtor>();

            // Assert
            Assert.AreEqual(Name, result.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void DependencyUnNamedParameter()
        {
            // Arrange
            Container.RegisterInstance(Name);

            // Act
            var result = Container.Resolve<DependencyNamedParameterCtor>();

            // Assert
            Assert.AreEqual(Name, result.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void DependencyNamedParameterNegative()
        {
            // Act
            var result = Container.Resolve<DependencyNamedParameterCtor>();

            // Assert
            Assert.AreEqual(Name, result.Value);
        }

        [TestMethod]
        public void DependencyNamedWithDefault()
        {
            // Arrange
            Container.RegisterInstance(Name, Name);

            // Act
            var result = Container.Resolve<DependencyNamedWithDefaultCtor>();

            // Assert
            Assert.AreEqual(Name, result.Value);
        }

        [TestMethod]
        public void DependencyUnNamedWithDefault()
        {
            // Arrange
            Container.RegisterInstance(Name);

            // Act
            var result = Container.Resolve<DependencyNamedWithDefaultCtor>();

            // Assert
            Assert.AreEqual(DefaultString, result.Value);
        }

        [TestMethod]
        public void DependencyNamedWithDefaultNegative()
        {
            // Act
            var result = Container.Resolve<DependencyNamedWithDefaultCtor>();

            // Assert
            Assert.AreEqual(DefaultString, result.Value);
        }

        #endregion
    }
}
