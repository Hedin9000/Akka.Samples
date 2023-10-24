using Akka.Actor;

namespace Akka.Samples.Cluster.Common;

public abstract class BaseRootActor : ReceiveActor
{
    
    protected string DeployPath { get; set; }

    protected BaseRootActor()
    {
        DeployPath = GetDeployPath(Context);
    }
    private static string GetDeployPath(IActorContext context)
    {
        var parentActorPath = context.Parent.Path.ToString()!;
        return parentActorPath[..(parentActorPath!.LastIndexOf('/') + 1)];
    }


}