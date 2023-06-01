using MetricsAgent.Services;
using Quartz;
using System.Diagnostics;

namespace MetricsAgent.Job
{
    public class DotNetMetricJob : IJob
    {
        private PerformanceCounter _dotNetCounter;
        private IServiceScopeFactory _serviceScopeFactory;

        public DotNetMetricJob(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _dotNetCounter = new PerformanceCounter(".NET CLR Memory", "% Time in GC", "_Global_");
        }

        public Task Execute(IJobExecutionContext context)
        {
            using (IServiceScope serviceScope = _serviceScopeFactory.CreateScope())
            {
                var dotNetMetricsRepository = serviceScope.ServiceProvider.GetService<IDotNetMetricsRepository>();
                try
                {
                    var dotNetUsageInPercents = _dotNetCounter.NextValue();
                    var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
                    
                    dotNetMetricsRepository.Create(new Models.DotNetMetric
                    {
                        Value = (int)dotNetUsageInPercents,
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
