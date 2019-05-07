using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unity.Specification.Diagnostic.Exceptions
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void PropertyErrorLevel1()
        {
            Exception exception = null;

            // Act
            try { Container.Resolve(typeof(ClassWithStringProperty)); }
            catch (Exception ex) { exception = ex; }

            // Validate
            Assert.IsNotNull(exception.InnerException);
            Assert.AreEqual(3, exception.InnerException.Data.Count);
        }

        [TestMethod]
        public void PropertyErrorLevel2()
        {
            Exception exception = null;

            // Act
            try { Container.Resolve(typeof(ClassWithStringPropertyDependency)); }
            catch (Exception ex) { exception = ex; }

            // Validate
            Assert.IsNotNull(exception.InnerException);
            Assert.AreEqual(5, exception.InnerException.Data.Count);
        }

        [TestMethod]
        public void PropertyWithNamedType()
        {
            Exception exception = null;

            // Act
            try { Container.Resolve(typeof(ClassWithNamedStringProperty)); }
            catch (Exception ex) { exception = ex; }

            // Validate
            Assert.IsNotNull(exception.InnerException);
            Assert.AreEqual(3, exception.InnerException.Data.Count);
        }

    }
}
