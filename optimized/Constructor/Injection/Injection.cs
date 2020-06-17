using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Specification.TestData;

namespace Unity.Specification.Constructor.Injection
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public virtual void NoConstructor()
        {
            // Act
            Container.RegisterType<TypeWithAmbiguousCtors>(
                Invoke.Constructor(Resolve.Parameter()));

            // Act
            var instance = Container.Resolve<TypeWithAmbiguousCtors>();
        }

        [TestMethod]
        public virtual void MultipleConstructor()
        {
            // Arrange
            Container.RegisterType<TypeWithAmbiguousCtors>(
                Invoke.Constructor(),
                Invoke.Constructor());

            // Act
            var instance = Container.Resolve<TypeWithAmbiguousCtors>();

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(TypeWithAmbiguousCtors.One, instance.Signature);
        }

    }
}
