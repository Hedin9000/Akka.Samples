using Akka.Actor;
using Akka.Samples.Cluster.Common;

namespace Akka.Samples.Cluster.Actors;

public class PingActor : ReceiveActor
{
    private int _value = 0;
    public PingActor()
    {
        Receive<PingMessage>(message =>
        {
            //Console.WriteLine($"[PING-{GetNextPong()}]:{message.Text}");
            Context.Sender.Tell(new PingMessage($"[PONG-{GetNextPong()}]:{message.Text}"));
        });
    }
    private int GetNextPong() => _value++;
    
}