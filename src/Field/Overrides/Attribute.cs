using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Field.Overrides
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void BaseLine()
        {
            // Act
            var result = Container.Resolve<ObjectWithAttributes>();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Dependency);
            Assert.AreEqual(result.Dependency, Name1);
            Assert.IsNull(result.Optional);
        }

        [TestMethod]
        public void InjectorOverAttribute()
        {
            // Setup
            Container.RegisterType<ObjectWithAttributes>(
                Resolve.Field(nameof(ObjectWithAttributes.Dependency)));

            // Act
            var result = Container.Resolve<ObjectWithAttributes>();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Dependency);
            Assert.AreEqual(result.Dependency, Name);
            Assert.IsNull(result.Optional);
        }

        [TestMethod]
        public void InjectedValueOverAttribute()
        {
            // Setup
            Container.RegisterType<ObjectWithAttributes>(
                Inject.Field(nameof(ObjectWithAttributes.Dependency), Name2));

            // Act
            var result = Container.Resolve<ObjectWithAttributes>();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Dependency);
            Assert.AreEqual(result.Dependency, Name2);
            Assert.IsNull(result.Optional);
        }


        [TestMethod]
        public void InjectedResolverOverAttribute()
        {
            // Setup
            Container.RegisterType<ObjectWithAttributes>(
                Inject.Field(nameof(ObjectWithAttributes.Dependency), Resolve.Parameter(Name1)));

            // Act
            var result = Container.Resolve<ObjectWithAttributes>();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Dependency);
            Assert.AreEqual(result.Dependency, Name1);
            Assert.IsNull(result.Optional);
        }
    }
}
