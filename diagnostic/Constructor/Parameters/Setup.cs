using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;

namespace Unity.Specification.Diagnostic.Constructor.Parameters
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

    public interface IDecorator
    { }

    public class BaseDecorator : IDecorator
    {
        private readonly IDecorator decorator;

        public BaseDecorator(IDecorator decorator)
        {
            this.decorator = decorator;
        }
    }

    public class Decorator : IDecorator
    { }

    public class TypeWithConstructorWithRefParameter
    {
        public TypeWithConstructorWithRefParameter(ref string ignored)
        {
        }

        public int Property { get; set; }
    }

    public class TypeWithConstructorWithOutParameter
    {
        public TypeWithConstructorWithOutParameter(out string ignored)
        {
            ignored = null;
        }

        public int Property { get; set; }
    }


    public class Unresolvable
    {
        private Unresolvable() { }

        public Unresolvable(ref long data) { }

        public static Unresolvable Create() => new Unresolvable();
    }

    public class Service
    {
        public Service(Unresolvable data) => Ctor = 2;

        public Service(Unresolvable unr, RegistryKey data) => Ctor = 3;

        public int Ctor { get; }
    }

    #endregion
}
