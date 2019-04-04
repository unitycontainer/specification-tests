using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unity.Specification.Diagnostic.Constructor.Injection
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NoReuse()
        {
            // Arrange
            var ctor = Invoke.Constructor();

            // Act
            Container.RegisterType<ObjectWithAmbiguousConstructors>("1", ctor)
                     .RegisterType<ObjectWithAmbiguousConstructors>("2", ctor);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NoDefaultConstructor()
        {
            // Act
            Container.RegisterType<ObjectWithAmbiguousConstructors>("Test",
                TypeLifetime.PerContainer,
                Invoke.Constructor());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AmbiguousConstructor()
        {
            // Act
            Container.RegisterType<ObjectWithAmbiguousConstructors>(
                Invoke.Constructor(Resolve.Parameter()));
        }


    }
}
