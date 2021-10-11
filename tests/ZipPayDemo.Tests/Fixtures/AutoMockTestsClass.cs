using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using AutoFixture;
using AutoMapper;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace ZipPayDemo.Tests.Fixtures
{
    [ExcludeFromCodeCoverage]
    public abstract class AutoMockTestsClass<TTarget> : IClassFixture<CommonTestsFixture>
    {
        private readonly Dictionary<Type, Mock> _mocks;
        private readonly CommonTestsFixture _testsFixture;
        protected CancellationToken CancellationToken { get; }
        protected TTarget Target { get; set; }
        protected IMapper Mapper => _testsFixture.Mapper;
        protected Fixture Fixture => _testsFixture.Fixture;
        protected AutoMockTestsClass(CommonTestsFixture testsFixture)
        {
            _testsFixture = testsFixture;
            var ctor = typeof(TTarget).GetConstructors().First(x => x.IsPublic);
            _mocks = MockHelper.CreateMocks(ctor);
            CancellationToken = It.IsAny<CancellationToken>();
            var arguments = _mocks.Select(kp =>
            {
                if (kp.Key == typeof(IMapper))
                {
                    return Mapper;
                }

                return kp.Value.Object;
            }).ToArray();

            Target = (TTarget)ctor.Invoke(arguments);
        }

        protected Mock<T> Mock<T>() where T : class
        {
            return (Mock<T>)_mocks[typeof(T)];
        }

        protected void SetupMapper<T>(T obj = null) where T : class, new()
        {
            Mock<IMapper>().Setup(x => x.Map<T>(It.IsAny<object>())).Returns(obj ?? new T());
        }

        protected HttpResponseMessage OkResponse(object content)
        {
            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = content != null ? new StringContent(JsonConvert.SerializeObject(content)) : null
            };
        }

        protected HttpResponseMessage ErrorResponse()
        {
            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError,
            };
        }

        protected void SetupOptions<T>(T options) where T : class, new()
        {
            Mock<IOptions<T>>().Setup(x => x.Value).Returns(options);
        }
    }
}
