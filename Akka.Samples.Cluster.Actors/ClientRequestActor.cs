using Akka.Actor;

namespace Akka.Samples.Cluster.Actors;

public class ClientRequestActor : ReceiveActor
{
    public ClientRequestActor(string deployPath)
    {
        Receive<string>(message =>
        {
            var echoService = Context.ActorSelection($"{deployPath}/pingNode/c1/pingService");
            echoService.Tell(message, Context.Sender);

        });
    }
}