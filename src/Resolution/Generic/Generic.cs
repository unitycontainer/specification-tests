using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Unity.Specification.TestData;

namespace Unity.Specification.Resolution.Generic
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void Resolve_ListOfT()
        {
            Container.RegisterType(typeof(List<>), new InjectionConstructor());
            Assert.IsNotNull(Container.Resolve<List<Service>>());
        }
    }
}
