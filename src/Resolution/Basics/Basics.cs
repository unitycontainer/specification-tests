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

        [TestMethod]
        public void NamedType()
        {
            // Arrange
            Container.RegisterType<IFoo, Foo>()
                     .RegisterType<IFoo, Foo1>(Name);

            var instance = Container.Resolve<IFoo>();

            // Act / Validate
            Assert.IsInstanceOfType(instance, typeof(Foo));
            Assert.IsInstanceOfType(Container.Resolve<IFoo>(Name), typeof(Foo1));
        }

        [TestMethod]
        public void NamedInstance()
        {
            // Arrange
            Container.RegisterInstance<IFoo>(new Foo())
                     .RegisterInstance<IFoo>(Name, new Foo1());

            // Act / Validate
            Assert.IsInstanceOfType(Container.Resolve<IFoo>(), typeof(Foo));
            Assert.IsInstanceOfType(Container.Resolve<IFoo>(Name), typeof(Foo1));
        }

        [TestMethod]
        public void NamedFactory()
        {
            // Arrange
            Container.RegisterFactory<IFoo>((c,t,n) => new Foo())
                     .RegisterFactory<IFoo>(Name, (c, t, n) => new Foo1());

            // Act / Validate
            Assert.IsInstanceOfType(Container.Resolve<IFoo>(), typeof(Foo));
            Assert.IsInstanceOfType(Container.Resolve<IFoo>(Name), typeof(Foo1));
        }


        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void NamedTypeNegative()
        {
            // Arrange
            Container.RegisterType<IFoo, Foo>()
                     .RegisterType<IFoo, Foo1>(Name);

            // Act / Validate
            Container.Resolve<IFoo>("none");
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void NamedInstanceNegative()
        {
            // Arrange
            Container.RegisterInstance<IFoo>(new Foo())
                     .RegisterInstance<IFoo>(Name, new Foo1());

            // Act / Validate
            Container.Resolve<IFoo>("none");
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void NamedFactoryNegative()
        {
            // Arrange
            Container.RegisterFactory<IFoo>((c, t, n) => new Foo())
                     .RegisterFactory<IFoo>(Name, (c, t, n) => new Foo1());

            // Act / Validate
            Container.Resolve<IFoo>("none");
        }

    }
}
