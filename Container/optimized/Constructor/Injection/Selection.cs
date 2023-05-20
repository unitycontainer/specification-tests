using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Unity.Specification.Constructor.Injection
{
    public abstract partial class SpecificationTests 
    {
        public static IEnumerable<object[]> DefaultConstructorTestData
        {

            // Format:                     |TypeFrom,  |TypeTo,                                        |Name,      |TypeToResolve
            get
            {
                yield return new object[] { null, typeof(object), null, typeof(object) };
                yield return new object[] { null, typeof(TestClass), null, typeof(TestClass) };
                yield return new object[] { null, typeof(GenericTestClass<int, string, object>), null, typeof(GenericTestClass<int, string, object>) };
                yield return new object[] { null, typeof(object), "0", typeof(object) };
                yield return new object[] { null, typeof(TestClass), "1", typeof(TestClass) };
                yield return new object[] { null, typeof(GenericTestClass<,,>), "2", typeof(GenericTestClass<int, string, object>) };
                yield return new object[] { null, typeof(GenericTestClass<int, string, object>), "3", typeof(GenericTestClass<int, string, object>) };
                yield return new object[] { null, typeof(GenericTestClass<,,>), "4", typeof(GenericTestClass<object, string, object>) };
            }
        }

        public static IEnumerable<object[]> DefaultConstructorTestDataFailed
        {
            // Format:                      |TypeTo/TypeToResolve,        |Name
            get
            {
                yield return new object[] { typeof(GenericTestClass<,,>), null };
                yield return new object[] { typeof(GenericTestClass<,,>), null };
                yield return new object[] { typeof(GenericTestClass<,,>), "2" };
                yield return new object[] { typeof(GenericTestClass<,,>), "4" };
            }
        }

        public static IEnumerable<object[]> ConstructorSelectionTestData
        {

            // Format: Type typeFrom, Type typeTo, string name, Type typeToResolve, object[] parameters, Func<object, bool> validator
            get
            {
                // SelectAndResolveByValue
                yield return new object[]
                {
                    "Constructor with 3 strings",               //  Name, 
                    null,                                       //  Type typeFrom, 
                    typeof(TypeWithMultipleCtors),              //  Type typeTo, 
                    typeof(TypeWithMultipleCtors),              //  Type typeToResolve, 
                    new object[] {                              //  object[] parameters
                        Resolve.Parameter(typeof(string)),      // By Type of parameter 
                        string.Empty,                           // By value's type
                        string.Empty },                         // By value's type
                    new Func<object, bool>(r =>                 //  Validator
                        TypeWithMultipleCtors.Four == ((TypeWithMultipleCtors)r).Signature)
                };


                // ResolveNamedTypeArgument
                yield return new object[]
                {
                    "Constructor with 3 Type parameters",       //  Name, 
                    null,                                       //  Type typeFrom, 
                    typeof(TypeWithMultipleCtors),              //  Type typeTo, 
                    typeof(TypeWithMultipleCtors),              //  Type typeToResolve, 
                    new object[] {                              //  object[] parameters
                        typeof(string),                         // By Type of parameter
                        typeof(string),                         // By Type of parameter
                        typeof(bool)},                          // By Type of parameter
                    new Func<object, bool>(r =>                 //  Validator
                        TypeWithMultipleCtors.Three == ((TypeWithMultipleCtors)r).Signature)
                };


                // TypeArgument
                yield return new object[]
                {
                    "Constructor with 3 parameters by type",       //  Name, 
                    null,                                       //  Type typeFrom, 
                    typeof(TypeWithMultipleCtors),              //  Type typeTo, 
                    typeof(TypeWithMultipleCtors),              //  Type typeToResolve, 
                    new object[] {                              //  object[] parameters
                        typeof(string),                         // By Type of parameter
                        typeof(string),                         // By Type of parameter
                        typeof(IUnityContainer)},               // By Type of parameter
                    new Func<object, bool>(r =>                 //  Validator
                        TypeWithMultipleCtors.Five == ((TypeWithMultipleCtors)r).Signature)
                };

                // ResolveNamedTypeArgument
                yield return new object[]
                {
                    "Constructor with 2 By requested Type",     //  Name, 
                    null,                                       //  Type typeFrom, 
                    typeof(TypeWithMultipleCtors),              //  Type typeTo, 
                    typeof(TypeWithMultipleCtors),              //  Type typeToResolve, 
                    new object[] {                              //  object[] parameters
                        typeof(string),                         // By requested Type of parameter
                        typeof(IUnityContainer)},               // By requested Type of parameter
                    new Func<object, bool>(r =>                 //  Validator
                        TypeWithMultipleCtors.Four == ((TypeWithMultipleCtors)r).Signature)
                };

                // SelectByValues
                yield return new object[]
                {
                    "Constructor with int, string, and float",  //  Name, 
                    null,                                       //  Type typeFrom, 
                    typeof(TypeWithMultipleCtors),              //  Type typeTo, 
                    typeof(TypeWithMultipleCtors),              //  Type typeToResolve, 
                    new object[] { 0, string.Empty, 0.0f },     //  object[] parameters, 
                    new Func<object, bool>(r => TypeWithMultipleCtors.Two == ((TypeWithMultipleCtors)r).Signature) 
                };


                // DefaultConstructorGeneric
                yield return new object[]
                {
                    "Constructor with no parameters",           //  Name, 
                    null,                                       //  Type typeFrom, 
                    typeof(InjectionTestCollection<>),          //  Type typeTo, 
                    typeof(InjectionTestCollection<object>),    //  Type typeToResolve, 
                    new object[] { },                           //  object[] parameters, 
                    new Func<object, bool>(r =>                 //  Func<object, bool> validator
                        typeof(InjectionTestCollection<>).Name == ((InjectionTestCollection<object>)r).CollectionName)  
                };


                //
                yield return new object[]
                {
                    "",                                         //  string name, 
                    null,                                       //  Type typeFrom, 
                    typeof(object),                             //  Type typeTo, 
                    typeof(object),                             //  Type typeToResolve, 
                    new object[] {},                            //  object[] parameters, 
                    new Func<object, bool>(r => true)           //  Func<object, bool> validator
                };
            }
        }


        public static IEnumerable<object[]> ConstructorRegistrationFailedTestData
        {

            // Format: Type typeFrom, Type typeTo, string name, Type typeToResolve, object[] parameters, Func<object, bool> validator
            get
            { 
                yield return new object[] { null, typeof(object),                                 null, typeof(object),                                   new object[] {}, new Func<object, bool>(r => true) };
                yield return new object[] { null, typeof(TestClass),                              null, typeof(TestClass),                                new object[] {}, new Func<object, bool>(r => true) };
                yield return new object[] { null, typeof(GenericTestClass<int, string, object>),  null, typeof(GenericTestClass<int, string, object>),    new object[] {}, new Func<object, bool>(r => true) };
                yield return new object[] { null, typeof(object),                                 "0",  typeof(object),                                   new object[] {}, new Func<object, bool>(r => true) };
                yield return new object[] { null, typeof(TestClass),                              "1",  typeof(TestClass),                                new object[] {}, new Func<object, bool>(r => true) };
                yield return new object[] { null, typeof(GenericTestClass<,,>),                   "2",  typeof(GenericTestClass<int, string, object>),    new object[] {}, new Func<object, bool>(r => true) };
                yield return new object[] { null, typeof(GenericTestClass<int, string, object>),  "3",  typeof(GenericTestClass<int, string, object>),    new object[] {}, new Func<object, bool>(r => true) };
                yield return new object[] { null, typeof(GenericTestClass<,,>),                   "4",  typeof(GenericTestClass<object, string, object>), new object[] {}, new Func<object, bool>(r => true) };
            }
        }


        [DataTestMethod]
        [DynamicData(nameof(ConstructorSelectionTestData))]
        public void Selection(string name, Type typeFrom, Type typeTo, Type typeToResolve, object[] parameters, Func<object, bool> validator)
        {
            // Setup
            Container.RegisterType(typeFrom, typeTo, name, null, Invoke.Constructor(parameters));

            // Act
            var result = Container.Resolve(typeToResolve, name);

            // Verify
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeToResolve);
            Assert.IsTrue(validator?.Invoke(result) ?? true);
        }

        [DataTestMethod]
        [DynamicData(nameof(DefaultConstructorTestData))]
        public void Default(Type typeFrom, Type typeTo, string name, Type typeToResolve)
        {
            // Setup
            Container.RegisterType(typeFrom, typeTo, name, null, Invoke.Constructor());

            // Act
            var result = Container.Resolve(typeToResolve, name);

            // Verify
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeToResolve);
        }


        [DataTestMethod]
        [DynamicData(nameof(DefaultConstructorTestDataFailed))]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void DefaultCtorValidation(Type type, string name)
        {
            // Setup
            Container.RegisterType((Type)null, type, name, null, Invoke.Constructor());

            // Act
            var result = Container.Resolve(type, name);
            Assert.IsNotNull(result);
        }

    }
}
