using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Injection;

namespace Unity.Specification.Issues.GitHub
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        // https://github.com/unitycontainer/abstractions/issues/96
        public virtual void Abstractions_96()
        {
            // Act
            var ctor = new InjectionConstructor();
            Container.RegisterType<IService, Service>(ctor);
            Container.RegisterType<IService, Service>("name", ctor);
        }

        [TestMethod]
        // https://github.com/unitycontainer/abstractions/issues/83
        public void Abstractions_83()
        {
            // Arrange
            Container.RegisterInstance(Name);
            Container.RegisterType<ObjectWithThreeProperties>(
                Inject.Property(nameof(ObjectWithThreeProperties.Property), Name));

            // Act
            var result = Container.Resolve<ObjectWithThreeProperties>();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Name);
            Assert.IsNotNull(result.Container);
            Assert.IsNotNull(result.Property);
        }
    }
}
