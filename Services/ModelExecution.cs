using BackgroundJobs.Data;
using BackgroundJobs.Interfaces;
using BackgroundJobs.Models;
using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackgroundJobs.Services
{
    public class ModelExecution : JobExecution<Model>
    {
        public ModelExecution(IBackgroundJobClient hangFire, ModelFlowContext modelFlowContext) : base(hangFire, modelFlowContext)
        {
        }

        public async override Task Execute(Model message)
        {
            message.Name = "running";
            HangFire.Enqueue<IExecution<Node>>(job => job.FireAndForget(message.RootNode));
            ModelFlowContext.Update(message);
            await Task.FromResult(0);
        }
    }
}
