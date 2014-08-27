using Autofac;
using Autofac.Core;
using MVPVM;
using PKB.Application.Handlers;
using PKB.DomainModel.Model;
using PKB.DomainModel.Repositories;
using PKB.Infrastructure;
using PKB.Infrastructure.Commanding;
using PKB.Infrastructure.Eventing;
using PKB.Infrastructure.Messaging;
using PKB.Utility;
using PKB.WPF.Common;
using PKB.WPF.Common.Interfaces;
using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace PKB.WPF
{
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var builder = new ContainerBuilder();

            var executingAssembly = Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(executingAssembly)
                .Where(t => t.Name.EndsWith("Presenter"))
                .AsSelf()
                .OnActivating(SetAutomaticSubscribeAndUnsubscribe);

            builder.RegisterAssemblyTypes(executingAssembly)
               .Where(t => t.Name.EndsWith("Presenter"))
               .As(t => t.GetInterfaces().Where(s => s.Name.EndsWith("Controller")))
               .OnActivating(t =>
               {
                   SetAutomaticSubscribeAndUnsubscribe(t);
                   SetView(t);
               });


            var appAssembly = typeof(AddNewSectionHandler).Assembly;

            executingAssembly.GetReferencedAssemblies()
                .Where(x => x.Name.StartsWith("PKB."))
                .ForEach(x =>
                {

                    Assembly assembly = Assembly.Load(x);
                    builder.RegisterAssemblyTypes(assembly)
                        .Where(t => typeof (IHandler).IsAssignableFrom(t))
                        .As(t => ServiceMapping(t))
                        .AsSelf();
                });



            builder.RegisterType<PublishEventOnMessageBus>().As<IEventPublisher>();
            builder.RegisterGeneric(typeof(PublishEventOnMessageBus<>)).As(typeof(IEventPublisher<>));

            builder.RegisterType<PublishCommandOnMessageBus>().As<ICommandPublisher>();
            builder.RegisterGeneric(typeof(PublishCommandOnMessageBus<>)).As(typeof(ICommandPublisher<>));

            builder.RegisterType<PublishMessageOnMessageBus>().As<IMessagePublisher>();
            builder.RegisterGeneric(typeof(PublishMessageOnMessageBus<>)).As(typeof(IMessagePublisher<>));

            builder.RegisterType<ResourceRepository>().As<IResourceRepository>();

            builder.RegisterType<MessageBus>().As<IMessageBus>().SingleInstance();
            builder.RegisterType<MessageSubscriber>();
            builder.RegisterType<EventSubscriber>();
            builder.RegisterType<CommandSubscriber>();
            builder.RegisterType<Subscriber>();

            var container = builder.Build();

            PresenterLocationProvider.PresenterFactory = type => (IPresenter)container.Resolve(type);

            var subscriber = container.Resolve<Subscriber>();

            executingAssembly.GetReferencedAssemblies()
                .Where(x => x.Name.StartsWith("PKB."))
                .ForEach(x => SubscriberHandlers(container, x));
        }

        private static Type ServiceMapping(Type t)
        {
            var result = t.GetInterfaces().Single(x => x != typeof(IHandler) && typeof(IHandler).IsAssignableFrom(x));
            return result;
        }

        private void SubscriberHandlers(IContainer container, AssemblyName assemblyName)
        {
            Assembly assembly = Assembly.Load(assemblyName);
            var subscriber = container.Resolve<Subscriber>();

            assembly.GetExportedTypes()
                     .Where(t => typeof(IHandler).IsAssignableFrom(t) &&
                         !t.IsInterface &&
                         !t.IsAbstract)
                     .Select(container.Resolve)
                     .ForEach(subscriber.Subscribe);
        }

        private static void SetView(IActivatingEventArgs<object> t)
        {
            var type = t.Component.Activator.LimitType;

            var baseName = type.FullName.Remove(type.FullName.Length - "Presenter".Length);
            var viewAssemblyName = type.GetTypeInfo().Assembly.FullName;
            var viewName = string.Format(
                CultureInfo.InvariantCulture, "{0}View, {1}",
                baseName, viewAssemblyName);

            var viewType = Type.GetType(viewName);
            if (viewType == null)
                throw new InvalidOperationException();

            var view = Activator.CreateInstance(viewType);
            var presenterAware = (IPresenterAware)view;
            if (presenterAware.Presenter != null)
                throw new InvalidOperationException("AutoWirePresenter must be false");

            ((IPresenterAware)view).Presenter = (IPresenter)t.Instance;
        }

        private static void SetAutomaticSubscribeAndUnsubscribe(IActivatingEventArgs<object> x)
        {
            if (!(x.Instance is IHandler))
                return;

            var subscriber = x.Context.Resolve<Subscriber>();

            ((IActivate)x.Instance).Activated += (_, __) =>
               subscriber.Subscribe(x.Instance);

            ((IDeactivate)x.Instance).Deactivated += (_, __) =>
                subscriber.Unsubscribe(x.Instance);
        }
    }
}