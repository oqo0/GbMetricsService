using AutoMapper;
using MetricsAgent.Models;
using MetricsAgent.Models.Dto;
using MetricsAgent.Models.Requests;
using MetricsAgent.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        #region Services

        private readonly ILogger<NetworkMetricsController> _logger;
        private readonly INetworkMetricsRepository _networkMetricsRepository;
        private readonly IMapper _mapper;

        #endregion

        public NetworkMetricsController(
            INetworkMetricsRepository networkMetricsRepository,
            ILogger<NetworkMetricsController> logger,
            IMapper mapper)
        {
            _networkMetricsRepository = networkMetricsRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] NetworkMetricCreateRequest request)
        {
            _networkMetricsRepository.Create(_mapper.Map<NetworkMetric>(request));
            return Ok();
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetNetworkMetrics(
            [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Get network metrics call.");
            return Ok(_networkMetricsRepository.GetByTimePeriod(fromTime, toTime)
                        .Select(metric => _mapper.Map<NetworkMetricDto>(metric)).ToList());
        }

        [HttpGet("all")]
        public ActionResult<IList<NetworkMetricDto>> GetAllCpuMetrics()
        {
            return Ok(_networkMetricsRepository.GetAll()
                        .Select(metric => _mapper.Map<NetworkMetricDto>(metric)).ToList());
        }
    }
}
