using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Unity.Specification.Diagnostic.Constructor.Types
{
    public abstract partial class SpecificationTests
    {

        // https://unity.codeplex.com/workitem/11899
        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void ResolveDelegateThrowsExplicitException()
        {
            var func = Container.Resolve<Func<string, object>>();
        }

        [TestMethod]
        public void CanBuildupObjectWithExplicitInterface()
        {
            // Setup
            Container.RegisterType<ILogger, MockLogger>();

            // Act
            var o = new ObjectWithExplicitInterface();
            Container.BuildUp<ISomeCommonProperties>(o);

            // Verify
            o.ValidateInterface();
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void WhenResolvingAnOpenGenericType()
        {
            Container.Resolve(typeof(List<>));
        }

        [TestMethod]
        public void CanBuildupObjectWithExplicitInterfaceUsingNongenericMethod()
        {
            // Setup
            Container.RegisterType<ILogger, MockLogger>();

            // Act
            var o = new ObjectWithExplicitInterface();
            Container.BuildUp(typeof(ISomeCommonProperties), o);

            // Verify
            o.ValidateInterface();

        }

        [TestMethod]
        public void BuildAbstractBaseAndChildObject1()
        {
            // Setup
            ConcreteChild objChild = new ConcreteChild();
            Assert.IsNotNull(objChild);
            Assert.IsNull(objChild.AbsBaseProp);
            Assert.IsNull(objChild.ChildProp);

            // Act
            Container.BuildUp(typeof(AbstractBase), objChild);

            // Verify
            Assert.IsNotNull(objChild.AbsBaseProp);
            Assert.IsNull(objChild.ChildProp);
        }


        [TestMethod]
        public void BuildBaseAndChildObject4()
        {
            // Setup
            BaseStub1 objBase = new BaseStub1();
            Assert.IsNotNull(objBase);
            Assert.IsNull(objBase.InterfaceProp);

            // Act
            Container.BuildUp(typeof(Interface1), objBase);

            // Verify
            Assert.IsNotNull(objBase.InterfaceProp);
        }
    }
}
