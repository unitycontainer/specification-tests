using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Constructor.Injection
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void InjectDefaultCtor()
        {
            // Arrange
            #region inject_default_ctor_arrange

            Container.RegisterType<Service>(Invoke.Constructor());

            #endregion

            // Act
            #region inject_default_ctor_act

            var instance = Container.Resolve<Service>();

            // 1 == instance.Ctor

            #endregion

            // Validate
            Assert.AreEqual(1, instance.Ctor);
        }

        [TestMethod]
        public void InjectDefaultCtorClosedGeneric()
        {
            // Arrange
            #region inject_default_ctor_closed_generic_arrange

            Container.RegisterType<Service<object>>(Invoke.Constructor());

            #endregion

            // Act
            #region inject_default_ctor_closed_generic_act

            var instance = Container.Resolve<Service<object>>();

            // 1 == instance.Ctor

            #endregion

            // Validate
            Assert.AreEqual(1, instance.Ctor);
        }


        [TestMethod]
        public void InjectDefaultCtorOpenGeneric()
        {
            // Arrange
            #region inject_default_ctor_open_generic_arrange

            Container.RegisterType(typeof(Service<>), Invoke.Constructor());

            #endregion

            // Act
            var instance = Container.Resolve<Service<object>>();

            // Validate
            Assert.AreEqual(1, instance.Ctor);
        }
    }
}
