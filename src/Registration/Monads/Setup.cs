using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Registration.Monads
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
    public interface IService1 { }
    public interface IService2 { }
    public interface IService3 { }

    public class Service : IService, 
                           IService1, 
                           IService2, 
                           IService3
    {
    }
        
    #endregion
}
