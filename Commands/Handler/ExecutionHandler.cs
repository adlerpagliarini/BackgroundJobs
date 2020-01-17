using BackgroundJobs.Data;
using BackgroundJobs.Interfaces;
using BackgroundJobs.Models.Domain;
using BackgroundJobs.Models.Publisher;
using Hangfire;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace BackgroundJobs.Commands.Handler
{
    public abstract class ExecutionHandler<T> : IExecution<T> where T : Message
    {
        protected readonly IPublisher JobPublisher;
        protected readonly ModelFlowContext ModelFlowContext;
        private readonly ICollection<dynamic> nextRequests = new Collection<dynamic>();
        protected ExecutionHandler(IPublisher jobPublisher, ModelFlowContext modelFlowContext)
        {
            ModelFlowContext = modelFlowContext;
            JobPublisher = jobPublisher;
        }

        public abstract Task Execute(T message);
        public async Task FireAndForget(T message)
        {
            await Execute(message);
            await ModelFlowContext.SaveChangesAsync();
            nextRequests.ToList().ForEach(r => JobPublisher.PublishMessage(r));
        }

        public void Publish(Request @request) => nextRequests.Add(request);
    }
}
