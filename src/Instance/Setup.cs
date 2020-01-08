using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Instance
{
    public abstract partial class SpecificationTests : TestFixtureBase
    {
        [TestInitialize]
        public override void Setup()
        {
            Container = GetContainer();
        }
    }

    #region Test Data

    public interface IService
    {
    }

    public class Service : IService 
    {
    }

    public class Unresolvable : IService
    {
        private Unresolvable() { }

        public static Unresolvable Create() => new Unresolvable();
    }


    #endregion
}
