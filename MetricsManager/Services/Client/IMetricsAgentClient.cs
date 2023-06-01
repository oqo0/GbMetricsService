using MetricsManager.Models.Requests;

namespace MetricsManager.Services.Client
{
    public interface IMetricsAgentClient
    {
        CpuMetricsResponse GetCpuMetrics(CpuMetricsRequest request);
        DotNetMetricsResponse GetDotNetMetrics(DotNetMetricsRequest request);
        HddMetricsResponse GetHddMetrics(HddMetricsRequest request);
        NetworkMetricsResponse GetNetworkMetrics(NetworkMetricsRequest request);
        RamMetricsResponse GetRamMetrics(RamMetricsRequest request);
    }
}
