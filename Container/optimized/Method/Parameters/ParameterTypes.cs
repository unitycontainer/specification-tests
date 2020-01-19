using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unity.Specification.Method.Parameters
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void RefParameter()
        {
            // Act
            Container.Resolve<TypeWithMethodWithRefParameter>();
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void OutParameter()
        {
            // Act
            Container.Resolve<TypeWithMethodWithOutParameter>();
        }
    }
}
