using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unity.Specification.Diagnostic.Exceptions
{
    public abstract partial class SpecificationTests
    {

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void UserExceptionIsNotWrappad()
        {
            // Arrange
            Container.RegisterFactory<IService>(c => { throw new InvalidOperationException("User error"); });

            // Act
            Container.Resolve<IService>();
        }
    }
}
