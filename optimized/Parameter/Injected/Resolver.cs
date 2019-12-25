using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Specification.Utility;

namespace Unity.Specification.Parameter.Injected
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        [Ignore]
        public void WithInjectedResolver()
        {
            // Arrange
            var resolver = new ValidatingResolver(1);
            Container.RegisterType<Service>(
                Invoke.Method(nameof(Service.MethodOne),
                    Inject.Parameter(resolver)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOfType(result.Value, typeof(object));
            Assert.IsNotNull(result.ValueOne);
            Assert.IsInstanceOfType(result.ValueOne, typeof(int));
            Assert.AreEqual(1, result.Value);
        }
    }

}
