using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Registrations.IsRegistered
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

    public interface ILogger
    {
    }

    public class MockLogger : ILogger
    {
    }

    public class SpecialLogger : ILogger
    {
    }

    #endregion
}

