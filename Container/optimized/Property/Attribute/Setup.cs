﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unity.Specification.Property.Attribute
{
    public abstract partial class SpecificationTests : TestFixtureBase
    {
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
            Container.RegisterInstance(Name, Name);
        }

        #region Test Data

        public class NoAttributeType
        {
            public object Value { get; set; }
            public int Called = 1;
        }

        public class DependencyAttributeType
        {
            [Dependency]
            public object Value { get; set; }
            public int Called = 2;
        }

        public class NamedDependencyAttributeType
        {
            [Dependency(Name)]
            public string Value { get; set; }
            public int Called = 3;
        }

        public class OptionalDependencyAttributeType
        {
            [OptionalDependency]
            public object Value { get; set; }
            public int Called = 4;
        }
        public class OptionalDependencyWithDefaultType
        {
            [OptionalDependency]
            public int Value { get; set; } = 100;
            public int Called = 100;
        }

        public class OptionalNamedDependencyAttributeType
        {
            [OptionalDependency(Name)]
            public string Value { get; set; }
            public int Called = 5;
        }

        public class OptionalDependencyAttributeMissingType
        {
            [OptionalDependency]
            public IDisposable Value { get; set; }
            public int Called = 6;
        }

        public class OptionalNamedDependencyAttributeMissingType
        {
            [OptionalDependency(Name)]
            public IDisposable Value { get; set; }
            public int Called = 7;
        }

#pragma warning disable 649
        public class DependencyAttributePrivateType
        {
            [Dependency]
            private object Dependency { get; set; }

            public object Value => Dependency;
            public int Called = 8;
        }
#pragma warning restore 649

        public class DependencyAttributeProtectedType
        {
            [Dependency]
            protected object Dependency { get; set; }

            public object Value => Dependency;
            public int Called = 9;
        }

        #endregion
    }
}
