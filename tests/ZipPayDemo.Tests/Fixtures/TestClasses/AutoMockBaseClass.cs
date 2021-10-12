using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AutoMapper;
using Microsoft.Extensions.Options;
using Moq;
using ZipPayDemo.Application;
using ZipPayDemo.Tests.Utilities;

namespace ZipPayDemo.Tests.Fixtures.TestClasses
{
    public abstract class AutoMockBaseClass<TTarget>
    {
        private readonly Dictionary<Type, Mock> _mocks;
        private IMapper _mapper = null;
        protected TTarget Target { get; set; }
        protected CancellationToken CancellationToken { get; }

        public IMapper Mapper
        {
            get
            {
                if (_mapper == null)
                {
                    var configurationProvider = new MapperConfiguration(
                        cfg => cfg.AddMaps(typeof(AutoMapperProfile)));
                    _mapper = configurationProvider.CreateMapper();
                }

                return _mapper;
            }
        }

        protected AutoMockBaseClass()
        {
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

        protected void SetupOptions<T>(T options) where T : class, new()
        {
            Mock<IOptions<T>>().Setup(x => x.Value).Returns(options);
        }
    }
}