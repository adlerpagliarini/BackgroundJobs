using BackgroundJobs.Data;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BackgroundJobs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlowExecutionController : ControllerBase
    {
        private readonly ILogger<FlowExecutionController> _logger;
        private readonly ModelFlowContext _modelFlowContext;
        private readonly IBackgroundJobClient _hangFire;

        public FlowExecutionController(ILogger<FlowExecutionController> logger, ModelFlowContext modelFlowContext, IBackgroundJobClient hangFire)
        {
            _logger = logger;
            _modelFlowContext = modelFlowContext;
            _hangFire = hangFire;
        }

        [HttpGet]
        public IActionResult Get()
        {
            //_hangFire.Enqueue(() => ModelExecution());
            return Ok("Execution Started");
        }
    }
}
