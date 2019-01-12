using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Diagnostic.Issues.Container
{
    public abstract partial class SpecificationTests
    {

        [TestMethod]
        // https://github.com/unitycontainer/container/issues/129
        public void Issue_126()
        {
            // Setup
            Container.RegisterSingleton<MockLogger>();
            Container.Resolve<MockLogger>();
            Container.RegisterType<ILogger, MockLogger>();
            Container.RegisterType<ObjectWithLotsOfDependencies>();

            // Act
            Container.Resolve<ObjectWithLotsOfDependencies>();

            // Validate
            Assert.IsTrue(((SpyStrategy)Container.Configure<SpyExtension>().Strategy).BuildUpCallCount.ContainsKey((typeof(MockLogger), null)));
            var count = ((SpyStrategy)Container.Configure<SpyExtension>().Strategy).BuildUpCallCount[(typeof(MockLogger), null)];
            Assert.AreEqual(1, count);
        }
    }
}
