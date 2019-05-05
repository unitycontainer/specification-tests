using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Lifetime;

namespace Unity.Specification.Diagnostic.Constructor.Types
{
    public abstract partial class SpecificationTests
    {

        [TestMethod]
        public void Unity_54()
        {
            using (IUnityContainer container = GetContainer())
            {
                container.RegisterType(typeof(ITestClass), typeof(TestClass), null, null, null);
                container.RegisterInstance(new TestClass());
                var instance = container.Resolve<ITestClass>(); //0
                Assert.IsNotNull(instance);
            }

            using (IUnityContainer container = GetContainer())
            {
                container.RegisterType(typeof(ITestClass), typeof(TestClass));
                container.RegisterType<TestClass>(new ContainerControlledLifetimeManager());

                try
                {
                    var instance = container.Resolve<ITestClass>(); //2
                    Assert.Fail("Should throw an exception");
                }
                catch (ResolutionFailedException e)
                {
                    Assert.IsInstanceOfType(e, typeof(ResolutionFailedException));
                }

            }
        }
    }
}
