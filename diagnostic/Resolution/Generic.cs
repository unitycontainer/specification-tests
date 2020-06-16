using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unity.Specification.Diagnostic.Resolution.Generic
{
    public abstract partial class SpecificationTests : Specification.Resolution.Generic.SpecificationTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public override void AppropriateExceptionIsThrownWhenNoMatchingConstructorCanBeFound()
        {
            base.AppropriateExceptionIsThrownWhenNoMatchingConstructorCanBeFound();
        }
    }
}
