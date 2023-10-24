using Akka.Actor;
using Akka.Samples.Cluster.Common;

namespace Akka.Samples.Cluster.Actors;

public class EchoActor : ReceiveActor
{
    public EchoActor()
    {
        var c = Context;
        Receive<PingMessage>((s =>
        {
            Console.WriteLine($"[ECHO]:{s.Text}");
        }));
    }


}