using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Unity.Specification.Resolution.Array
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void ContainerCanResolveListOfT()
        {

            Container.RegisterType(typeof(List<>), Invoke.Constructor());

            var result = Container.Resolve<List<EmptyClass>>();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ContainerReturnsEmptyArrayIfNoObjectsRegistered()
        {
            var results = Container.Resolve<object[]>();

            Assert.IsNotNull(results);
            Assert.AreEqual(0, results.Length);
        }

        [TestMethod]
        public void ResolveAllReturnsRegisteredObjects()
        {
            object o1 = new object();
            object o2 = new object();

            Container.RegisterInstance("o1", o1)
                     .RegisterInstance("o2", o2);

            var results = Container.Resolve<object[]>();

            Assert.IsNotNull(results);
            Assert.AreEqual(2, results.Length);
            Assert.AreSame(o1, results[0]);
            Assert.AreSame(o2, results[1]);
        }


        [TestMethod]
        public void ResolveAllReturnsRegisteredObjectsForBaseClass()
        {
            IService o1 = new Service();
            IService o2 = new OtherService();

            Container.RegisterInstance("o1", o1)
                     .RegisterInstance("o2", o2);

            var results = Container.Resolve<IService[]>();

            Assert.IsNotNull(results);
            Assert.AreEqual(2, results.Length);
            Assert.AreSame(o1, results[0]);
            Assert.AreSame(o2, results[1]);
        }

    }
}
