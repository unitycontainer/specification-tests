using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Diagnostic.Constructor.Types
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

    public interface ITestClass
    { }

    public class TestClass : ITestClass
    {
        public TestClass()
        { }

        [InjectionConstructor]
        public TestClass(TestClass _) //1
        {
        }
    }

    public interface ILogger
    {
    }
    public class MockLogger : ILogger
    {
    }

    public interface ISomeCommonProperties
    {
        [Dependency]
        ILogger Logger { get; set; }

        [Dependency]
        object SyncObject { get; set; }
    }


    public class ObjectWithExplicitInterface : ISomeCommonProperties
    {
        private ILogger logger;
        private object syncObject;

        private object somethingElse;

        [Dependency]
        public object SomethingElse
        {
            get { return somethingElse; }
            set { somethingElse = value; }
        }

        [Dependency]
        ILogger ISomeCommonProperties.Logger
        {
            get { return logger; }
            set { logger = value; }
        }

        [Dependency]
        object ISomeCommonProperties.SyncObject
        {
            get { return syncObject; }
            set { syncObject = value; }
        }

        public void ValidateInterface()
        {
            Assert.IsNotNull(logger);
            Assert.IsNotNull(syncObject);
        }
    }

    public abstract class AbstractBase
    {
        private object baseProp;

        [Dependency]
        public object AbsBaseProp
        {
            get { return baseProp; }
            set { baseProp = value; }
        }

        public abstract void AbstractMethod();
    }

    public class ConcreteChild : AbstractBase
    {
        public override void AbstractMethod()
        {
        }

        [Dependency]
        public object ChildProp { get; set; }
    }

    public interface Interface1
    {
        [Dependency]
        object InterfaceProp
        {
            get;
            set;
        }
    }

    public class BaseStub1 : Interface1
    {
        private object baseProp;
        private object interfaceProp;

        [Dependency]
        public object BaseProp
        {
            get { return this.baseProp; }
            set { this.baseProp = value; }
        }

        public object InterfaceProp
        {
            get { return this.interfaceProp; }
            set { this.interfaceProp = value; }
        }
    }

    #endregion
}
