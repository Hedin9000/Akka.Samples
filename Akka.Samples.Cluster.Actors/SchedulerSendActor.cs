using Akka.Actor;
using Akka.Samples.Cluster.Common;

namespace Akka.Samples.Cluster.Actors;

public class SchedulerSendActor :ReceiveActor
{
    public SchedulerSendActor(string deployPath)
    {
        var remoteEcho1 = Context.ActorSelection($"{deployPath}/pingNode/c1/echoService");
        //     .ResolveOne(TimeSpan.FromSeconds(30))
        //     .ConfigureAwait(false)
        //     .GetAwaiter()
        //     .GetResult();

    

        Context.System.Scheduler.ScheduleTellRepeatedlyCancelable(
            initialDelay: TimeSpan.FromSeconds(5),
            interval: TimeSpan.FromSeconds(1),
            receiver: remoteEcho1,
            message: new PingMessage() { Text = "TEST CONTENT" },
            sender: Self
        );
    }
}