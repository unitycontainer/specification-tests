// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;

namespace Unity.Specification.TestData
{
    public class DisposableObject : IDisposable
    {
        private bool _wasDisposed;

        public bool WasDisposed
        {
            get => _wasDisposed;
            set => _wasDisposed = value;
        }

        public void Dispose()
        {
            _wasDisposed = true;
        }
    }
}
