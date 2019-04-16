using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Unity.Specification.Resolution.Generic
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void Mapping()
        {
            // Arrange
            Container.RegisterType(typeof(IFoo<>), typeof(Foo<>));

            // Act 
            var instance = Container.Resolve<IFoo<IService>>();

            // Validate
            Assert.IsNotNull(instance);
        }

        [TestMethod]
        public void Named_null_null()
        {
            // Arrange
            Container.RegisterType(typeof(IFoo<>), typeof(Foo<>), Name);

            // Act 
            var instance = Container.Resolve<IFoo<IService>>(Name);

            // Validate
            Assert.IsNotNull(instance);
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void Named_null_Name_null()
        {
            // Arrange
            Container.RegisterType(typeof(IFoo<>), typeof(Foo<>));
            Container.RegisterType<IOtherService, OtherService>(Name);

            // Act 
            Container.Resolve<IFoo<IOtherService>>();
        }

        [TestMethod]
        public void Named_null_Name_name()
        {
            // Arrange
            Container.RegisterType(typeof(IFoo<>), typeof(Foo<>));
            Container.RegisterType<IOtherService, OtherService>(Name);

            // Act 
            var instance = Container.Resolve<IFoo<IService>>(Name);

            // Validate
            Assert.IsNotNull(instance);
        }

        [TestMethod]
        public void Named_Name_Name_Name()
        {
            // Arrange
            Container.RegisterType(typeof(IFoo<>), typeof(Foo<>), Name);
            Container.RegisterType<IOtherService, OtherService>(Name);

            // Act 
            var instance = Container.Resolve<IFoo<IService>>(Name);

            // Validate
            Assert.IsNotNull(instance);
        }

    }
}
