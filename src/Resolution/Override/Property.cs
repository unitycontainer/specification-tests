using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Injection;

namespace Unity.Specification.Resolution.Override
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void Property()
        {
            using (var container = GetContainer())
            {
                // Arrange
                container.RegisterType(typeof(object));

                // Act
                var result = container.Resolve<object>();

                // Assert
                Assert.IsNotNull(result);
            }
        }


        [TestMethod]
        public void Property_CanOverrideValue()
        {
            using (var container = GetContainer())
            {
                // Arrange
                container.RegisterType<ObjectTakingASomething>(
                        new InjectionConstructor(),
                        new InjectionProperty("MySomething"))
                    .RegisterType<ISomething, Something1>()
                    .RegisterType<ISomething, Something2>("other");

                // Act
                var result = container.Resolve<ObjectTakingASomething>(
                    new PropertyOverride("MySomething", 
                        new ResolvedParameter<ISomething>("other"))
                            .OnType<ObjectTakingASomething>());

                // Assert
                Assert.IsNotNull(result);
                Assert.IsNotNull(result.MySomething);
                Assert.IsInstanceOfType(result.MySomething, typeof(Something2));
            }
        }


        [TestMethod]
        public void Property_ValueOverrideForTypeDifferentThanResolvedTypeIsIgnored()
        {
            using (var container = GetContainer())
            {
                // Arrange
                container.RegisterType<ObjectTakingASomething>(
                        new InjectionConstructor(),
                        new InjectionProperty("MySomething"))
                    .RegisterType<ISomething, Something1>()
                    .RegisterType<ISomething, Something2>("other");

                // Act
                var result = container.Resolve<ObjectTakingASomething>(
                    new PropertyOverride("MySomething", 
                        new ResolvedParameter<ISomething>("other"))
                            .OnType<ObjectThatDependsOnSimpleObject>());

                // Assert
                Assert.IsNotNull(result);
                Assert.IsNotNull(result.MySomething);
                Assert.IsInstanceOfType(result.MySomething, typeof(Something1));
            }
        }

    }
}
