using AutoMapper;
using MetricsAgent.Controllers;
using MetricsAgent.Models;
using MetricsAgent.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace MetricsAgentTests
{
    public class HddMetricsControllerTests
    {
        private readonly HddMetricsController _hddMetricsController;
        private readonly Mock<IHddMetricsRepository> _mock;

        public HddMetricsControllerTests()
        {
            var mockLogger = new Mock<ILogger<HddMetricsController>>();
            var logger = mockLogger.Object;
            var mockMapper = new Mock<IMapper>();
            var mapper = mockMapper.Object;
            
            _mock = new Mock<IHddMetricsRepository>();

            _hddMetricsController = new HddMetricsController(_mock.Object, logger, mapper);
        }

        [Fact]
        public void GetHddMetrics_ReturnOk()
        {
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(10800);
            var result = _hddMetricsController.GetHddMetrics(fromTime, toTime);
            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            _mock.Setup(repository => repository.Create(It.IsAny<HddMetric>())).Verifiable();

            var result = _hddMetricsController.Create(new
            MetricsAgent.Models.Requests.HddMetricCreateRequest
            {
                Time = TimeSpan.FromSeconds(1),
                Value = 50
            });

            _mock.Verify(repository => repository.Create(It.IsAny<HddMetric>()), Times.AtMostOnce());
        }
    }
}
