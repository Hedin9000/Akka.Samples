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
        Console.WriteLine(Context.Self.Path);
        var pingNodeRef = Context.ActorOf(Props.Create<EchoNodeRootActor>().WithRouter(FromConfig.Instance),"pingNode");
        var schedulerNodeRef = Context.ActorOf(Props.Create<SenderNodeRootActor>().WithRouter(FromConfig.Instance),"schedulerNode");

        Receive<PingMessage>(message => Console.WriteLine(message.Text));

    }
}