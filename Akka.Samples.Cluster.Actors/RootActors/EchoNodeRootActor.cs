using Akka.Actor;
using Akka.Samples.Cluster.Common;

namespace Akka.Samples.Cluster.Actors.RootActors;

public class EchoNodeRootActor : BaseRootActor
{
    public EchoNodeRootActor()
    {
         Context.ActorOf(Props.Create(()=> new EchoActor(DeployPath)),"echoService");
    }
}