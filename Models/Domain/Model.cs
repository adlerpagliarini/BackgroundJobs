using System;

namespace BackgroundJobs.Models.Domain
{
    public class Model
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Node RootNode { get; set; }

        public Model()
        {
            Id = Id == Guid.Empty ? Guid.NewGuid() : Id;
        }
        public void MapModelId() => RootNode.SetModelId(Id);

    }
}
