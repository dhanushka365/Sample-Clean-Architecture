using Sample.Application.interfaces.Sample;

namespace Sample.Application.Implementation
{
    public class SampleService : ISampleService
    {
        private readonly ISampleRepository _sampleRepository;

        public SampleService(ISampleRepository sampleRepository)
        {
            _sampleRepository = sampleRepository;

        }

        public List<Domain.Sample> GetAllSamples()
        {
            return _sampleRepository.GetAllSamples();
        }

    }
}
