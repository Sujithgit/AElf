﻿using AElf.Database.Config;
using AElf.Kernel.KernelAccount;
using AElf.Kernel.Modules.AutofacModule;
using Autofac;
using Xunit;
using Xunit.Abstractions;
using Xunit.Frameworks.Autofac;

[assembly: TestFramework("AElf.Runtime.CSharp.Tests.ConfigureTestFramework", "AElf.Runtime.CSharp.Tests")]

namespace AElf.Runtime.CSharp.Tests
{
    public class ConfigureTestFramework : AutofacTestFramework
    {
        public ConfigureTestFramework(IMessageSink diagnosticMessageSink)
            : base(diagnosticMessageSink)
        {
        }

        protected override void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new MainModule());
            builder.RegisterModule(new DatabaseModule(new DatabaseConfig()));
            builder.RegisterModule(new LoggerModule());
            
            var smartContractRunnerFactory = new SmartContractRunnerFactory();
            var runner = new SmartContractRunner("../../../../AElf.Runtime.CSharp.Tests.TestContract/bin/Debug/netstandard2.0/");
            smartContractRunnerFactory.AddRunner(0, runner);
            builder.RegisterInstance(smartContractRunnerFactory).As<ISmartContractRunnerFactory>().SingleInstance();
            // configure your container
            // e.g. builder.RegisterModule<TestOverrideModule>();
        }
    }
}