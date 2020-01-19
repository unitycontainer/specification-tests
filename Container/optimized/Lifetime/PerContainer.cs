using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Lifetime
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void PerContainer_Instance_Null()
        {
            // Arrange
            Container.RegisterInstance(typeof(IService), null, null, InstanceLifetime.PerContainer);

            // Act
            var instance = Container.Resolve<IService>();

            // Validate
            Assert.IsNull(instance);
            Assert.IsNull(Container.Resolve<IService>());
        }

        [TestMethod]
        public void PerContainer_Factory_Null()
        {
            // Arrange
            Container.RegisterFactory<IService>(c => null, FactoryLifetime.Singleton);

            // Act
            var instance = Container.Resolve<IService>();

            // Validate
            Assert.IsNull(instance);
            Assert.IsNull(Container.Resolve<IService>());
        }

        [TestMethod]
        public void PerContainer_GenericWithInstances()
        {
            Container.RegisterSingleton(typeof(IFoo<>), typeof(Foo<>));

            var rootContainer = Container as IUnityContainer;

            var childContainer1 = rootContainer.CreateChildContainer();
            var childContainer2 = rootContainer.CreateChildContainer();

            childContainer1.RegisterInstance<IService>(new Service());
            childContainer2.RegisterInstance<IService>(new Service());

            var test1 = childContainer1.Resolve<IFoo<object>>();
            var test2 = childContainer2.Resolve<IFoo<object>>();

            Assert.AreSame(test1, test2);
        }

        [TestMethod]
        public void PerContainer_GenericSingletons()
        {
            Container.RegisterSingleton(typeof(IFoo<>), typeof(Foo<>));

            var rootContainer = Container as IUnityContainer;

            var childContainer1 = rootContainer.CreateChildContainer();
            var childContainer2 = rootContainer.CreateChildContainer();

            var test1 = childContainer1.Resolve<IFoo<object>>();
            var test2 = childContainer2.Resolve<IFoo<object>>();

            Assert.AreSame(test1, test2);
        }
    }
}
