using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Unity.Specification.TestData;

namespace Unity.Specification.Constructor.Injection.Validation
{
    public abstract partial class SpecificationTests : Unity.Specification.Constructor.Injection.SpecificationTests
    {
        [TestInitialize]
        public override void Setup() => base.Setup();

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NoReuse()
        {
            // Arrange
            var ctor = Invoke.Constructor();

            // Act
            Container.RegisterType<TypeWithAmbiguousCtors>("1", ctor)
                     .RegisterType<TypeWithAmbiguousCtors>("2", ctor);
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public override void ByCountNamedGeneric() => base.ByCountNamedGeneric();

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public override void MultipleConstructor() => base.MultipleConstructor();
    }
}
