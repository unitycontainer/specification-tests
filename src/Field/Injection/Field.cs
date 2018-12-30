using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Injection;

namespace Unity.Specification.Field.Injection
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void None()
        {
            // Act
            var service = Container.Resolve(typeof(object), null, null);

            // Assert
            Assert.IsNotNull(service);
            Assert.IsInstanceOfType(service, typeof(object));
        }

        [TestMethod]
        public void CanInjectOnAttributed()
        {
            // Arrange
            Container.RegisterType<ObjectWithThreeFields>(
                new InjectionField(nameof(ObjectWithThreeFields.Field), Name));

            // Act
            var result = Container.Resolve<ObjectWithThreeFields>();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Name);
            Assert.IsNotNull(result.Container);
            Assert.IsNotNull(result.Field);
        }
    }
}
