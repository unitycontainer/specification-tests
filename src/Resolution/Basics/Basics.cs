using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Resolution.Basics
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void UnityContainer()
        {
            Assert.IsNotNull(Container.Resolve<IUnityContainer>());
        }

        [TestMethod]
        public void CanCreateObjectFromUnConfiguredContainer()
        {
            // Act/Verify
            Assert.IsNotNull(Container.Resolve<object>());
        }

        [TestMethod]
        public void ContainerResolvesRecursiveConstructorDependencies()
        {
            // Act
            var dep = Container.Resolve<ObjectWithOneDependency>();

            // Verify
            Assert.IsNotNull(dep);
            Assert.IsNotNull(dep.InnerObject);
            Assert.AreNotSame(dep, dep.InnerObject);
        }


        [TestMethod]
        public void ContainerResolvesMultipleRecursiveConstructorDependencies()
        {
            // Act
            var dep = Container.Resolve<ObjectWithTwoConstructorDependencies>();

            // Verify
            dep.Validate();
        }

    }
}
