using Akka.Routing;

namespace Akka.Samples.Cluster.Common;

public class PingMessage : IConsistentHashable
{
    public string Text { get; init; }
    public object ConsistentHashKey => Text.GetHashCode();

    public PingMessage(string text)
    {
        Text = text;
    }
}