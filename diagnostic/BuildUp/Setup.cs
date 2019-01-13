using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Diagnostic.BuildUp
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

    public class ChildStub1 : BaseStub1
    {
        private object childProp;

        [Dependency]
        public object ChildProp
        {
            get { return this.childProp; }
            set { this.childProp = value; }
        }
    }

    public class BuildUnmatchedObject2_PropertyDependencyClassStub1
    {
        private object myFirstObj;

        [Dependency]
        public object MyFirstObj
        {
            get { return myFirstObj; }
            set { myFirstObj = value; }
        }
    }

    public class BuildUnmatchedObject2__PropertyDependencyClassStub2
    {
        private object myFirstObj;
        private object mySecondObj;

        [Dependency]
        public object MyFirstObj
        {
            get { return myFirstObj; }
            set { myFirstObj = value; }
        }

        [Dependency]
        public object MySecondObj
        {
            get { return mySecondObj; }
            set { mySecondObj = value; }
        }
    }

    #endregion
}
