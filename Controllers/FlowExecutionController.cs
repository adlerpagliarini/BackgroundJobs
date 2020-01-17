using BackgroundJobs.Data;
using BackgroundJobs.Interfaces;
using BackgroundJobs.Models.Commands.Dtos;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BackgroundJobs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlowExecutionController : ControllerBase
    {
        private readonly IPublisher _publisher;

        public FlowExecutionController(IPublisher publisher)
        {
            _publisher = publisher;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var message = new ModelExecution();
            _publisher.PublishMessage(message);
            return Ok("Execution Started");
        }
    }
}
