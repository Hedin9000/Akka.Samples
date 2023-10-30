using Akka.Actor;
using Akka.Cluster.Tools.Client;
using Akka.Routing;
using Akka.Samples.Cluster.Actors.RootActors;
using Akka.Samples.Cluster.Common;

namespace Akka.Samples.Cluster.Seed;

public class SeedNodeRootActor : ReceiveActor
{
    public SeedNodeRootActor()
    {
        var pingNodeRef = Context.ActorOf(Props.Create<EchoNodeRootActor>().WithRouter(FromConfig.Instance),"pingNode");
        var schedulerNodeRef = Context.ActorOf(Props.Create<SenderNodeRootActor>().WithRouter(FromConfig.Instance),"schedulerNode");
        var clientRouterNodeRef = Context.ActorOf(Props.Create<ClientRouterRootActor>().WithRouter(FromConfig.Instance),"clientRouterNode");
    }
}