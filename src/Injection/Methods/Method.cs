using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Injection.Methods
{
    public abstract partial class SpecificationTests : TestFixtureBase
    {
        [TestMethod]
        public void QualifyingInjectionMethodCanBeConfiguredAndIsCalled()
        {
            // Setup
            Container.RegisterType<LegalInjectionMethod>(
                Execute.Method(nameof(LegalInjectionMethod.InjectMe)));

            // Act
            LegalInjectionMethod result = Container.Resolve<LegalInjectionMethod>();

            // Verify
            Assert.IsTrue(result.WasInjected);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CannotConfigureGenericInjectionMethod()
        {
            // Act
            Container.RegisterType<OpenGenericInjectionMethod>(
                Execute.Method(nameof(OpenGenericInjectionMethod.InjectMe)));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CannotConfigureMethodWithOutParams()
        {
            // Act
            Container.RegisterType<OutParams>(Execute.Method(nameof(OutParams.InjectMe), 12));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CannotConfigureMethodWithRefParams()
        {
            // Act
            Container.RegisterType<RefParams>(
                    Execute.Method(nameof(RefParams.InjectMe), 15));
        }

        [TestMethod]
        public void CanInvokeInheritedMethod()
        {
            // Setup
            Container.RegisterType<InheritedClass>(
                Execute.Method(nameof(InheritedClass.InjectMe)));

            // Act
            var result = Container.Resolve<InheritedClass>();

            // Verify
            Assert.IsTrue(result.WasInjected);
        }


        [TestMethod]
        public void CanInjectMethodPassingVoid()
        {
            // Setup
            Container.RegisterType(typeof(GuineaPig),
                Execute.Method(nameof(GuineaPig.Inject1)));

            // Act
            GuineaPig pig = Container.Resolve<GuineaPig>();

            // Verify
            Assert.AreEqual("void", pig.StringValue);
        }

        [TestMethod]
        public void CanInjectMethodReturningVoid()
        {
            // Setup
            Container.RegisterType(typeof(GuineaPig),
                Execute.Method(nameof(GuineaPig.Inject2), "Hello"));

            // Act
            GuineaPig pig = Container.Resolve<GuineaPig>();

            // Verify
            Assert.AreEqual("Hello", pig.StringValue);
        }

        [TestMethod]
        public void CanInjectMethodReturningInt()
        {
            // Setup
            Container.RegisterType(typeof(GuineaPig),
                Execute.Method(nameof(GuineaPig.Inject3), 17));

            // Act
            GuineaPig pig = Container.Resolve<GuineaPig>();


            // Verify
            Assert.AreEqual(17, pig.IntValue);
        }

        [TestMethod]
        public void CanConfigureMultipleMethods()
        {
            // Setup
            Container.RegisterType<GuineaPig>(
                    Execute.Method(nameof(GuineaPig.Inject3), 37),
                    Execute.Method(nameof(GuineaPig.Inject2), "Hi there"));

            // Act
            GuineaPig pig = Container.Resolve<GuineaPig>();


            // Verify
            Assert.AreEqual(37, pig.IntValue);
            Assert.AreEqual("Hi there", pig.StringValue);
        }

        [TestMethod]
        public void StaticMethodsShouldNotBeInjected()
        {
            // Act
            GuineaPig pig = Container.Resolve<GuineaPig>();

            // Verify
            Assert.IsFalse(GuineaPig.StaticMethodWasCalled);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ContainerThrowsWhenConfiguringStaticMethodForInjection()
        {
            // Verify
            Container.RegisterType<GuineaPig>(
                Execute.Method(nameof(GuineaPig.ShouldntBeCalled)));
        }


        #region Data

        public class GuineaPig
        {
            public int IntValue;
            public string StringValue;
            public static bool StaticMethodWasCalled = false;

            public void Inject1()
            {
                StringValue = "void";
            }

            public void Inject2(string stringValue)
            {
                StringValue = stringValue;
            }

            public int Inject3(int intValue)
            {
                IntValue = intValue;
                return intValue * 2;
            }

            [InjectionMethod]
            public static void ShouldntBeCalled()
            {
                StaticMethodWasCalled = true;
            }
        }

        public class LegalInjectionMethod
        {
            public bool WasInjected = false;

            public void InjectMe()
            {
                WasInjected = true;
            }
        }

        public class OpenGenericInjectionMethod
        {
            public void InjectMe<T>()
            {
            }
        }

        public class OutParams
        {
            public void InjectMe(out int x)
            {
                x = 2;
            }
        }

        public class RefParams
        {
            public void InjectMe(ref int x)
            {
                x *= 2;
            }
        }

        public class InheritedClass : LegalInjectionMethod
        {
        }

        #endregion
    }
}
