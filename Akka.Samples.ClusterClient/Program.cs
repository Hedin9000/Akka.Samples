using System.Collections.Immutable;
using Akka.Actor;
using Akka.Cluster.Tools.Client;
using Akka.Routing;
using Akka.Samples.Cluster.Common;
using Akka.Samples.ClusterClient;

var actorSystem = BootstrapNode.InitAkka();
//"akka.tcp://ClusterSystem@localhost:8081"
var immutableHashSet = ImmutableHashSet.Create<ActorPath>(ActorPath.Parse("akka.tcp://ClusterSystem@localhost:8081/system/receptionist"));



var settings = ClusterClientSettings.Create(actorSystem).WithInitialContacts(immutableHashSet);

// Актор, представляющий собой доступ к кластеру. Если нужно отправить запрос в кластер - то спрашивать с него
var actorClusterClientRef = actorSystem.ActorOf(ClusterClient.Props(settings), "client");

while (true)
{ 
    actorClusterClientRef.Tell(
        new ClusterClient.Send("/user/cluster/manager/pingNode/c1/echoService", 
            new PingMessage("CLIENT ECHO")));
    Task.Delay(1000).ConfigureAwait(false).GetAwaiter().GetResult();
}
Console.ReadLine();