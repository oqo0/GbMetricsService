using MetricsManager.Models;
using MetricsManager.Models.Requests;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;

namespace MetricsManager.Services.Client.Impl
{
    public class MetricsAgentClient : IMetricsAgentClient
    {
        #region Services

        private readonly HttpClient _httpClient;
        private IAgentInfoRepository _agentInfoRepository;

        #endregion

        public MetricsAgentClient(HttpClient httpClient, IAgentInfoRepository agentInfoRepository)
        {
            _httpClient = httpClient;
            _agentInfoRepository = agentInfoRepository;
        }

        public CpuMetricsResponse GetCpuMetrics(CpuMetricsRequest request)
        {
            AgentInfo agentInfo = _agentInfoRepository.GetById(request.AgentId);
            if (agentInfo == null)
                return null;

            string requestStr =
                $"{agentInfo.AgentAddress}api/metrics/cpu/from/{request.FromTime.ToString("dd\\.hh\\:mm\\:ss")}/to/{request.ToTime.ToString("dd\\.hh\\:mm\\:ss")}";
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestStr);
            httpRequestMessage.Headers.Add("Accept", "application/json");
            HttpResponseMessage response = _httpClient.SendAsync(httpRequestMessage).Result;
            if (response.IsSuccessStatusCode)
            {
                string responseStr = response.Content.ReadAsStringAsync().Result;
                var metrics = JsonConvert.DeserializeObject<CpuMetric[]>(responseStr);
                return new CpuMetricsResponse() { AgentId = request.AgentId, Metrics = metrics };
            }
            return null;
        }

        public DotNetMetricsResponse GetDotNetMetrics(DotNetMetricsRequest request)
        {
            AgentInfo agentInfo = _agentInfoRepository.GetById(request.AgentId);
            if (agentInfo == null)
                return null;

            string requestStr =
                $"{agentInfo.AgentAddress}api/metrics/dotNet/from/{request.FromTime.ToString("dd\\.hh\\:mm\\:ss")}/to/{request.ToTime.ToString("dd\\.hh\\:mm\\:ss")}";
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestStr);
            httpRequestMessage.Headers.Add("Accept", "application/json");
            HttpResponseMessage response = _httpClient.SendAsync(httpRequestMessage).Result;
            if (response.IsSuccessStatusCode)
            {
                string responseStr = response.Content.ReadAsStringAsync().Result;
                var metrics = JsonConvert.DeserializeObject<DotNetMetric[]>(responseStr);
                return new DotNetMetricsResponse() { AgentId = request.AgentId, Metrics = metrics };
            }
            return null;
        }

        public HddMetricsResponse GetHddMetrics(HddMetricsRequest request)
        {
            AgentInfo agentInfo = _agentInfoRepository.GetById(request.AgentId);
            if (agentInfo == null)
                return null;

            string requestStr =
                $"{agentInfo.AgentAddress}api/metrics/hdd/from/{request.FromTime.ToString("dd\\.hh\\:mm\\:ss")}/to/{request.ToTime.ToString("dd\\.hh\\:mm\\:ss")}";
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestStr);
            httpRequestMessage.Headers.Add("Accept", "application/json");
            HttpResponseMessage response = _httpClient.SendAsync(httpRequestMessage).Result;
            if (response.IsSuccessStatusCode)
            {
                string responseStr = response.Content.ReadAsStringAsync().Result;
                var metrics = JsonConvert.DeserializeObject<HddMetric[]>(responseStr);
                return new HddMetricsResponse() { AgentId = request.AgentId, Metrics = metrics };
            }
            return null;
        }

        public NetworkMetricsResponse GetNetworkMetrics(NetworkMetricsRequest request)
        {
            AgentInfo agentInfo = _agentInfoRepository.GetById(request.AgentId);
            if (agentInfo == null)
                return null;

            string requestStr =
                $"{agentInfo.AgentAddress}api/metrics/network/from/{request.FromTime.ToString("dd\\.hh\\:mm\\:ss")}/to/{request.ToTime.ToString("dd\\.hh\\:mm\\:ss")}";
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestStr);
            httpRequestMessage.Headers.Add("Accept", "application/json");
            HttpResponseMessage response = _httpClient.SendAsync(httpRequestMessage).Result;
            if (response.IsSuccessStatusCode)
            {
                string responseStr = response.Content.ReadAsStringAsync().Result;
                var metrics = JsonConvert.DeserializeObject<NetworkMetric[]>(responseStr);
                return new NetworkMetricsResponse() { AgentId = request.AgentId, Metrics = metrics };
            }
            return null;
        }

        public RamMetricsResponse GetRamMetrics(RamMetricsRequest request)
        {
            AgentInfo agentInfo = _agentInfoRepository.GetById(request.AgentId);
            if (agentInfo == null)
                return null;

            string requestStr =
                $"{agentInfo.AgentAddress}api/metrics/ram/from/{request.FromTime.ToString("dd\\.hh\\:mm\\:ss")}/to/{request.ToTime.ToString("dd\\.hh\\:mm\\:ss")}";
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestStr);
            httpRequestMessage.Headers.Add("Accept", "application/json");
            HttpResponseMessage response = _httpClient.SendAsync(httpRequestMessage).Result;
            if (response.IsSuccessStatusCode)
            {
                string responseStr = response.Content.ReadAsStringAsync().Result;
                var metrics = JsonConvert.DeserializeObject<RamMetric[]>(responseStr);
                return new RamMetricsResponse() { AgentId = request.AgentId, Metrics = metrics };
            }
            return null;
        }
    }
}
