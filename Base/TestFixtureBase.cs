namespace Unity.Specification
{
    public abstract class TestFixtureBase
    {
        protected IUnityContainer Container;
        public const string Name = "name";
        public const string Legacy = "legacy";


        public abstract IUnityContainer GetContainer();


        public virtual void Setup()
        {
            Container = GetContainer();
        }
    }
}
