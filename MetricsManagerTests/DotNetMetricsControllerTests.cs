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
    public class DotNetMetricsControllerTests
    {
        private DotNetMetricsController _dotNetMetricsController;

        public DotNetMetricsControllerTests()
        {
            var mockLogger = new Mock<ILogger<DotNetMetricsController>>();
            var logger = mockLogger.Object;
            var mockMetricsAgentClient = new Mock<IMetricsAgentClient>();
            var metricsAgentClient = mockMetricsAgentClient.Object;
            var mockHttpClientFactory = new Mock<IHttpClientFactory>();
            var httpClientFactory = mockHttpClientFactory.Object;
            var mockAgentInfoRepository = new Mock<IAgentInfoRepository>();
            var agentInfoRepository = mockAgentInfoRepository.Object;

            _dotNetMetricsController = new DotNetMetricsController(logger, metricsAgentClient, httpClientFactory, agentInfoRepository);
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnOk()
        {
            int agentId = 1;
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);

            var result = _dotNetMetricsController.GetMetricsFromAgent(agentId, fromTime,
            toTime);

            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetMetricsAll_ReturnOk()
        {
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);

            var result = _dotNetMetricsController.GetMetricsFromAll( fromTime, toTime);

            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
