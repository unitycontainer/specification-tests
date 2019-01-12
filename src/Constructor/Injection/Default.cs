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
                yield return new object[] { null,       typeof(object),                                 null,       typeof(object)                                   };
                yield return new object[] { null,       typeof(TestClass),                              null,       typeof(TestClass)                                };
                yield return new object[] { null,       typeof(GenericTestClass<int, string, object>),  null,       typeof(GenericTestClass<int, string, object>)    };
                yield return new object[] { null,       typeof(object),                                 "0",        typeof(object)                                   };
                yield return new object[] { null,       typeof(TestClass),                              "1",        typeof(TestClass)                                };
                yield return new object[] { null,       typeof(GenericTestClass<,,>),                   "2",        typeof(GenericTestClass<int, string, object>)    };
                yield return new object[] { null,       typeof(GenericTestClass<int, string, object>),  "3",        typeof(GenericTestClass<int, string, object>)    };
                yield return new object[] { null,       typeof(GenericTestClass<,,>),                   "4",        typeof(GenericTestClass<object, string, object>) };
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
    }
}
