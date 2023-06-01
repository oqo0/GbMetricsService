using MetricsManager.Controllers;
using MetricsManager.Services;
using MetricsManager.Services.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsManagerTests
{
    public class HddMetricsControllerTests
    {
        private HddMetricsController _hddMetricsController;

        public HddMetricsControllerTests()
        {
            var mockLogger = new Mock<ILogger<HddMetricsController>>();
            var logger = mockLogger.Object;
            var mockMetricsAgentClient = new Mock<IMetricsAgentClient>();
            var metricsAgentClient = mockMetricsAgentClient.Object;
            var mockHttpClientFactory = new Mock<IHttpClientFactory>();
            var httpClientFactory = mockHttpClientFactory.Object;
            var mockAgentInfoRepository = new Mock<IAgentInfoRepository>();
            var agentInfoRepository = mockAgentInfoRepository.Object;

            _hddMetricsController = new HddMetricsController(logger, metricsAgentClient, httpClientFactory, agentInfoRepository);
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnOk()
        {
            int agentId = 1;
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);

            var result = _hddMetricsController.GetMetricsFromAgent(agentId, fromTime,
            toTime);

            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetMetricsAll_ReturnOk()
        {
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);

            var result = _hddMetricsController.GetMetricsFromAll( fromTime, toTime);

            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
