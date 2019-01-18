using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unity.Specification.Diagnostic.Constructor.Validation
{
    public abstract partial class SpecificationTests
    {
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
