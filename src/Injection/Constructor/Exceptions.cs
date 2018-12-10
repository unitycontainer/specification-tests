using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unity.Specification.Injection.Constructor
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void IncorrectType()
        {
            Container.RegisterType<TypeWithAmbiguousCtors>(Execute.Constructor(typeof(int)));
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void IncorrectValue()
        {
            Container.RegisterType<TypeWithAmbiguousCtors>(Execute.Constructor(0));
        }
    }
}
