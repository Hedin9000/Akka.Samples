using System.Reflection;
using Akka.Actor;
using Akka.Configuration;
using Jaeger;
using Jaeger.Reporters;
using Jaeger.Samplers;
using Jaeger.Senders;
using Jaeger.Senders.Thrift;
using Microsoft.Extensions.Logging;
using OpenTracing.Util;

namespace Akka.Samples.Cluster.Common;

public static class BootstrapNode
{
    public static ActorSystem InitAkka()
    {
        using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
        var logger = loggerFactory.CreateLogger("Main");
        
        var senderConfig = new Jaeger.Configuration.SenderConfiguration(loggerFactory)
            .WithAgentHost("localhost")
            .WithAgentPort(5775)
            .WithSenderResolver(new SenderResolver(loggerFactory).RegisterSenderFactory<ThriftSenderFactory>());
        var reporter = new RemoteReporter.Builder()
            .WithLoggerFactory(loggerFactory)
            .WithSender(senderConfig.GetSender())
            .Build();
        var sampler = new ConstSampler(true);
        var tracer = new Tracer.Builder(Assembly.GetCallingAssembly().GetName().Name)
            .WithLoggerFactory(loggerFactory)
            .WithReporter(reporter)
            .WithSampler(sampler)
            .Build();
        GlobalTracer.Register(tracer);
        var bootstrapSetup =BootstrapSetup.Create().WithConfig(ConfigurationFactory.ParseString(File.ReadAllText("akka.conf")));
        return ActorSystem.Create("ClusterSystem",bootstrapSetup);
    }
}