using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Parameter.Resolved
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public virtual void ProvidingConcreteTypeForGenericFails()
        {
            Container.RegisterType(typeof(GenericService<,,>), 
               Invoke.Method("Method", Resolve.Parameter(typeof(string))));

            // Act
            var result = Container.Resolve<GenericService<object, string, int>>();

            // Verify
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Called, 2);
            Assert.IsInstanceOfType(result.Value, typeof(string));
        }

        [TestMethod]
        public void GenericParameterT1()
        {
            // Arrange
            Container.RegisterType(typeof(GenericService<,,>),
               Invoke.Method("Method", Resolve.Generic("T1")));

            // Act
            var result = Container.Resolve<GenericService<object, string, int>>();

            // Verify
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Called, 1);
            Assert.IsInstanceOfType(result.Value, typeof(object));
        }

        [TestMethod]
        public void GenericParameterT2()
        {
            // Arrange
            Container.RegisterType(typeof(GenericService<,,>),
               Invoke.Method("Method", Resolve.Generic("T2")));

            // Act
            var result = Container.Resolve<GenericService<object, string, int>>();

            // Verify
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Called, 2);
            Assert.IsInstanceOfType(result.Value, typeof(string));
            Assert.AreEqual(result.Value, "other");
        }

        [TestMethod]
        public void GenericParameterT3()
        {
            // Arrange
            Container.RegisterType(typeof(GenericService<,,>),
               Invoke.Method("Method", Resolve.Generic("T3")));

            // Act
            var result = Container.Resolve<GenericService<object, string, int>>();

            // Verify
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Called, 3);
            Assert.AreEqual(result.Value, 10);
        }

        [TestMethod]
        public void GenericParameterT1WithName()
        {
            // Arrange
            Container.RegisterType(typeof(GenericService<,,>),
               Invoke.Method("Method", Resolve.Generic("T1", "1")));

            // Act
            var result = Container.Resolve<GenericService<object, string, int>>();

            // Verify
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Called, 1);
            Assert.IsInstanceOfType(result.Value, typeof(object));
        }

        [TestMethod]
        public void GenericParameterT2WithName()
        {
            // Arrange
            Container.RegisterType(typeof(GenericService<,,>),
               Invoke.Method("Method", Resolve.Generic("T2", "1")));

            // Act
            var result = Container.Resolve<GenericService<object, string, int>>();

            // Verify
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Called, 2);
            Assert.IsInstanceOfType(result.Value, typeof(string));
            Assert.AreEqual(result.Value, "1");
        }

        [TestMethod]
        public void GenericParameterT3WithName()
        {
            // Arrange
            Container.RegisterType(typeof(GenericService<,,>),
               Invoke.Method("Method", Resolve.Generic("T3", "1")));

            // Act
            var result = Container.Resolve<GenericService<object, string, int>>();

            // Verify
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Called, 3);
            Assert.AreEqual(result.Value, 1);
        }
    }
}
