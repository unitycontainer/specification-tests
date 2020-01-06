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
            var child = parent.CreateChildContainer();

            parent.RegisterType<Service>(TypeLifetime.Hierarchical);
            child.RegisterInstance(Unresolvable.Create());
            
            // Act
            var childService = child.Resolve<Service>();
            var parentService = parent.Resolve<Service>();

            // Validate
            Assert.IsNotNull(childService);
            Assert.IsNotNull(parentService);

            Assert.AreNotSame(childService, parentService);

            Assert.AreEqual(1, childService.Parameters);
            Assert.AreEqual(1, parentService.Parameters);
        }
    }
}
