using Akka.Actor;
using Akka.Samples.Cluster.Common;
using DigTP.Sdk.AkkaMediator.Common.ForTestOnly;
using DigTP.Sdk.Tracing.Common;
using OpenTracing.Util;

namespace Akka.Samples.Cluster.Actors;

public class ClientRequestActor : ReceiveActor
{
    public ClientRequestActor(string deployPath)
    {
        var c = Context;
        Receive<PingAkkaRequest>(message =>
        {
            using var span = GlobalTracer.Instance
                .BuildSpan(nameof(ClientRequestActor))
                .AsChildOf(message.ToSpanContext())
                .StartActive(true);
            
            var echoService = Context.ActorSelection($"{deployPath}/pingNode/c1/pingService");
            echoService.Tell(message, Context.Sender);
        });
        Receive<PingMessage>(message =>
        {
            var echoService = Context.ActorSelection($"{deployPath}/pingNode/c1/pingService");
            echoService.Tell(message, Context.Sender);
        });
        Receive<string>(message =>
        {
            var echoService = Context.ActorSelection($"{deployPath}/pingNode/c1/pingService");
            echoService.Tell(message, Context.Sender);
        });
    }
}