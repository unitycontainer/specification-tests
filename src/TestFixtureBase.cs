
namespace Unity.Specification
{
    public abstract class TestFixtureBase
    {
        protected IUnityContainer Container;
        protected const string Name = "name";

        public abstract IUnityContainer GetContainer();


        public virtual void Setup()
        {
            Container = GetContainer();
        }
    }
}
