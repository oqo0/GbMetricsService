using AutoMapper;
using MetricsAgent.Controllers;
using MetricsAgent.Models;
using MetricsAgent.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace MetricsAgentTests
{
    public class CpuMetricsControllerTests
    {
        private readonly CpuMetricsController _cpuMetricsController;
        private readonly Mock<ICpuMetricsRepository> _mock;

        public CpuMetricsControllerTests()
        {
            var mockLogger = new Mock<ILogger<CpuMetricsController>>();
            var logger = mockLogger.Object;
            var mockMapper = new Mock<IMapper>();
            var mapper = mockMapper.Object;
            
            _mock = new Mock<ICpuMetricsRepository>();

            _cpuMetricsController = new CpuMetricsController(_mock.Object, logger, mapper);
        }

        [Fact]
        public void GetCpuMetrics_ReturnOk()
        {
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(10800);
            var result = _cpuMetricsController.GetCpuMetrics(fromTime, toTime);
            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            _mock.Setup(repository => repository.Create(It.IsAny<CpuMetric>())).Verifiable();

            var result = _cpuMetricsController.Create(new
            MetricsAgent.Models.Requests.CpuMetricCreateRequest
            {
                Time = TimeSpan.FromSeconds(1),
                Value = 50
            });

            _mock.Verify(repository => repository.Create(It.IsAny<CpuMetric>()), Times.AtMostOnce());
        }
    }
}
