using Akka.Actor;
using Akka.Samples.Cluster.Common;

namespace Akka.Samples.Cluster.Actors;

public class PingActor : ReceiveActor
{
    public PingActor(string deployPath)
    {
        var c = Context;
        Receive<PingMessage>((s =>
        {
            Console.WriteLine($"Incoming ping:{s.Text}");
            // var actorRef = Context.ActorSelection($"{deployPath}/shedulerSender");
            // actorRef.Tell(new PingMessage(){Text = $"PONG:{s.Text}"});
        }));
    }
}