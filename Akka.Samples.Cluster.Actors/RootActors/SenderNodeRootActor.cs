using Akka.Actor;
using Akka.Samples.Cluster.Common;

namespace Akka.Samples.Cluster.Actors.RootActors;

public class SenderNodeRootActor : BaseRootActor
{
    public SenderNodeRootActor()
    {
        // локальный актор, фактически выполняющий работу
        Context.ActorOf(Props.Create(()=> new SchedulerSendActor(DeployPath)),"shedulerSender");
    }

}