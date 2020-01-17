using BackgroundJobs.Commands.Handler;
using BackgroundJobs.Data;
using BackgroundJobs.Interfaces;
using BackgroundJobs.Models.Commands.Dtos;
using BackgroundJobs.Models.Domain;
using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackgroundJobs.Services
{
    public class NodeExecutionHandler : ExecutionHandler<NodeExecution>
    {
        public NodeExecutionHandler(IPublisher jobPublisher, ModelFlowContext modelFlowContext) : base(jobPublisher, modelFlowContext)
        {
        }

        public async override Task Execute(NodeExecution message)
        {
            var node = await ModelFlowContext.NodeFlow.FindAsync(message.Id);
            node.Name = "I'm running";
            node.ExecuteOperation(message.Input);
            
            var nextNodesToExecute = node.LinkedNodes.Select(n => n.Id);
            nextNodesToExecute.ToList().ForEach(next => new NodeExecution { Id = next, Input = node.Output.Value });

            ModelFlowContext.Update(node);
        }
    }
}
