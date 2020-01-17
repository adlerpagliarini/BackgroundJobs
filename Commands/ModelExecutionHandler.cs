using BackgroundJobs.Data;
using BackgroundJobs.Interfaces;
using BackgroundJobs.Models.Domain;
using BackgroundJobs.Models.Commands.Dtos;
using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackgroundJobs.Commands;
using BackgroundJobs.Commands.Handler;

namespace BackgroundJobs.Services
{
    public class ModelExecutionHandler : ExecutionHandler<ModelExecution>
    {
        public ModelExecutionHandler(IPublisher jobPublisher, ModelFlowContext modelFlowContext) : base(jobPublisher, modelFlowContext)
        {
        }

        public async override Task Execute(ModelExecution message)
        {
            var model = await ModelFlowContext.ModelFlow.FindAsync(message.Id);
            model.Name = "I'm Running";
            ModelFlowContext.Update(message);
        }
    }
}
