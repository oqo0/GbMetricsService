using MetricsAgent.Services;
using Quartz;
using System.Diagnostics;

namespace MetricsAgent.Job
{
    public class RamMetricJob : IJob
    {
        private PerformanceCounter _ramCounter;
        private IServiceScopeFactory _serviceScopeFactory;

        public RamMetricJob(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _ramCounter = new PerformanceCounter("Memory", "Available MBytes");
        }

        public Task Execute(IJobExecutionContext context)
        {
            using (IServiceScope serviceScope = _serviceScopeFactory.CreateScope())
            {
                var ramMetricsRepository = serviceScope.ServiceProvider.GetService<IRamMetricsRepository>();
                try
                {
                    var ramUsageInPercents = _ramCounter.NextValue();
                    var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

                    ramMetricsRepository.Create(new Models.RamMetric
                    {
                        Value = (int)ramUsageInPercents,
                        Time = (long)time.TotalSeconds
                    });
                }
                catch
                {
                    // ingnored
                }
            }               

            return Task.CompletedTask;
        }
    }
}
