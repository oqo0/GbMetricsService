using Dapper;
using MetricsManager.Models;
using Microsoft.Extensions.Options;
using System.Data.SQLite;

namespace MetricsManager.Services.Impl
{
    public class AgentInfoRepository : IAgentInfoRepository
    {
        
        #region Services

        private readonly IOptions<DatabaseOptions> _databaseOptions;

        #endregion

        public AgentInfoRepository(IOptions<DatabaseOptions> databaseOptions)
        {
            _databaseOptions = databaseOptions;
        }

        public void Create(AgentInfo item)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            connection.Execute("INSERT INTO agentInfo(agentId, agentAddress, enable) VALUES(@agentId, @agentAddress, @enable)", new
            {
                agentId = item.AgentId,
                agentAddress = item.AgentAddress,
                enable = item.Enable
            });
        }

        public void Delete(int agentId)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            connection.Execute("DELETE FROM agentInfo WHERE agentId=@agentId",
            new
            {
                agentId = agentId
            });
        }

        public void Update(AgentInfo item)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            connection.Execute("UPDATE agentInfo SET agentAddress = @agentAddress, enable = @enable WHERE agentId = @agentId",
            new
            {
                agentId = item.AgentId,
                agentAddress = item.AgentAddress,
                enable = item.Enable
            });
        }

        public IList<AgentInfo> GetAll()
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            return connection.Query<AgentInfo>("SELECT agentId, agentAddress, enable FROM agentInfo").ToList();
        }

        public AgentInfo GetById(int agentId)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            AgentInfo metric = connection.QuerySingle<AgentInfo>("SELECT agentId, agentAddress, enable FROM agentInfo WHERE agentId = @agentId",
            new { agentId = agentId });
            return metric;
        }

        public void EnableOrDisableAgentById(int agentId, bool enable)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            connection.Execute("UPDATE agentInfo SET enable = @enable WHERE agentId = @agentId",
            new
            {
                agentId = agentId,
                enable = enable
            });
        }
    }
}
