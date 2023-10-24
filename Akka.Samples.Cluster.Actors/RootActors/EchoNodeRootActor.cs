using Akka.Actor;
using Akka.Samples.Cluster.Common;

namespace Akka.Samples.Cluster.Actors.RootActors;

public class EchoNodeRootActor : BaseRootActor
{


    public EchoNodeRootActor()
    {
         Receive<PingMessage>((s => Console.WriteLine($"Work:{s.Text}")));
        //TODO: создать как дочерний актор
       // Context.ActorOf(Props.Create(()=> new PingActor(DeployPath)),"pingService");
       
    }
}