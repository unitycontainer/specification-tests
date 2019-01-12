using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Field.Injection
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void Attributes()
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
        public void ValueOverAttribute()
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
        public void ResolveOverAttribute()
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
        public void ResolverOverAttribute()
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
