using AutoMapper;
using MetricsAgent.Controllers;
using MetricsAgent.Models;
using MetricsAgent.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace MetricsAgentTests
{
    public class NetworkMetricsControllerTests
    {
        private readonly NetworkMetricsController _networkMetricsController;
        private readonly Mock<INetworkMetricsRepository> _mock;

        public NetworkMetricsControllerTests()
        {
            var mockLogger = new Mock<ILogger<NetworkMetricsController>>();
            var logger = mockLogger.Object;
            var mockMapper = new Mock<IMapper>();
            var mapper = mockMapper.Object;
            
            _mock = new Mock<INetworkMetricsRepository>();

            _networkMetricsController = new NetworkMetricsController(_mock.Object, logger, mapper);
        }

        [Fact]
        public void GetNetworkMetrics_ReturnOk()
        {
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(10800);
            var result = _networkMetricsController.GetNetworkMetrics(fromTime, toTime);
            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            _mock.Setup(repository => repository.Create(It.IsAny<NetworkMetric>())).Verifiable();

            var result = _networkMetricsController.Create(new
            MetricsAgent.Models.Requests.NetworkMetricCreateRequest
            {
                Time = TimeSpan.FromSeconds(1),
                Value = 50
            });

            _mock.Verify(repository => repository.Create(It.IsAny<NetworkMetric>()), Times.AtMostOnce());
        }
    }
}
