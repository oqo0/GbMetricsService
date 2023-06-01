using MetricsAgent.Services;
using Quartz;
using System.Diagnostics;

namespace MetricsAgent.Job
{
    public class NetworkMetricJob : IJob
    {
        private PerformanceCounter _networkCounter;
        private IServiceScopeFactory _serviceScopeFactory;

        public NetworkMetricJob(IServiceScopeFactory serviceScopeFactory)
        {
            PerformanceCounterCategory pcNetworkInterface = new PerformanceCounterCategory("Network Interface");

            _networkCounter = new PerformanceCounter("Network Interface", "Bytes Sent/sec", 
                pcNetworkInterface.GetInstanceNames().Where(x => x != "MS TCP Loopback interface").First());

            _serviceScopeFactory = serviceScopeFactory;
        }

        public Task Execute(IJobExecutionContext context)
        {
            using (IServiceScope serviceScope = _serviceScopeFactory.CreateScope())
            {
                var networkMetricsRepository = serviceScope.ServiceProvider.GetService<INetworkMetricsRepository>();
                try
                {
                    var networkUsageInPercents = _networkCounter.NextValue();
                    var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

                    networkMetricsRepository.Create(new Models.NetworkMetric
                    {
                        Value = (int)networkUsageInPercents,
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
