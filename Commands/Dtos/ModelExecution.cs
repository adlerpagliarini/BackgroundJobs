using BackgroundJobs.Models.Publisher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackgroundJobs.Models.Commands.Dtos
{
    public class ModelExecution : Request
    {
        public Guid Id { get; set; }
    }
}
