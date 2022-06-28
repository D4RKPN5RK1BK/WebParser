using System.Linq;
using System.Text.RegularExpressions;

namespace WebPareser.Nodes
{
    public class DoubleNode : IDoubleNode
    {
        public DoubleNode(string name, string content, Dictionary<string, string> attributes)
        {
            TagName = name;
            Attributes = attributes;
            Content = content;
        }

        private string _content;
        public string Content
        {
            get { return _content; }
            set
            {
                _content = value;
                InnerContent = new Regex(@$"^{OpenTag}(\S*){CloseTag}$").Match(_content).Groups[0]?.Value ?? String.Empty;
            }
        }
        public string InnerContent { get; set; }
        public string OpenTag { get; set; }
        public string CloseTag { get; set; }
        public Dictionary<string, string> Attributes { get; set; }
        public List<INode> InnerNodes { get; set; }

        private string _tagName;
        public string TagName
        {
            get { return _tagName; }
            set
            {
                _tagName = value;
                OpenTag = $"<{_tagName}>";
                CloseTag = $"</{_tagName}>";
            }
        }
    }
}