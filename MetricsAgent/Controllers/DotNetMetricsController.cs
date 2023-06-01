using AutoMapper;
using MetricsAgent.Models;
using MetricsAgent.Models.Dto;
using MetricsAgent.Models.Requests;
using MetricsAgent.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {
        #region Services

        private readonly ILogger<DotNetMetricsController> _logger;
        private readonly IDotNetMetricsRepository _dotNetMetricsRepository;
        private readonly IMapper _mapper;

        #endregion

        public DotNetMetricsController(
            IDotNetMetricsRepository dotNetMetricsRepository,
            ILogger<DotNetMetricsController> logger,
            IMapper mapper)
        {
            _dotNetMetricsRepository = dotNetMetricsRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] DotNetMetricCreateRequest request)
        {
            _dotNetMetricsRepository.Create(_mapper.Map<DotNetMetric>(request));
            return Ok();
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetDotNetMetrics(
            [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Get dotNet metrics call.");
            return Ok(_dotNetMetricsRepository.GetByTimePeriod(fromTime, toTime)
                       .Select(metric => _mapper.Map<DotNetMetricDto>(metric)).ToList());
        }

        [HttpGet("all")]
        public ActionResult<IList<DotNetMetricDto>> GetAllCpuMetrics()
        {
            return Ok(_dotNetMetricsRepository.GetAll()
                        .Select(metric => _mapper.Map<DotNetMetricDto>(metric)).ToList());
        }
    }
}
