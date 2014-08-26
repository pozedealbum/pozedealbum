using Autofac;
using Autofac.Core;
using MVPVM;
using PKB.Infrastructure;
using PKB.Infrastructure.Commanding;
using PKB.Infrastructure.Eventing;
using PKB.Infrastructure.Messaging;
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

            builder.RegisterType<PublishEventOnMessageBus>().As<IEventPublisher>();
            builder.RegisterGeneric(typeof(PublishEventOnMessageBus<>)).As(typeof(IEventPublisher<>));

            builder.RegisterType<PublishCommandOnMessageBus>().As<ICommandPublisher>();
            builder.RegisterGeneric(typeof(PublishCommandOnMessageBus<>)).As(typeof(ICommandPublisher<>));

            builder.RegisterType<PublishMessageOnMessageBus>().As<IMessagePublisher>();
            builder.RegisterGeneric(typeof(PublishMessageOnMessageBus<>)).As(typeof(IMessagePublisher<>));

            builder.RegisterType<IMessageBus>().As<MessageBus>().SingleInstance();

            builder.RegisterType<Subscriber>();

            var container = builder.Build();

            PresenterLocationProvider.PresenterFactory = type => (IPresenter)container.Resolve(type);
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

            var eventSubscriber = x.Context.Resolve<Subscriber>();

            ((IActivate)x.Instance).Activated += (_, __) =>
               eventSubscriber.Subscribe(x.Instance);

            ((IDeactivate)x.Instance).Deactivated += (_, __) =>
                eventSubscriber.Unsubscribe(x.Instance);
        }
    }
}