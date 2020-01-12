using BackgroundJobs.Data;
using BackgroundJobs.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackgroundJobs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ModelController : ControllerBase
    {
        private readonly ModelFlowContext _modelFlowContext;

        public ModelController(ModelFlowContext modelFlowContext)
        {
            _modelFlowContext = modelFlowContext;
        }

        [HttpGet]
        public IEnumerable<Model> Get() => _modelFlowContext.ModelFlow;

        [HttpPost]
        public async Task<Model> Post(Model input)
        {
            input.MapModelId();

            await _modelFlowContext.AddAsync(input);
            await _modelFlowContext.SaveChangesAsync();

            var created = await _modelFlowContext.ModelFlow.FindAsync(input.Id);
            created.RootNode.GenerateDtoResponse();
            return created;
        }

    }
}
