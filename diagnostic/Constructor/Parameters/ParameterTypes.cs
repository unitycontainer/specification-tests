using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Injection;

namespace Unity.Specification.Diagnostic.Constructor.Parameters
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void CanBuildUpExistingObjectOnTypeWithCtorWithRefParameter()
        {
            // Setup
            string ignored = "ignored";
            TypeWithConstructorWithRefParameter instance = new TypeWithConstructorWithRefParameter(ref ignored);
            Container.RegisterType<TypeWithConstructorWithRefParameter>(new InjectionProperty("Property", 10));

            // Act
            Container.BuildUp(instance);

            // Verify
            Assert.AreEqual(10, instance.Property);
        }

        [TestMethod]
        public void CanBuildUpExistingObjectOnTypeWithCtorWithOutParameter()
        {
            // Setup
            string ignored = "ignored";
            TypeWithConstructorWithOutParameter instance = new TypeWithConstructorWithOutParameter(out ignored);
            Container.RegisterType<TypeWithConstructorWithOutParameter>(new InjectionProperty("Property", 10));

            // Act 
            Container.BuildUp(instance);

            // Verify
            Assert.AreEqual(10, instance.Property);
        }
    }
}
