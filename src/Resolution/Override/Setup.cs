using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;

namespace Unity.Specification.Resolution.Override
{
    public abstract partial class SpecificationTests : TestFixtureBase
    {
        [TestInitialize]
        public override void Setup()
        {
            Container = GetContainer();

            Container.RegisterType<ObjectTakingASomething>(
                    new InjectionConstructor(),
                    new InjectionProperty("MySomething"))
                .RegisterType<ISomething, Something1>()
                .RegisterType<ISomething, Something2>("other");
        }



        public class ObjectWithTwoProperties
        {
            private object obj1;
            private object obj2;

            [Dependency]
            public object Obj1
            {
                get { return obj1; }
                set { obj1 = value; }
            }

            [Dependency]
            public object Obj2
            {
                get { return obj2; }
                set { obj2 = value; }
            }

            public void Validate()
            {
                Assert.IsNotNull(obj1);
                Assert.IsNotNull(obj2);
                Assert.AreNotSame(obj1, obj2);
            }
        }

        public class SimpleTestObject
        {
            public SimpleTestObject()
            {
            }

            public SimpleTestObject(int x)
            {
                X = x;
            }

            public int X { get; private set; }
        }

        public class ObjectThatDependsOnSimpleObject
        {
            public SimpleTestObject TestObject { get; set; }

            public ObjectThatDependsOnSimpleObject(SimpleTestObject testObject)
            {
                TestObject = testObject;
            }

            public SimpleTestObject OtherTestObject { get; set; }
        }

        public interface ISomething { }
        public class Something1 : ISomething { }
        public class Something2 : ISomething { }

        public class ObjectTakingASomething
        {
            public ISomething MySomething { get; set; }
            public ObjectTakingASomething()
            {
            }

            public ObjectTakingASomething(ISomething something)
            {
                MySomething = something;
            }
        }

        public class Outer
        {
            public Outer(Inner inner, int logLevel)
            {
                this.Inner = inner;
                this.LogLevel = logLevel;
            }

            public int LogLevel { get; private set; }
            public Inner Inner { get; private set; }
        }

        public class Inner
        {
            public Inner(int logLevel, string label)
            {
                this.LogLevel = logLevel;
            }

            public int LogLevel { get; private set; }
        }

    }
}
