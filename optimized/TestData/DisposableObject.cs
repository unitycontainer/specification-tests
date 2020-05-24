

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
