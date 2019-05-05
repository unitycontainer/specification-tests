using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unity.Specification.Diagnostic.Exceptions
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void ConstructorErrorLevel0()
        {
            Exception exception = null;

            // Act
            try { Container.Resolve<string>(); }
            catch (Exception ex) { exception = ex; }

            // Validate
            Assert.IsNotNull(exception.InnerException);
            Assert.AreEqual(1, exception.InnerException.Data.Count);
        }

        [TestMethod]
        public void ConstructorErrorLevel1()
        {
            Exception exception = null;

            // Act
            try { Container.Resolve<ClassWithStringDependency>(); }
            catch (Exception ex) { exception = ex; }

            // Validate
            Assert.IsNotNull(exception.InnerException);
            Assert.AreEqual(4, exception.InnerException.Data.Count);
        }

        [TestMethod]
        public void ConstructorErrorLevel2()
        {
            Exception exception = null;

            // Act
            try { Container.Resolve<ClassWithOtherDependency>(); }
            catch (Exception ex) { exception = ex; }

            // Validate
            Assert.IsNotNull(exception.InnerException);
            Assert.AreEqual(7, exception.InnerException.Data.Count);
        }

        [TestMethod]
        public void ConstructorErrorInterface()
        {
            // Setup
            Exception exception = null;
            Container.RegisterType<IProvider, Provider>();

            // Act
            try { Container.Resolve<IProvider>(); }
            catch (Exception ex) { exception = ex; }

            // Validate
            Assert.IsNotNull(exception.InnerException);
            Assert.AreEqual(6, exception.InnerException.Data.Count);
        }

        [TestMethod]
        public void ConstructorErrorDeep()
        {
            // Setup
            Exception exception = null;
            Container.RegisterType<IProvider, Provider>();

            // Act
            try { Container.Resolve<DependsOnDependOnProvider>(); }
            catch (Exception ex) { exception = ex; }

            // Validate
            Assert.IsNotNull(exception.InnerException);
            Assert.AreEqual(12, exception.InnerException.Data.Count);
        }
    }
}
