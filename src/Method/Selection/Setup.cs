using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Method.Selection
{
    public abstract partial class SpecificationTests : TestFixtureBase
    {
        #region Setup

        [TestInitialize]
        public override void Setup()
        {
            base.Setup();

            Container.RegisterInstance(Name);
            Container.RegisterType(typeof(IInjectedMethodTest), typeof(InjectedMethodTest));
            Container.RegisterType(typeof(IGenericInjectedMethodTest<>), typeof(GenericInjectedMethodTest<>));
        }

        #endregion


        #region Test Data

        public interface IInjectedMethodTest
        {
            void Execute(string data);

            string Executed { get; }
        }


        public class InjectedMethodTest : IInjectedMethodTest
        {
            public bool ExecutedVoid { get; private set; }

            [InjectionMethod]
            public void ExecuteVoid() => ExecutedVoid = true;

            [InjectionMethod]
            public void Execute(string data)
            {
                Executed = data;
            }

            public string Executed { get; private set; }
        }




        public interface IGenericInjectedMethodTest<T>
        {
            void ExecuteGeneric(T data);

            T ExecutedGeneric { get; }
        }

        public class GenericInjectedMethodTest<T> : IGenericInjectedMethodTest<T>
        {
            public bool ExecutedVoid { get; private set; }

            [InjectionMethod]
            public void ExecuteVoid() => ExecutedVoid = true;


            [InjectionMethod]
            public void ExecuteGeneric(T data)
            {
                ExecutedGeneric = data;
            }

            public T ExecutedGeneric { get; private set; }
        }

        #endregion
    }
}
