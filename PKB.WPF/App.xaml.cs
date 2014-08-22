using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows;
using Autofac;
using Autofac.Core;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Mvvm;
using MVPVM;
using PKB.WPF.Common;
using PKB.WPF.Common.Interfaces;
using PKB.WPF.EventAggregator;
using PKB.WPF.Views;
using PKB.WPF.Views.SectionTree.EditableSection;
using PKB.WPF.Views.Main;
using PKB.WPF.Views.SectionTree;

namespace PKB.WPF
{


    public partial class App : Application
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

            builder.RegisterType<EventPublisher>().As<IEventPublisher>();
            builder.RegisterGeneric(typeof(EventPublisher<>)).As(typeof(IEventPublisher<>));
            builder.RegisterType<DictonaryHandlersManager>().As<IEventSubscriber>().As<IHandlersManager>().SingleInstance();

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
            if (!(x.Instance is IHandle))
                return;

            var eventSubscriber = x.Context.Resolve<IEventSubscriber>();

            ((IActivate)x.Instance).Activated += (_, __) =>
               eventSubscriber.Subscribe(x.Instance);

            ((IDeactivate)x.Instance).Deactivated += (_, __) =>
                eventSubscriber.Unsubscribe(x.Instance);
        }
    }
}
