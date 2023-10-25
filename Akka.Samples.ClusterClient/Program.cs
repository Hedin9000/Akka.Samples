using System.Collections.Immutable;
using Akka.Actor;
using Akka.Cluster.Tools.Client;
using Akka.Samples.Cluster.Common;

var actorSystem = BootstrapNode.InitAkka();
var immutableHashSet = ImmutableHashSet.Create<ActorPath>(ActorPath.Parse("akka.tcp://ClusterSystem@localhost:8081/system/receptionist"));

var settings = ClusterClientSettings.Create(actorSystem).WithInitialContacts(immutableHashSet);

// Актор, представляющий собой доступ к кластеру. Если нужно отправить запрос в кластер - то спрашивать с него
var actorClusterClientRef = actorSystem.ActorOf(ClusterClient.Props(settings), "client");

while (true)
{
    var task = actorClusterClientRef.Ask<PingMessage>(
        // путь до актора, без указания физического адреса.
        new ClusterClient.Send("/user/cluster/manager/pingNode/c1/pingService", 
            new PingMessage("CLIENT ECHO")));
    
    var pingMessage = task.ConfigureAwait(false).GetAwaiter().GetResult();
    
    Console.WriteLine($"ASK:{pingMessage.Text}");
    Task.Delay(1000).ConfigureAwait(false).GetAwaiter().GetResult();
}
Console.ReadLine();