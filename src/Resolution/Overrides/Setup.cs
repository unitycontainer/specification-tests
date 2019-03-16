using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Resolution.Overrides
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
                .RegisterType<IService, Service1>()
                .RegisterType<IService, Service2>(Name)
                .RegisterInstance(Name);
        }

    }

    #region Test Data

    public class TestType
    {
        private readonly string _dependency;

        public TestType() { }

        public TestType(string dependency)
        {
            _dependency = dependency;
        }

        public string DependencyField;
        public string DependencyProperty { get; set; }

        public override string ToString() => _dependency;
    }

    public interface IFoo { }

    public class Foo : IFoo
    {
        public object Fred { get; }

        public object George { get; }

        public Foo([OptionalDependency("Fred")]   IService x,
                    [OptionalDependency("George")] IService y)
        {
            Fred = x;
            George = y;
        }
    }

    public class DependsOnIFoo
    {
        public DependsOnIFoo(IFoo foo)
        {
            Foo = foo;
        }

        public IFoo Foo { get; }
    }
    public class DependsOnIFooName
    {
        public DependsOnIFooName([Dependency("name")]IFoo foo)
        {
            Foo = foo;
        }

        public IFoo Foo { get; }
    }

    public class Foo1 : IFoo { }

    public class Foo2 : IFoo { }

    public class Foo3 : IFoo { }

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

    public interface IService { }

    public class Service1 : IService { }

    public class Service2 : IService { }

    public class ObjectWithProperty
    {
        public IService MyProperty { get; set; }

        public ObjectWithProperty()
        {
        }

        public ObjectWithProperty(IService property)
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

    public class ObjectTakingASomething
    {
        public IService MySomething { get; set; }
        public ObjectTakingASomething()
        {
        }

        public ObjectTakingASomething(IService something)
        {
            MySomething = something;
        }
    }


    #endregion
}
