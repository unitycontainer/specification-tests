using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Parameter.Resolved
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void AttributedMethodWithValue()
        {
            // Arrange
            Container.RegisterType<Service>(
                Invoke.Method(nameof(Service.Method),
                    Resolve.Parameter<string>()));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.IsNotNull(result.Value);
            Assert.AreSame(result.Value, Container.Resolve<string>());
        }

        [TestMethod]
        public void MethodWithResolvedInt()
        {
            // Arrange
            Container.RegisterType<Service>(
                Invoke.Method(nameof(Service.MethodOne),
                    Resolve.Parameter<int>()));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.IsNotNull(result.ValueOne);
            Assert.AreEqual(result.ValueOne, Container.Resolve<int>());
        }

        [TestMethod]
        public void MethodWithResolvedNamedInt()
        {
            // Arrange
            Container.RegisterType<Service>(
                Invoke.Method(nameof(Service.MethodOne),
                    Resolve.Parameter<int>("1")));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.IsNotNull(result.ValueOne);
            Assert.AreEqual(result.ValueOne, Container.Resolve<int>("1"));
        }

        [TestMethod]
        public void MethodWithResolvedString()
        {
            // Arrange
            Container.RegisterType<Service>(
                Invoke.Method(nameof(Service.MethodOne),
                    Resolve.Parameter<string>()));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.IsNotNull(result.ValueOne);
            Assert.AreSame(result.ValueOne, Container.Resolve<string>());
        }

        [TestMethod]
        public void MethodWithResolvedNamedString()
        {
            // Arrange
            Container.RegisterType<Service>(
                Invoke.Method(nameof(Service.MethodOne),
                    Resolve.Parameter<string>("1")));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.IsNotNull(result.ValueOne);
            Assert.AreSame(result.ValueOne, Container.Resolve<string>("1"));
        }
    }
}
