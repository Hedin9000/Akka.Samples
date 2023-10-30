using Akka.Actor;
using Akka.Cluster.Tools.Client;
using Akka.Samples.Cluster.Common;

namespace Akka.Samples.Cluster.Actors.RootActors;

public class ClientRouterRootActor : BaseRootActor
{

    public ClientRouterRootActor()
    {
        var actorClientRequestRef = Context.ActorOf(Props.Create(()=> new ClientRequestActor(DeployPath)),"clientRequest");
        ClusterClientReceptionist.Get(Context.System).RegisterService(actorClientRequestRef);


    }
}