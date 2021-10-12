using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Moq;

namespace ZipPayDemo.Tests.Utilities
{
    public static class MockHelper
    {
        public static Dictionary<Type, Mock> CreateMocks(ConstructorInfo constructorInfo)
        {
            var parameters = constructorInfo.GetParameters();
            var types = parameters.Select(x => x.ParameterType).ToArray();

            return CreateMocks(types);
        }

        public static Dictionary<Type, Mock> CreateMocks(params Type[] types)
        {
            var mock = typeof(Mock<>);
            var dict = new Dictionary<Type, Mock>();
            foreach (var typeToMock in types)
            {
                var target = mock.MakeGenericType(typeToMock);
                dict.Add(typeToMock, (Mock)Activator.CreateInstance(target));
            }

            return dict;
        }
    }
}