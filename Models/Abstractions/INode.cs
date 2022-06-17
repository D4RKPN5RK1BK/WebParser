namespace WebPareser.Models.Abstractions
{
    interface INode
    {
        public IEnumerable<INode> Children { get; set; }
    }
}