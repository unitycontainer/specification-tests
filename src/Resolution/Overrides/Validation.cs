using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Resolution;
using Unity.Specification.TestData;

namespace Unity.Specification.Resolution.Overrides
{
    public abstract partial class SpecificationTests
    {

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatingAPropertyOverrideForANullValueThrows()
        {
            // Act
            new PropertyOverride("ignored", null);
        }
    }
}
