using Akka.Actor;
using Akka.Routing;
using Akka.Samples.Cluster.Actors.RootActors;

namespace Akka.Samples.Cluster.Seed;

public class SeedNodeRootActor : ReceiveActor
{

    public SeedNodeRootActor()
    {
        
        Context.ActorOf(Props.Create<EchoNodeRootActor>().WithRouter(FromConfig.Instance),"pingNode");
        Context.ActorOf(Props.Create<SenderNodeRootActor>().WithRouter(FromConfig.Instance),"schedulerNode");

    }
}