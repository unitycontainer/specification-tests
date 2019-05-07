using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unity.Specification.Diagnostic.Exceptions
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void FieldErrorLevel1()
        {
            Exception exception = null;

            // Act
            try { Container.Resolve(typeof(ClassWithStringField)); }
            catch (Exception ex) { exception = ex; }

            // Validate
            Assert.IsNotNull(exception.InnerException);
            Assert.AreEqual(3, exception.InnerException.Data.Count);
        }

        [TestMethod]
        public void FieldErrorLevel2()
        {
            Exception exception = null;

            // Act
            try { Container.Resolve(typeof(ClassWithStringFieldDependency)); }
            catch (Exception ex) { exception = ex; }

            // Validate
            Assert.IsNotNull(exception.InnerException);
            Assert.AreEqual(5, exception.InnerException.Data.Count);
        }


        [TestMethod]
        public void FieldWithNamedType()
        {
            Exception exception = null;

            // Act
            try { Container.Resolve(typeof(ClassWithNamedStringField)); }
            catch (Exception ex) { exception = ex; }

            // Validate
            Assert.IsNotNull(exception.InnerException);
            Assert.AreEqual(3, exception.InnerException.Data.Count);
        }

    }
}
