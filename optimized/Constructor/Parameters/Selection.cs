using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Constructor.Parameters
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void ParameterSelection()
        {
            // Act
            var instance = Container.Resolve<TypeWithDynamicParameter>();

            // Validate
            Assert.IsNotNull(instance);
        }

        [TestMethod]
        public void HierarchicalParameterSelection()
        {
            // Arrange
            var parent = GetContainer();
            var child1 = parent.CreateChildContainer();
            var child2 = child1.CreateChildContainer();

            child1.RegisterType<Service>(TypeLifetime.Hierarchical);
            child1.RegisterFactory<DependencyType>((c, t, n) => DependencyType.Create());
            child2.RegisterInstance(Unresolvable.Create());
            
            // Act
            var child2Service = child2.Resolve<Service>();
            var child1Service = child1.Resolve<Service>();

            // Validate
            Assert.IsNotNull(child2Service);
            Assert.IsNotNull(child1Service);

            Assert.AreNotSame(child2Service, child1Service);

            Assert.AreEqual(1, child2Service.Parameters);
            Assert.AreEqual(1, child1Service.Parameters);
        }
    }
}
