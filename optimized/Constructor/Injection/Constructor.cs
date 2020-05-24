using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Constructor.Injection
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void InjectRegistered()
        {
            // Arrange
            Container.RegisterInstance(_data);

            // Act
            var value = Container.Resolve<Foo>();

            // Verify
            Assert.AreSame(_data, value.Data);
        }

        [TestMethod]
        public void InjectionCtor()
        {
            // Arrange
            Container.RegisterInstance(_data)
                     .RegisterType<Foo>(Invoke.Constructor(_override));

            // Act
            var value = Container.Resolve<Foo>();

            // Verify
            Assert.AreSame(_override, value.Data);
        }
    }
}
