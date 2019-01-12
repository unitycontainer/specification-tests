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
            TypeWithConstructorWithOutParameter instance = new TypeWithConstructorWithOutParameter(out _);
            Container.RegisterType<TypeWithConstructorWithOutParameter>(new InjectionProperty("Property", 10));

            // Act 
            Container.BuildUp(instance);

            // Verify
            Assert.AreEqual(10, instance.Property);
        }


        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        // https://github.com/unitycontainer/container/issues/122
        public void GitHub_Container_122()
        {
            Container.RegisterType<I1, C1>();
            Container.RegisterType<I2, C2>();

            //next line returns StackOverflowException
            Container.Resolve<I2>();
        }
    }
}
