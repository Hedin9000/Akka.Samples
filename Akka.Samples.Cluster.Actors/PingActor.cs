using Akka.Actor;
using Akka.Samples.Cluster.Common;
using DigTP.Sdk.AkkaMediator.Common.ForTestOnly;
using DigTP.Sdk.Tracing.Common;
using OpenTracing.Util;

namespace Akka.Samples.Cluster.Actors;

public class PingActor : ReceiveActor
{
    private int _value = 0;
    public PingActor()
    {
        Receive<PingAkkaRequest>(message =>
        {
            using var span = GlobalTracer.Instance
                .BuildSpan(nameof(PingActor))
                .AsChildOf(message.ToSpanContext())
                .StartActive(true);
            Context.Sender.Tell($"[PONG-{GetNextPong()}]:{message}");
        });
        Receive<PingMessage>(message =>
        {
            //Console.WriteLine($"[PING-{GetNextPong()}]:{message.Text}");
            Context.Sender.Tell(new PingMessage($"[PONG-{GetNextPong()}]:{message.Text}"));
        });
        Receive<string>(message =>
        {
            //Console.WriteLine($"[PING-{GetNextPong()}]:{message.Text}");
            Context.Sender.Tell( $"[PONG-{GetNextPong()}]:{message}");
        });
    }
    private int GetNextPong() => _value++;
    
}