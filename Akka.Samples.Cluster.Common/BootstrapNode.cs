using Akka.Actor;
using Akka.Configuration;

namespace Akka.Samples.Cluster.Common;

public static class BootstrapNode
{
    public static ActorSystem InitAkka()
    {
        var bootstrapSetup =BootstrapSetup.Create().WithConfig(ConfigurationFactory.ParseString(File.ReadAllText("akka.conf")));
        return ActorSystem.Create("ClusterSystem",bootstrapSetup);
    }
}