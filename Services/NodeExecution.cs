using BackgroundJobs.Data;
using BackgroundJobs.Models;
using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackgroundJobs.Services
{
    public class NodeExecution : JobExecution<Node>
    {
        public NodeExecution(IBackgroundJobClient hangFire, ModelFlowContext modelFlowContext) : base(hangFire, modelFlowContext)
        {
        }

        public async override Task Execute(Node node)
        {
            node.Name = "running";
            ModelFlowContext.Update(node);
            await Task.FromResult(0);
        }
    }
}
