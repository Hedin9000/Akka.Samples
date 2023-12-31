﻿// See https://aka.ms/new-console-template for more information

using Akka.Actor;
using Akka.Cluster.Tools.Client;
using Akka.Cluster.Tools.Singleton;
using Akka.Configuration;
using Akka.Samples.Cluster.Common;
using Akka.Samples.Cluster.Seed;

Console.WriteLine("Hello, World!");


var actorSystem = BootstrapNode.InitAkka();


var clusterSingletonManager = ClusterSingletonManager.Props(
    singletonProps: Props.Create<SeedNodeRootActor>(),
    terminationMessage: PoisonPill.Instance,
    settings: ClusterSingletonManagerSettings.Create(actorSystem)
        .WithSingletonName("manager")
        .WithRole("seed"));

var singletonRef = actorSystem.ActorOf(clusterSingletonManager, name: "cluster");


ClusterClientReceptionist.Get(actorSystem).RegisterService(singletonRef);





Console.ReadLine();