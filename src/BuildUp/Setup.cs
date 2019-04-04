using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.BuildUp
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

    public interface IFooInterface
    {
        [Dependency]
        object InterfaceProp
        {
            get;
            set;
        }
    }

    public interface IFooInterface2
    {
        object InterfaceProp
        {
            get;
            set;
        }
    }

    public class BarClass : IFooInterface
    {
        public object InterfaceProp { get; set; }
    }

    public class BarClass2 : IFooInterface2
    {
        [Dependency]
        public object InterfaceProp { get; set; }
    }

    #endregion
}
