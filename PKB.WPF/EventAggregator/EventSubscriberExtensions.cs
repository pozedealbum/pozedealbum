using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PKB.Utility;
namespace PKB.WPF.EventAggregator
{
    public static class EventSubscriberExtensions
    {
        private static readonly MethodInfo AddMethod = typeof(IEventSubscriber).GetMethod("Add");
        private static readonly MethodInfo RemoveMethod = typeof(IEventSubscriber).GetMethod("Remove");

        static EventSubscriberExtensions()
        {

        }

        public static void Subscribe(this IEventSubscriber subscriber, object handler)
        {
            CallMethod(AddMethod, subscriber, handler);
        }

        public static void Unsubscribe(this IEventSubscriber subscriber, object handler)
        {
            CallMethod(RemoveMethod, subscriber, handler);
        }

        private static void CallMethod(MethodInfo methodInfo, IEventSubscriber subscriber, object handler)
        {
            if (!(handler is IHandle))
                return;

            handler
                .GetType()
                .GetInterfaces()
                .Where(type =>
                    type.IsGenericType &&
                    type.GetGenericTypeDefinition() == typeof(IHandle<>))
                .Select(type => methodInfo.MakeGenericMethod(type.GetGenericArguments()[0]))
                .ForEach(method => method.Invoke(subscriber, new[] { handler }));
        }

    }
}
