namespace WebPareser.Nodes
{
    public interface INode
    {
        public string TagName { get; set; }

        public string Content { get; set; }

        public string InnerContent { get; set; }

        public Dictionary<string, string> Attributes { get; set; }

        public List<INode> InnerNodes { get; set; }
    }
}