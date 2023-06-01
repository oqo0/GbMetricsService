using AutoMapper;
using Castle.Core.Logging;
using MetricsAgent.Controllers;
using MetricsAgent.Models;
using MetricsAgent.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace MetricsAgentTests
{
    public class RamMetricsControllerTests
    {
        private readonly RamMetricsController _ramMetricsController;
        private readonly Mock<IRamMetricsRepository> _mock;

        public RamMetricsControllerTests()
        {
            var mockLogger = new Mock<ILogger<RamMetricsController>>();
            var logger = mockLogger.Object;
            var mockMapper = new Mock<IMapper>();
            var mapper = mockMapper.Object;
            
            _mock = new Mock<IRamMetricsRepository>();

            _ramMetricsController = new RamMetricsController(_mock.Object, logger, mapper);
        }

        [Fact]
        public void GetRamMetrics_ReturnOk()
        {
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(10800);
            var result = _ramMetricsController.GetRamMetrics(fromTime, toTime);
            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            _mock.Setup(repository => repository.Create(It.IsAny<RamMetric>())).Verifiable();

            var result = _ramMetricsController.Create(new
            MetricsAgent.Models.Requests.RamMetricCreateRequest
            {
                Time = TimeSpan.FromSeconds(1),
                Value = 50
            });

            _mock.Verify(repository => repository.Create(It.IsAny<RamMetric>()), Times.AtMostOnce());
        }
    }
}
