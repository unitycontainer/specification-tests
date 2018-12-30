using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Unity.Specification.Container.Registrations
{
    public abstract partial class SpecificationTests
    {

        // http://unity.codeplex.com/WorkItem/View.aspx?WorkItemId=6053
        [TestMethod]
        public void ResolveAllWithChildDoesNotRepeatOverriddenRegistrations()
        {
            Container
                .RegisterInstance("str1", "string1")
                .RegisterInstance("str2", "string2");

            var child = Container.CreateChildContainer()
                .RegisterInstance("str2", "string20")
                .RegisterInstance("str3", "string30");

            var result = child.ResolveAll<string>();

            Assert.IsTrue(new[] { "string1", "string20", "string30" }
                            .SequenceEqual(result.ToArray()));
        }
    }
}
