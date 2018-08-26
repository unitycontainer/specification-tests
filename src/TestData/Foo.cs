namespace Unity.Specification.TestData
{
    public interface IFoo<TEntity>
    {
        TEntity Value { get; }
    }

    public interface IFoo { }
    public interface IFoo1 { }
    public interface IFoo2 { }

    public class Foo<TEntity> : IFoo<TEntity>
    {
        public Foo()
        {
        }

        public Foo(TEntity value)
        {
            Value = value;
        }

        public TEntity Value { get; }
    }

    public class Foo : IFoo, IFoo1, IFoo2
    {
    }
}
