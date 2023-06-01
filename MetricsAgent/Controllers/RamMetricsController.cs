using AutoMapper;
using MetricsAgent.Models;
using MetricsAgent.Models.Dto;
using MetricsAgent.Models.Requests;
using MetricsAgent.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        #region Services

        private readonly ILogger<RamMetricsController> _logger;
        private readonly IRamMetricsRepository _ramMetricsRepository;
        private readonly IMapper _mapper;

        #endregion

        public RamMetricsController(
            IRamMetricsRepository ramMetricsRepository,
            ILogger<RamMetricsController> logger,
            IMapper mapper)
        {
            _ramMetricsRepository = ramMetricsRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] RamMetricCreateRequest request)
        {
            _ramMetricsRepository.Create(_mapper.Map<RamMetric>(request));
            return Ok();
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetRamMetrics(
            [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Get ram metrics call.");
            return Ok(_ramMetricsRepository.GetByTimePeriod(fromTime, toTime)
                        .Select(metric => _mapper.Map<RamMetricDto>(metric)).ToList());
        }

        [HttpGet("all")]
        public ActionResult<IList<RamMetricDto>> GetAllCpuMetrics()
        {
            return Ok(_ramMetricsRepository.GetAll()
                        .Select(metric => _mapper.Map<RamMetricDto>(metric)).ToList());
        }
    }
}
