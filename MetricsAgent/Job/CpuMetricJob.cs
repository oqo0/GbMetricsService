using MetricsAgent.Services;
using Quartz;
using System.Diagnostics;
using NLog.Fluent;

namespace MetricsAgent.Job
{
    public class CpuMetricJob : IJob
    {
        private PerformanceCounter _cpuCounter;
        private IServiceScopeFactory _serviceScopeFactory;

        public CpuMetricJob(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        }

        public Task Execute(IJobExecutionContext context)
        {
            using (IServiceScope serviceScope = _serviceScopeFactory.CreateScope())
            {
                var cpuMetricsRepository = serviceScope.ServiceProvider.GetService<ICpuMetricsRepository>();
                try
                {
                    var cpuUsageInPercents = _cpuCounter.NextValue();
                    var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
                    
                    cpuMetricsRepository.Create(new Models.CpuMetric
                    {
                        Value = (int)cpuUsageInPercents,
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
