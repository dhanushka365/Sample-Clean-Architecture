using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sample.Application.interfaces.Sample;

namespace Sample.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SamplesController : ControllerBase
    {
        private readonly ISampleService _sampleService;

        public SamplesController(ISampleService sampleService)
        {
            _sampleService = sampleService;
        }

        [HttpGet]
        public ActionResult<IList<Domain.Sample>> Get()
        {
            return Ok(_sampleService.GetAllSamples());
        }
    }
}
