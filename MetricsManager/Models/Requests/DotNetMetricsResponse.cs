using System.Text.Json.Serialization;

namespace MetricsManager.Models.Requests
{
    public class DotNetMetricsResponse
    {
        public int AgentId { get; set; }

        [JsonPropertyName("metrics")]
        public DotNetMetric[] Metrics { get; set; }
    }
}
