using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Diagnostic.Registration.Instance
{
    public abstract partial class SpecificationTests : TestFixtureBase
    {
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
        }
    }

    #region Test Data

    public interface IService { }

    public class Service : IService { }

    #endregion
}
