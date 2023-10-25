using Akka.Actor;
using Akka.Cluster.Tools.Client;
using Akka.Samples.Cluster.Common;

namespace Akka.Samples.Cluster.Actors.RootActors;

public class EchoNodeRootActor : BaseRootActor
{
    public EchoNodeRootActor()
    {
         var actorEchoRef = Context.ActorOf(Props.Create(()=> new EchoActor()),"echoService");
         Context.ActorOf(Props.Create(()=> new PingActor()),"pingService");
             // Регистрация актора, как доступного из клиентов
         ClusterClientReceptionist.Get(Context.System).RegisterService(actorEchoRef);
    }
}