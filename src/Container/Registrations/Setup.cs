using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Container.Registrations
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

    public interface ISpecialLogger
    {
    }

    public class MockLogger : ILogger
    {
    }

    public class SpecialLogger : ILogger, ISpecialLogger
    {
    }

    public class MockLoggerWithCtor : ILogger
    {
        public MockLoggerWithCtor(string _)
        {

        }
    }

    public class SpecialLoggerWithCtor : ILogger, ISpecialLogger
    {
        public SpecialLoggerWithCtor(string _)
        {

        }
    }

    #endregion
}
