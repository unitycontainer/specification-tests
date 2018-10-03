using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Lifetime;
using Unity.Specification.TestData;

namespace Unity.Specification.Resolution.Override
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void Mapping()
        {
            using (IUnityContainer container = GetContainer())
            {
                // Arrange
                container.RegisterType(typeof(Foo), new ContainerControlledLifetimeManager());
                container.RegisterType(typeof(IFoo1), typeof(Foo));
                container.RegisterType(typeof(IFoo2), typeof(Foo));

                // Act
                var service1 = container.Resolve<IFoo1>();
                var service2 = container.Resolve<IFoo2>();

                // Assert
                Assert.IsNotNull(service1);
                Assert.IsNotNull(service2);

                Assert.AreSame(service1, service2);
            }
        }
    }
}
