using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Unity.Injection;
using Unity.Specification.Issues.GitHub;

namespace Unity.Specification.Diagnostic.Issues.GitHub
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        // https://github.com/unitycontainer/abstractions/issues/96
        public override void Abstractions_96()
        {
            // Act
            var ctor = new InjectionConstructor();
            Container.RegisterType<IService, Service>(ctor);
            Container.RegisterType<IService, Service>("name", ctor);
        }

    }
}
