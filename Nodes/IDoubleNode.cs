namespace WebPareser.Nodes
{
    public interface IDoubleNode : INode
    {
        public string OpenTag { get; set;}

        public string CloseTag { get; set; }
    }
}