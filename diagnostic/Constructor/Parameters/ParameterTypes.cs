using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Injection;

namespace Unity.Specification.Diagnostic.Constructor.Parameters
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        // https://github.com/unitycontainer/container/issues/136
        public void GitHub_Container_135()
        {
            // Setup
            Container.RegisterType<IDecorator, BaseDecorator>(new InjectionConstructor(typeof(Decorator)));

            // Act/Validate
            Assert.IsNotNull(Container.Resolve<BaseDecorator>());
            Assert.IsInstanceOfType(Container.Resolve<IDecorator>(), typeof(BaseDecorator));
        }


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
        public void FalsePositive()
        {
            // Act
            var instance = Container.Resolve<Service>();
        }

    }
}
