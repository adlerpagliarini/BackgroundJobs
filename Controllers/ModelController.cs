using BackgroundJobs.Data;
using BackgroundJobs.Interfaces;
using BackgroundJobs.Models;
using BackgroundJobs.Models.Domain;
using BackgroundJobs.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackgroundJobs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ModelController : ControllerBase
    {
        private readonly ModelFlowContext _modelFlowContext;
        private readonly IExecution<Model> modelExecution;
        public ModelController(ModelFlowContext modelFlowContext, IExecution<Model> modelExecution)
        {
            _modelFlowContext = modelFlowContext;
            this.modelExecution = modelExecution;
        }

        [HttpGet("execute")]
        public async Task<Model> Execute() {
            var models = await _modelFlowContext.ModelFlow.Include(e => e.RootNode).ToListAsync();
            var model = models.FirstOrDefault();
            await modelExecution.FireAndForget(model);
            return model;
        }

        [HttpGet]
        public IEnumerable<Model> Get() => _modelFlowContext.ModelFlow.ToList();

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
