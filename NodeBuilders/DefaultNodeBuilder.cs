using Microsoft.Extensions.Logging;

namespace WebParser.NodeBuilders
{
    public class DefaultNodeBuilder
    {
        public string Source { get; private set; }
        public DefaultNodeBuilder(string source) {
            Source = source;
        }

        
    }
}