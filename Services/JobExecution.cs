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
    public abstract class JobExecution<T> : IExecution<T> where T : class
    {
        protected readonly IBackgroundJobClient HangFire;
        protected readonly ModelFlowContext ModelFlowContext;
        protected JobExecution(IBackgroundJobClient hangFire, ModelFlowContext modelFlowContext)
        {
            ModelFlowContext = modelFlowContext;
            HangFire = hangFire;
        }

        public abstract Task Execute(T node);
        public async Task FireAndForget(T message)
        {
            await Execute(message);
            await ModelFlowContext.SaveChangesAsync();
        }
    }
}
