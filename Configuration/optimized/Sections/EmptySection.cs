using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Unity.Specification.Configuration.Sections
{
    [TestClass]
    public abstract partial class SpecificationTests
    {

        [DataTestMethod, DynamicData(nameof(ConfigurationFixture.Variants), typeof(ConfigurationFixture), DynamicDataSourceType.Method)]
        public void ValidateCreatedXDoc(object[] variants)
        {
        }


        [DataTestMethod, DynamicData(nameof(ConfigurationFixture.Variants), typeof(ConfigurationFixture), DynamicDataSourceType.Method)]
        public void ValidateLoadContent(object[] variants)
        {
        }

    }
}
