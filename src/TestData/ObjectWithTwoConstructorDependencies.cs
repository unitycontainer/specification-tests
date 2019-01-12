

using System;

namespace Unity.Specification.TestData
{
    // A class that contains another one which has another
    // constructor @delegate. Used to validate recursive
    // buildup of constructor dependencies.
    public class ObjectWithTwoConstructorDependencies
    {
        private readonly ObjectWithOneDependency _oneDep;

        public ObjectWithTwoConstructorDependencies(ObjectWithOneDependency oneDep)
        {
            _oneDep = oneDep;
        }

        public ObjectWithOneDependency OneDep => _oneDep;

        public void Validate()
        {
            if (null == _oneDep) throw new ArgumentNullException(nameof(_oneDep));
            _oneDep.Validate();
        }
    }
}
