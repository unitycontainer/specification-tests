// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using Unity.Attributes;

namespace Unity.Specification.TestData
{
    public class ObjectWithInjectionConstructor
    {
        public ObjectWithInjectionConstructor(object constructorDependency)
        {
            this.ConstructorDependency = constructorDependency;
        }

        [InjectionConstructor]
        public ObjectWithInjectionConstructor(string s)
        {
            ConstructorDependency = s;
        }

        public object ConstructorDependency { get; }
    }
}
