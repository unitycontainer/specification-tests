using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.TestData
{
    public class ObjectWithStaticAndInstanceProperties
    {
        [Dependency]
        public static object StaticProperty { get; set; }

        [Dependency]
        public object InstanceProperty { get; set; }

        public void Validate()
        {
            Assert.IsNull(StaticProperty);
            Assert.IsNotNull(this.InstanceProperty);
        }
    }
}
