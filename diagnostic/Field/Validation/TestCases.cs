using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unity.Specification.Diagnostic.Field.Validation
{
    public abstract partial class SpecificationTests
    {

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void InvalidValue()
        {
            // Act
            Container.RegisterType<Specification.Field.Injection.ObjectWithThreeFields>(
                Inject.Field(nameof(Specification.Field.Injection.ObjectWithThreeFields.Container), Name));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ReadOnlyProperty()
        {
            // Act
            Container.RegisterType<ObjectWithFourFields>(
                Inject.Field(nameof(ObjectWithFourFields.ReadOnlyField), "test"));
        }
    }
}
