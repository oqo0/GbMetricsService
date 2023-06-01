using AutoMapper;
using MetricsAgent.Controllers;
using MetricsAgent.Models;
using MetricsAgent.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace MetricsAgentTests
{
    public class DotNetMetricsControllerTests
    {
        private readonly DotNetMetricsController _dotNetMetricsController;
        private readonly Mock<IDotNetMetricsRepository> _mock;

        public DotNetMetricsControllerTests()
        {
            var mockLogger = new Mock<ILogger<DotNetMetricsController>>();
            var logger = mockLogger.Object;
            var mockMapper = new Mock<IMapper>();
            var mapper = mockMapper.Object;
            
            _mock = new Mock<IDotNetMetricsRepository>();

            _dotNetMetricsController = new DotNetMetricsController(_mock.Object, logger, mapper);
        }

        [Fact]
        public void GetDotNetMetrics_ReturnOk()
        {
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(10800);
            var result = _dotNetMetricsController.GetDotNetMetrics(fromTime, toTime);
            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            _mock.Setup(repository => repository.Create(It.IsAny<DotNetMetric>())).Verifiable();

            var result = _dotNetMetricsController.Create(new
            MetricsAgent.Models.Requests.DotNetMetricCreateRequest
            {
                Time = TimeSpan.FromSeconds(1),
                Value = 50
            });

            _mock.Verify(repository => repository.Create(It.IsAny<DotNetMetric>()), Times.AtMostOnce());
        }
    }
}
