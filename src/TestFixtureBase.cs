using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Specification.TestData;

namespace Unity.Specification
{
    public abstract class TestFixtureBase
    {
        protected IUnityContainer Container;
        protected string Name = "name";

        public abstract IUnityContainer GetContainer();


        public virtual void Setup()
        {
            Container = GetContainer();

            Container.RegisterType<ILogger, MockLogger>();
            Container.RegisterType<ILogger, MockLogger>(Name);

            var service = new Service();
            Container.RegisterInstance<IService>(service);
            Container.RegisterInstance<IService>(Name, service);

            Container.RegisterType(typeof(IFoo<>), typeof(Foo<>));
            Container.RegisterType(typeof(IFoo<>), typeof(Foo<>), Name);
        }

    }
}
