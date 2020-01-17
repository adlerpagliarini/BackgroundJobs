using BackgroundJobs.Models.Publisher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackgroundJobs.Models.Commands.Dtos
{
    public class NodeExecution : Request
    {
        public Guid Id { get; set; }
        public long Input { get; set; }
    }
}
