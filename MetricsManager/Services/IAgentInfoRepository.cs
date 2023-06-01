using MetricsManager.Models;

namespace MetricsManager.Services
{
    public interface IAgentInfoRepository : IRepository<AgentInfo>
    {
        void EnableOrDisableAgentById(int agentId, bool enable);
    }
}
