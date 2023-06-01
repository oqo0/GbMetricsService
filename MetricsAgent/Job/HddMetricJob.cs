using MetricsAgent.Services;
using Quartz;
using System.Diagnostics;

namespace MetricsAgent.Job
{
    public class HddMetricJob : IJob
    {
        private PerformanceCounter _hddCounter;
        private IServiceScopeFactory _serviceScopeFactory;

        public HddMetricJob(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _hddCounter = new PerformanceCounter("LogicalDisk", "% Disk Time", "C:");
        }

        public Task Execute(IJobExecutionContext context)
        {
            using (IServiceScope serviceScope = _serviceScopeFactory.CreateScope())
            {
                var hddMetricsRepository = serviceScope.ServiceProvider.GetService<IHddMetricsRepository>();
                try
                {
                    var hddUsageInPercents = _hddCounter.NextValue();
                    var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

                    hddMetricsRepository.Create(new Models.HddMetric
                    {
                        Value = (int)hddUsageInPercents,
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
