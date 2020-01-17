using BackgroundJobs.Interfaces;
using BackgroundJobs.Models.Publisher;
using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackgroundJobs.Commands.Handler
{
    public class PublishHandler : IPublisher
    {
        private readonly IBackgroundJobClient _backgroundJobClient;

        public PublishHandler(IBackgroundJobClient backgroundJobClient)
        {
            _backgroundJobClient = backgroundJobClient;
        }

        public async Task PublishMessage<T>(T message) where T : Message => 
            _backgroundJobClient.Enqueue<IExecution<T>>(process => process.FireAndForget(message));

    }
}
