// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Attributes;

namespace Unity.Specification.TestData
{
    public interface ISomeCommonProperties
    {
        [Dependency]
        ILogger Logger { get; set; }

        [Dependency]
        object SyncObject { get; set; }
    }

    public class ObjectWithExplicitInterface : ISomeCommonProperties
    {
        private ILogger _logger;
        private object _syncObject;

        [Dependency]
        public object SomethingElse { get; set; }

        [Dependency]
        ILogger ISomeCommonProperties.Logger
        {
            get => _logger;
            set => _logger = value;
        }

        [Dependency]
        object ISomeCommonProperties.SyncObject
        {
            get => _syncObject;
            set => _syncObject = value;
        }

        public void ValidateInterface()
        {
            Assert.IsNotNull(_logger);
            Assert.IsNotNull(_syncObject);
        }
    }
}
