using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Unity.Lifetime;

namespace Unity.Specification.Lifetime
{
    public abstract partial class SpecificationTests
    {
        #region Legacy Managers

        public class LegacyHierarchical : SynchronizedLifetimeManager,
                                          IFactoryLifetimeManager,
                                          ITypeLifetimeManager
        {
            #region Fields

            private readonly IDictionary<ILifetimeContainer, object> _values =
                new ConcurrentDictionary<ILifetimeContainer, object>();

            #endregion


            #region Overrides

            /// <inheritdoc/>
            protected override object SynchronizedGetValue(ILifetimeContainer container = null)
            {
                return _values.TryGetValue(container ?? throw new ArgumentNullException(nameof(container)),
                                           out object value) ? value : NoValue;
            }

            /// <inheritdoc/>
            protected override void SynchronizedSetValue(object newValue, ILifetimeContainer container = null)
            {
                _values[container ?? throw new ArgumentNullException(nameof(container))] = newValue;
                container.Add(new DisposableAction(() => RemoveValue(container)));
            }


            /// <inheritdoc/>
            public override void RemoveValue(ILifetimeContainer container = null)
            {
                if (null == container) throw new ArgumentNullException(nameof(container));
                if (!_values.TryGetValue(container, out object value)) return;

                _values.Remove(container);
                if (value is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }

            /// <inheritdoc/>
            protected override LifetimeManager OnCreateLifetimeManager()
            {
                return new HierarchicalLifetimeManager();
            }

            /// <summary>
            /// This method provides human readable representation of the lifetime
            /// </summary>
            /// <returns>Name of the lifetime</returns>
            public override string ToString() => "Lifetime:Hierarchical";

            #endregion


            #region IDisposable

            /// <inheritdoc/>
            protected override void Dispose(bool disposing)
            {
                try
                {
                    if (0 == _values.Count) return;

                    foreach (var disposable in _values.Values
                                                      .OfType<IDisposable>()
                                                      .ToArray())
                    {
                        disposable.Dispose();
                    }
                    _values.Clear();
                }
                finally
                {
                    base.Dispose(disposing);
                }
            }

            #endregion


            #region Nested Types

            private class DisposableAction : IDisposable
            {
                private readonly Action _action;

                public DisposableAction(Action action)
                {
                    _action = action ?? throw new ArgumentNullException(nameof(action));
                }

                public void Dispose()
                {
                    _action();
                }
            }

            #endregion
        }


        public class LegacyPerContainer : SynchronizedLifetimeManager,
                                          IInstanceLifetimeManager,
                                          IFactoryLifetimeManager,
                                          ITypeLifetimeManager
        {
            #region Fields

            /// <summary>
            /// An instance of the object this manager is associated with.
            /// </summary>
            /// <value>This field holds a strong reference to the associated object.</value>
            protected object Value = NoValue;

            private Func<ILifetimeContainer, object> _currentGetValue;
            private Action<object, ILifetimeContainer> _currentSetValue;

            #endregion


            #region Constructor

            public LegacyPerContainer()
            {
                _currentGetValue = base.GetValue;
                _currentSetValue = base.SetValue;
            }

            #endregion


            #region SynchronizedLifetimeManager

            /// <inheritdoc/>
            public override object GetValue(ILifetimeContainer container = null)
            {
                return _currentGetValue(container);
            }

            /// <inheritdoc/>
            public override void SetValue(object newValue, ILifetimeContainer container = null)
            {
                _currentSetValue(newValue, container);
                _currentSetValue = (o, c) => throw new InvalidOperationException("InjectionParameterValue of ContainerControlledLifetimeManager can only be set once");
                _currentGetValue = SynchronizedGetValue;
            }

            /// <inheritdoc/>
            protected override object SynchronizedGetValue(ILifetimeContainer container = null)
            {
                return Value;
            }

            /// <inheritdoc/>
            protected override void SynchronizedSetValue(object newValue, ILifetimeContainer container = null)
            {
                Value = newValue;
            }

            /// <inheritdoc/>
            public override void RemoveValue(ILifetimeContainer container = null)
            {
                Dispose();
            }

            #endregion


            #region IFactoryLifetimeManager

            /// <inheritdoc/>
            protected override LifetimeManager OnCreateLifetimeManager()
            {
                throw new NotImplementedException();
            }

            #endregion


            #region IDisposable

            /// <inheritdoc/>
            protected override void Dispose(bool disposing)
            {
                try
                {
                    if (NoValue == Value) return;
                    if (Value is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }
                    Value = NoValue;
                }
                finally
                {
                    base.Dispose(disposing);
                }
            }

            #endregion


            #region Overrides

            /// <summary>
            /// This method provides human readable representation of the lifetime
            /// </summary>
            /// <returns>Name of the lifetime</returns>
            public override string ToString() => "Lifetime:LegacyPerContainer";

            #endregion
        }

        #endregion
    }
}
