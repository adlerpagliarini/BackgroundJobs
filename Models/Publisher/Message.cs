using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackgroundJobs.Models.Publisher
{
    public abstract class Message
    {
        public Guid MessageId { get; protected set; }
        public string MessageType { get; protected set; }
        protected Message()
        {
            MessageId = Guid.NewGuid();
            MessageType = GetType().Name;
        }
    }
}
