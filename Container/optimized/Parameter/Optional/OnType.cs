using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Parameter.Optional
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void AttributedWithDefaultOptional()
        {
            // Arrange
            Container.RegisterType<Service>(
                Invoke.Method(nameof(Service.Method),
                    Resolve.Optional()));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOfType(result.Value, typeof(object));
        }

        [TestMethod]
        public void AttributedWithTypedOptional()
        {
            // Arrange
            Container.RegisterType<Service>(
                Invoke.Method(nameof(Service.Method),
                    Resolve.Optional<string>()));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.IsNotNull(result.Value);
            Assert.AreSame(result.Value, Container.Resolve<string>());
        }

        [TestMethod]
        public void AttributedWithTypedMissingOptional()
        {
            // Arrange
            Container.RegisterType<Service>(
                Invoke.Method(nameof(Service.Method),
                    Resolve.Optional<ICommand<int>>()));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.IsNull(result.Value);
        }




        [TestMethod]
        public void Method()
        {
            // Arrange
            Container.RegisterType<Service>(
                Invoke.Method(nameof(Service.MethodOne),
                    Resolve.Parameter<string>()));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.IsNotNull(result.ValueOne);
            Assert.AreEqual(result.ValueOne, Container.Resolve<string>());
        }
    }
}
