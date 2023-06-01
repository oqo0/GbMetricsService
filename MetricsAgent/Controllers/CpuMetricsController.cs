using AutoMapper;
using MetricsAgent.Models;
using MetricsAgent.Models.Dto;
using MetricsAgent.Models.Requests;
using MetricsAgent.Services;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        #region Services

        private readonly ILogger<CpuMetricsController> _logger;
        private readonly ICpuMetricsRepository _cpuMetricsRepository;
        private readonly IMapper _mapper;

        #endregion

        public CpuMetricsController(
            ICpuMetricsRepository cpuMetricsRepository,
            ILogger<CpuMetricsController> logger,
            IMapper mapper)
        {
            _cpuMetricsRepository = cpuMetricsRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] CpuMetricCreateRequest request)
        {
            _cpuMetricsRepository.Create(_mapper.Map<CpuMetric>(request));
            return Ok();
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetCpuMetrics(
            [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Get cpu metrics call.");
            return Ok(_cpuMetricsRepository.GetByTimePeriod(fromTime, toTime)
                        .Select(metric => _mapper.Map<CpuMetricDto>(metric)).ToList());
        }

        [HttpGet("all")]
        public ActionResult<IList<CpuMetricDto>> GetAllCpuMetrics()
        {
            return Ok(_cpuMetricsRepository.GetAll()
                   .Select(metric => _mapper.Map<CpuMetricDto>(metric)).ToList());
        }
    }
}
