using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Property.Overrides
{
    public abstract partial class SpecificationTests : TestFixtureBase
    {

        [TestInitialize]
        public override void Setup()
        {
            base.Setup();

            Container.RegisterType<ObjectWithProperty>(
                    Invoke.Constructor(),
                    Resolve.Property(nameof(ObjectWithProperty.MyProperty)))
                .RegisterType<ISomething, Something1>()
                .RegisterType<ISomething, Something2>(Name)
                .RegisterInstance(Name);
        }

        public class ObjectWithThreeProperties
        {
            [Dependency]
            public string Name { get; set; }

            public object Property { get; set; }

            [Dependency]
            public IUnityContainer Container { get; set; }
        }

        public class SimpleTestObject
        {
            public SimpleTestObject()
            {
            }

            [InjectionConstructor]
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

        public class ObjectWithProperty
        {
            public ISomething MyProperty { get; set; }

            public ObjectWithProperty()
            {
            }

            public ObjectWithProperty(ISomething property)
            {
                MyProperty = property;
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
