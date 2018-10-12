
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

            Container.RegisterInstance(Name);
        }

    }
}
