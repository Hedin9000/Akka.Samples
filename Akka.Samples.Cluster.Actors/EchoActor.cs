using Akka.Actor;
using Akka.Samples.Cluster.Common;

namespace Akka.Samples.Cluster.Actors;

public class EchoActor : ReceiveActor
{
    public EchoActor(string deployPath)
    {
        var c = Context;
        Receive<PingMessage>((s =>
        {
            Console.WriteLine($"[ECHO]:{s.Text}");
            // var actorRef = Context.ActorSelection($"{deployPath}/shedulerSender");
            // actorRef.Tell(new PingMessage(){Text = $"PONG:{s.Text}"});
        }));
    }
}