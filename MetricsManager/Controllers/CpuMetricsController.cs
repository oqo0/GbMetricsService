using MetricsManager.Services;
using MetricsManager.Models;
using MetricsManager.Models.Requests;
using MetricsManager.Services.Client;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MetricsManager.Controllers
{
    [Route("api/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        #region Services

        private readonly ILogger<CpuMetricsController> _logger;
        private IHttpClientFactory _httpClientFactory;
        private IMetricsAgentClient _metricsAgentClient;
        private IAgentInfoRepository _agentInfoRepository;

        #endregion

        public CpuMetricsController(
            ILogger<CpuMetricsController> logger,
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
        public ActionResult<CpuMetricsResponse> GetMetricsFromAgent(
            [FromQuery] int agentId, [FromQuery] TimeSpan fromTime, [FromQuery] TimeSpan toTime)
        {
            _logger.LogInformation("Get metrics from agent call.");
            
            return Ok(_metricsAgentClient.GetCpuMetrics(new CpuMetricsRequest
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
