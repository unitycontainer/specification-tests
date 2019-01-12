using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unity.Specification.Diagnostic.Property.Validation
{
    public abstract partial class SpecificationTests
    {

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void InvalidValue()
        {
            // Act
            Container.RegisterType<Specification.Property.Injection.ObjectWithThreeProperties>(
                Inject.Property(nameof(Specification.Property.Injection.ObjectWithThreeProperties.Container), Name));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ReadOnlyProperty()
        {
            // Act
            Container.RegisterType<ObjectWithFourProperties>(
                Inject.Property(nameof(ObjectWithFourProperties.ReadOnlyProperty), "test"));
        }
    }
}
