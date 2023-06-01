using MetricsManager.Models;
using MetricsManager.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {

        #region Services

        private readonly ILogger<AgentsController> _logger;
        private readonly IAgentInfoRepository _agentInfoRepository;

        #endregion

        #region Constuctors

        public AgentsController(ILogger<AgentsController> logger, IAgentInfoRepository agentInfoRepository)
        {
            _logger = logger;
            _agentInfoRepository = agentInfoRepository;
        }

        #endregion

        #region Public Methods

        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
        {
            _logger.LogInformation("Register agent call.");
            
            if (agentInfo != null)
            {
                _agentInfoRepository.Create(agentInfo);
            }
            
            return Ok();
        }

        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            _logger.LogInformation("Enable agent call.");
            _agentInfoRepository.EnableOrDisableAgentById(agentId, true);
            
            return Ok();
        }

        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            _logger.LogInformation("Disable agent call.");
            _agentInfoRepository.EnableOrDisableAgentById(agentId, false);
            
            return Ok();
        }
        
        [HttpGet("get")]
        public ActionResult<AgentInfo[]> GetAllAgents()
        {
            _logger.LogInformation("Get all agent call.");
            
            return Ok(_agentInfoRepository.GetAll());
        }

        #endregion
    }
}
