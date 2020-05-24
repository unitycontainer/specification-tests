using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Parameter.Injected
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void InjectedAttributedBaseline()
        {
            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.IsNull(result.ValueOne);
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOfType(result.Value, typeof(object));
        }

        [TestMethod]
        public void InjectedAttributedMethod()
        {
            // Arrange
            Container.RegisterType<Service>(
                Invoke.Method(nameof(Service.Method)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOfType(result.Value, typeof(object));
        }

        [TestMethod]
        public void InjectedAttributedMethodWithValue()
        {
            var data = "value";

            // Arrange
            Container.RegisterType<Service>(
                Invoke.Method(nameof(Service.Method),
                    Inject.Parameter(data)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.IsNotNull(result.Value);
            Assert.AreSame(result.Value, data);
        }

        [TestMethod]
        public void WithInjectedInt()
        {
            // Arrange
            Container.RegisterType<Service>(
                Invoke.Method(nameof(Service.MethodOne), 
                    Inject.Parameter(1)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOfType(result.Value, typeof(object));
            Assert.IsNotNull(result.ValueOne);
            Assert.IsInstanceOfType(result.ValueOne, typeof(int));
        }

        [TestMethod]
        public void WithInjectedString()
        {
            // Arrange
            Container.RegisterType<Service>(
                Invoke.Method(nameof(Service.MethodOne),
                    Inject.Parameter("test")));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOfType(result.Value, typeof(object));
            Assert.IsNotNull(result.ValueOne);
            Assert.IsInstanceOfType(result.ValueOne, typeof(string));
        }

        [TestMethod]
        public void WithInjectedIntString()
        {
            // Arrange
            Container.RegisterType<Service>(
                Invoke.Method(nameof(Service.MethodTwo),
                    Inject.Parameter(1),
                    Inject.Parameter("test")));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOfType(result.Value, typeof(object));
            Assert.IsNotNull(result.ValueOne);
            Assert.IsInstanceOfType(result.ValueOne, typeof(int));
            Assert.IsNotNull(result.ValueTwo);
            Assert.IsInstanceOfType(result.ValueTwo, typeof(string));
        }

        [TestMethod]
        public void WithInjectedStringInt()
        {
            // Arrange
            Container.RegisterType<Service>(
                Invoke.Method(nameof(Service.MethodTwo),
                    Inject.Parameter("test"),
                    Inject.Parameter(1)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOfType(result.Value, typeof(object));
            Assert.IsNotNull(result.ValueOne);
            Assert.IsInstanceOfType(result.ValueOne, typeof(string));
            Assert.IsNotNull(result.ValueTwo);
            Assert.IsInstanceOfType(result.ValueTwo, typeof(int));
        }
    }
}
