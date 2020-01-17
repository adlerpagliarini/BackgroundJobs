using BackgroundJobs.Models.Publisher;
using System.Threading.Tasks;

namespace BackgroundJobs.Interfaces
{
    public interface IPublisher
    {
        Task PublishMessage<T>(T message) where T : Message;
    }
}
