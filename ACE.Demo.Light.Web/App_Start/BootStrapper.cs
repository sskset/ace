﻿using ACE.Demo.Model;
using ACE.Demo.Model.Accounts;
using ACE.Demo.Model.Investments;
using ACE.Demo.Model.Projects;
using ACE.Demo.Model.Write.AccountActivities;
using ACE.Demo.Model.Write.Messages;
using ACE.Demo.Repositories;
using ACE.Demo.Repositories.Write;
using ACE;
using Grit.Sequence;
using Grit.Sequence.Repository.MySql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace ACE.Demo.Light.Web
{
    public static class BootStrapper
    {
        public static Autofac.IContainer Container { get; private set; }

        private static ContainerBuilder _builder;

        public static void BootStrap()
        {
            _builder = new ContainerBuilder();
            Container = _builder.Build();

            _builder = new ContainerBuilder();
            BindFrameworkObjects();
            BindBusinessObjects();
            _builder.Update(Container);
        }

        private static void BindFrameworkObjects()
        {
            _builder.RegisterType<ACE.Loggers.Log4NetBusLogger>().As<ACE.Loggers.IBusLogger>().SingleInstance();
            _builder.RegisterType<CommandHandlerFactory>().As<ICommandHandlerFactory>()
                .SingleInstance()
                .WithParameter(new TypedParameter(typeof(Autofac.IContainer), Container))
                .WithParameter(Constants.ParamCommandAssmblies, new string[] { "ACE.Demo.ContractsFS" })
                .WithParameter(Constants.ParamHandlerAssmblies, new string[] { "ACE.Demo.Model.Write" });
            _builder.RegisterType<CommandBus>().As<ICommandBus>().SingleInstance();

            _builder.RegisterType<EventHandlerFactory>().As<IEventHandlerFactory>()
                .SingleInstance()
                .WithParameter(new TypedParameter(typeof(Autofac.IContainer), Container))
                .WithParameter(Constants.ParamEventAssmblies, new string[] { "ACE.Demo.ContractsFS" })
                .WithParameter(Constants.ParamHandlerAssmblies, new string[] { "ACE.Demo.Model.Write" });
            _builder.RegisterType<EventBus>().As<IEventBus>().InstancePerLifetimeScope();

            _builder.RegisterType<ActionHandlerFactory>().As<IActionHandlerFactory>()
                .SingleInstance()
                .WithParameter(new TypedParameter(typeof(Autofac.IContainer), Container))
                .WithParameter(Constants.ParamActionAssmblies, new string[] { "ACE.Demo.ContractsFS" })
                .WithParameter(Constants.ParamHandlerAssmblies, new string[] { "ACE.Demo.Application" });
            _builder.RegisterType<ActionStation>().As<IActionStation>()
                .SingleInstance()
                .WithParameter(new TypedParameter(typeof(Autofac.IContainer), Container));
        }

        private static void BindBusinessObjects()
        {
            _builder.RegisterType<SequenceRepository>().As<ISequenceRepository>().SingleInstance();
            _builder.RegisterType<SequenceService>().As<ISequenceService>().SingleInstance();

            _builder.RegisterType<InvestmentRepository>().As<IInvestmentRepository>().SingleInstance();
            _builder.RegisterType<InvestmentWriteRepository>().As<IInvestmentWriteRepository>().SingleInstance();
            _builder.RegisterType<InvestmentService>().As<IInvestmentService>().SingleInstance();
            _builder.RegisterType<ProjectRepository>().As<IProjectRepository>().SingleInstance();
            _builder.RegisterType<ProjectWriteRepository>().As<IProjectWriteRepository>().SingleInstance();
            _builder.RegisterType<ProjectService>().As<IProjectService>().SingleInstance();
            _builder.RegisterType<AccountRepository>().As<IAccountRepository>().SingleInstance();
            _builder.RegisterType<AccountWriteRepository>().As<IAccountWriteRepository>().SingleInstance();
            _builder.RegisterType<AccountService>().As<IAccountService>().SingleInstance();
            _builder.RegisterType<MessageWriteRepository>().As<IMessageWriteRepository>().SingleInstance();
            _builder.RegisterType<AccountActivityWriteRepository>().As<IAccountActivityWriteRepository>().SingleInstance();
        }

        public static void Dispose()
        {
            if (Container != null)
            {
                Container.Dispose();
            }
        }
    }
}
