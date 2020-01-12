using System;
using System.Collections.Generic;

namespace BackgroundJobs.Models
{
    public partial class Node
    {
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        public Guid ModelId { get; set; }
        public string Name { get; set; }
        public MathOperation Operation { get; set; }
        public long Input { get; set; }
        public long? Output { get; set; }
        public List<Node> LinkedNodes { get; set; }

        public virtual Node ParentReference { get; set; }

        public long ExecuteOperation(long input) => Operation switch
        {
            MathOperation.Addition => input + 2,
            MathOperation.Division => input / 2,
            MathOperation.Multiplication => input * 2,
            MathOperation.Subtraction => input - 2,
            _ => input
        };

        public Node()
        {
            Id = Id == Guid.Empty ? Guid.NewGuid() : Id;
            LinkedNodes = new List<Node>();
        }
        public void SetModelId(Guid modelId)
        {
            if (ModelId != Guid.Empty) return;

            ModelId = modelId;
            LinkedNodes ??= new List<Node>();
            LinkedNodes = MapLinkedNodes(Id, ModelId, LinkedNodes);

            static List<Node> MapLinkedNodes(Guid parentId, Guid modelId, List<Node> LinkedNodes)
            {
                foreach (var node in LinkedNodes)
                {
                    node.ModelId = modelId;
                    node.ParentId = parentId;
                    node.LinkedNodes = MapLinkedNodes(node.Id, modelId, node.LinkedNodes);
                }
                return LinkedNodes;
            }
        }

        public void GenerateDtoResponse()
        {
            ParentReference = null;
            LinkedNodes ??= new List<Node>();
            LinkedNodes = MapLinkedNodes(LinkedNodes);

            static List<Node> MapLinkedNodes(List<Node> LinkedNodes)
            {
                foreach (var node in LinkedNodes)
                {
                    node.ParentReference = null;
                    node.LinkedNodes = MapLinkedNodes(node.LinkedNodes);
                }
                return LinkedNodes;
            }
        }
    }
}
