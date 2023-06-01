using MetricsManager.Controllers;
using MetricsManager.Services;
using MetricsManager.Services.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace MetricsManagerTests
{
    public class CpuMetricsControllerTests
    {
        private CpuMetricsController _cpuMetricsController;

        public CpuMetricsControllerTests()
        {
            var mockLogger = new Mock<ILogger<CpuMetricsController>>();
            var logger = mockLogger.Object;
            var mockMetricsAgentClient = new Mock<IMetricsAgentClient>();
            var metricsAgentClient = mockMetricsAgentClient.Object;
            var mockHttpClientFactory = new Mock<IHttpClientFactory>();
            var httpClientFactory = mockHttpClientFactory.Object;
            var mockAgentInfoRepository = new Mock<IAgentInfoRepository>();
            var agentInfoRepository = mockAgentInfoRepository.Object;

            _cpuMetricsController = new CpuMetricsController(logger, metricsAgentClient, httpClientFactory, agentInfoRepository);
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnOk()
        {
            int agentId = 1;
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);

            var result = _cpuMetricsController.GetMetricsFromAgent(agentId, fromTime,
            toTime);

            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetMetricsAll_ReturnOk()
        {
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);

            var result = _cpuMetricsController.GetMetricsFromAll( fromTime, toTime);

            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
