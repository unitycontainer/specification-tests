using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Unity.Specification.Resolution.Enumerable
{
    public abstract partial class SpecificationTests
    {

        [TestMethod]
        public void HandlesGenerics()
        {
            // Arrange
            Container.RegisterInstance(typeof(IService), new OtherService());
            Container.RegisterInstance(typeof(IFoo<IService>), "service", new Foo<IService>(new Service()));
            Container.RegisterType(typeof(IFoo<>), typeof(Foo<>), "foo");

            // Act
            var array = Container.Resolve<IEnumerable<IFoo<IService>>>()
                                 .ToArray();

            // Assert
            Assert.IsNotNull(array);

            Assert.AreEqual(2, array.Length);
            Assert.IsNotNull(array.FirstOrDefault(e => e.Value is Service));
            Assert.IsNotNull(array.FirstOrDefault(e => e.Value is OtherService));
        }

        [TestMethod]
        public void HandlesConstraintViolation()
        {
            // Arrange
            Container.RegisterType(typeof(IService), typeof(Service));
            Container.RegisterType(typeof(IConstrained<>), typeof(Constrained<>), "foo");

            // Act
            var array = Container.Resolve< IEnumerable<IConstrained<IService>>>()
                                 .ToArray();
            // Assert
            Assert.IsNotNull(array);

            Assert.AreEqual(0, array.Length);
        }
    }
}
