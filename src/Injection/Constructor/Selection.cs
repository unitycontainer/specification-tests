using System;
using System.Collections.Generic;

namespace Unity.Specification.Injection.Constructor
{
    public abstract partial class SpecificationTests 
    {
        public static IEnumerable<object[]> ConstructorSelectionTestData
        {

            // Format: Type typeFrom, Type typeTo, string name, Type typeToResolve, object[] parameters, Func<object, bool> validator
            get
            {
                //
                yield return new object[]
                {
                    null,                                       //  Type typeFrom, 
                    typeof(object),                             //  Type typeTo, 
                    "",                                         //  string name, 
                    typeof(object),                             //  Type typeToResolve, 
                    new object[] {},                            //  object[] parameters, 
                    new Func<object, bool>(r => true)           //  Func<object, bool> validator
                };


                // DefaultConstructorGeneric
                yield return new object[]
                {
                    null,                                       //  Type typeFrom, 
                    typeof(InjectionTestCollection<>),          //  Type typeTo, 
                    "DefaultConstructorGeneric",                //  string name, 
                    typeof(InjectionTestCollection<object>),    //  Type typeToResolve, 
                    new object[] { },                           //  object[] parameters, 
                    new Func<object, bool>(r =>                 //  Func<object, bool> validator
                        typeof(InjectionTestCollection<>).Name == ((InjectionTestCollection<object>)r).CollectionName)  
                };


                // SelectAndResolveByValue
                yield return new object[]
                {
                    null,                                       //  Type typeFrom, 
                    typeof(TypeWithAmbiguousCtors),             //  Type typeTo, 
                    "SelectAndResolveByValue",                  //  string name, 
                    typeof(TypeWithAmbiguousCtors),             //  Type typeToResolve, 
                    new object[] {                              
                        Resolve.Parameter(typeof(string)),      //  object[] parameters, 
                        string.Empty,
                        string.Empty },                            
                    new Func<object, bool>(r =>                 //  Func<object, bool> validator
                        TypeWithAmbiguousCtors.Four == ((TypeWithAmbiguousCtors)r).Signature)           
                };


                // ResolveNamedTypeArgument
                yield return new object[]
                {
                    null,                                       //  Type typeFrom, 
                    typeof(TypeWithAmbiguousCtors),             //  Type typeTo, 
                    "ResolveNamedTypeArgument",                 //  string name, 
                    typeof(TypeWithAmbiguousCtors),             //  Type typeToResolve, 
                    new object[] {
                        typeof(string),                         //  object[] parameters, 
                        typeof(string),
                        typeof(IUnityContainer)},               
                    new Func<object, bool>(r =>                 //  Func<object, bool> validator
                        TypeWithAmbiguousCtors.Five == ((TypeWithAmbiguousCtors)r).Signature)           
                };


                // SelectByValues
                yield return new object[]
                {
                    null,                                       //  Type typeFrom, 
                    typeof(TypeWithAmbiguousCtors),             //  Type typeTo, 
                    "SelectByValues",                           //  string name, 
                    typeof(TypeWithAmbiguousCtors),             //  Type typeToResolve, 
                    new object[] { 0, string.Empty, 0.0f },     //  object[] parameters, 
                    new Func<object, bool>(r => TypeWithAmbiguousCtors.Two == ((TypeWithAmbiguousCtors)r).Signature)           //  Func<object, bool> validator
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
    }
}
