using BackgroundJobs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackgroundJobs.Interfaces
{
    public interface IExecution<T> where T : class
    {
        Task FireAndForget(T message);
    }
}
