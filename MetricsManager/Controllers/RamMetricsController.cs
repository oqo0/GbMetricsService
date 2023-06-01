using MetricsManager.Models;
using MetricsManager.Models.Requests;
using MetricsManager.Services;
using MetricsManager.Services.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MetricsManager.Controllers
{
    [Route("api/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        #region Services

        private readonly ILogger<RamMetricsController> _logger;
        private IHttpClientFactory _httpClientFactory;
        private IMetricsAgentClient _metricsAgentClient;
        private IAgentInfoRepository _agentInfoRepository;

        #endregion

        public RamMetricsController(
            ILogger<RamMetricsController> logger,
            IMetricsAgentClient metricsAgentClient,
            IHttpClientFactory httpClientFactory,
            IAgentInfoRepository agentInfoRepository)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _metricsAgentClient = metricsAgentClient;
            _agentInfoRepository = agentInfoRepository;
        }

        [HttpGet("get-all-by-id")]
        public ActionResult<RamMetricsResponse> GetMetricsFromAgent(
            [FromQuery] int agentId, [FromQuery] TimeSpan fromTime, [FromQuery] TimeSpan toTime)
        {
            _logger.LogInformation("Get metrics from agent call.");
            
            return Ok(_metricsAgentClient.GetRamMetrics(new RamMetricsRequest
            {
                AgentId = agentId,
                FromTime = fromTime,
                ToTime = toTime
            }));
        }

        [HttpGet("get-all")]
        public IActionResult GetMetricsFromAll(
            [FromQuery] TimeSpan fromTime, [FromQuery] TimeSpan toTime)
        {
            _logger.LogInformation("Get metrics from all call.");
            
            return Ok();
        }
    }
}
