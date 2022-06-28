namespace WebPareser.Nodes
{
    public class SingleNode : ISingleNode
    {
        public string Content { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string InnerContent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Dictionary<string, string> Attributes { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string TagName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<INode> InnerNodes { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}