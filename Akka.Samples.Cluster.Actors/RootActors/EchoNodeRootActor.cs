using Akka.Actor;
using Akka.Cluster.Tools.Client;
using Akka.Samples.Cluster.Common;

namespace Akka.Samples.Cluster.Actors.RootActors;

public class EchoNodeRootActor : BaseRootActor
{
    public EchoNodeRootActor()
    {
         var actorEchoRef = Context.ActorOf(Props.Create(()=> new EchoActor()),"echoService");
         var actorPingRef = Context.ActorOf(Props.Create(()=> new PingActor()),"pingService");
         var actorClientRequestRef = Context.ActorOf(Props.Create(()=> new ClientRequestActor(DeployPath)),"clientRequest");
             // Регистрация актора, как доступного из клиентов
         ClusterClientReceptionist.Get(Context.System).RegisterService(actorEchoRef);
         ClusterClientReceptionist.Get(Context.System).RegisterService(actorPingRef);
         ClusterClientReceptionist.Get(Context.System).RegisterService(actorClientRequestRef);
    }
}