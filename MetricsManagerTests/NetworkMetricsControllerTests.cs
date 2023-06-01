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
    public class NetworkMetricsControllerTests
    {
        private NetworkMetricsController _networkMetricsController;

        public NetworkMetricsControllerTests()
        {
            var mockLogger = new Mock<ILogger<NetworkMetricsController>>();
            var logger = mockLogger.Object;
            var mockMetricsAgentClient = new Mock<IMetricsAgentClient>();
            var metricsAgentClient = mockMetricsAgentClient.Object;
            var mockHttpClientFactory = new Mock<IHttpClientFactory>();
            var httpClientFactory = mockHttpClientFactory.Object;
            var mockAgentInfoRepository = new Mock<IAgentInfoRepository>();
            var agentInfoRepository = mockAgentInfoRepository.Object;

            _networkMetricsController = new NetworkMetricsController(logger, metricsAgentClient, httpClientFactory, agentInfoRepository);
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnOk()
        {
            int agentId = 1;
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);

            var result = _networkMetricsController.GetMetricsFromAgent(agentId, fromTime,
            toTime);

            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetMetricsAll_ReturnOk()
        {
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);

            var result = _networkMetricsController.GetMetricsFromAll( fromTime, toTime);

            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
