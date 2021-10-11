using System.Linq;
using AutoFixture;
using AutoMapper;
using ZipPayDemo.Application;

namespace ZipPayDemo.Tests.Fixtures
{
    public class CommonTestsFixture
    {
        private IMapper _mapper = null;
        private Fixture _fixture = null;

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

        public Fixture Fixture
        {
            get
            {
                if (_fixture == null)
                {
                    _fixture = new Fixture();

                    _fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
                        .ToList()
                        .ForEach(b => Fixture.Behaviors.Remove(b));

                    _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
                    //_fixture.Customizations.Add(new TypeRelay(typeof(I...), typeof(Mock...)));
                }

                return _fixture;
            }
        }
    }
}