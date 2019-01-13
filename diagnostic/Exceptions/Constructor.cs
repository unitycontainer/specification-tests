using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unity.Specification.Diagnostic.Exceptions
{
    public abstract partial class SpecificationTests
    {

        [TestMethod]
        public void ErrorDiagnosticMessage()
        {
            Exception exception = null;

            // Act
            try
            {
                Container.Resolve<SecondLevel>();
            }
            catch (Exception ex)
            {
                exception = ex;
            }


            // Validate
            Assert.IsNotNull(exception.InnerException);
            Assert.AreEqual(4, exception.InnerException.Data.Count);
        }
    }
}
