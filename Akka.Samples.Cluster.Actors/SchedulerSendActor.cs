using Akka.Actor;
using Akka.Samples.Cluster.Common;

namespace Akka.Samples.Cluster.Actors;

public class SchedulerSendActor :ReceiveActor
{
    public SchedulerSendActor(string deployPath)
    {
        // var echoService = Context.ActorSelection($"{deployPath}/pingNode/c1/echoService");

        Receive<PingMessage>(message =>
        {
            Console.WriteLine(message.Text);
        });
        
        var echoService = Context.ActorSelection($"{deployPath}/pingNode/c1/pingService");

        Context.System.Scheduler.ScheduleTellRepeatedlyCancelable(
            initialDelay: TimeSpan.FromSeconds(5),
            interval: TimeSpan.FromSeconds(1),
            receiver: echoService,
            message: new PingMessage("TEST CONTENT"),
            sender: Self
        );
    }
}