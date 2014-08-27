using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PKB.Infrastructure.Commanding;
using PKB.Infrastructure.Eventing;
using PKB.Infrastructure.Messaging;
using PKB.Utility;

namespace PKB.Infrastructure
{
    public class Subscriber
    {
        private static readonly CallMethodOnSubscriber CallSubscribeMessageHandler =
            new CallMethodOnSubscriber(typeof(IMessageHandler<>), typeof(MessageSubscriber).GetMethod("Subscribe"));

        private static readonly CallMethodOnSubscriber CallUnsubscribeMessageHandler =
            new CallMethodOnSubscriber(typeof(IMessageHandler<>), typeof(MessageSubscriber).GetMethod("Unsubscribe"));

        private static readonly CallMethodOnSubscriber CallSubscribeEventHandler =
           new CallMethodOnSubscriber(typeof(IEventHandler<>), typeof(EventSubscriber).GetMethod("Subscribe"));

        private static readonly CallMethodOnSubscriber CallUnsubscribeEventHandler =
            new CallMethodOnSubscriber(typeof(IEventHandler<>), typeof(EventSubscriber).GetMethod("Unsubscribe"));

        private static readonly CallMethodOnSubscriber CallSubscribeCommandHandler =
           new CallMethodOnSubscriber(typeof(ICommandHandler<>), typeof(CommandSubscriber).GetMethod("Subscribe"));

        private static readonly CallMethodOnSubscriber CallUnsubscribeCommandHandler =
            new CallMethodOnSubscriber(typeof(ICommandHandler<>), typeof(CommandSubscriber).GetMethod("Unsubscribe"));


        private readonly MessageSubscriber _messageSubscriber;
        private readonly EventSubscriber _eventSubscriber;
        private readonly CommandSubscriber _commandSubscriber;


        public Subscriber(MessageSubscriber messageSubscriber, EventSubscriber eventSubscriber, CommandSubscriber commandSubscriber)
        {
            _messageSubscriber = messageSubscriber;
            _eventSubscriber = eventSubscriber;
            _commandSubscriber = commandSubscriber;
        }

        public void Subscribe(object handler)
        {
            if (!(handler is IHandler))
                return;

            HandlerInterfaceTypesFor(handler).ForEach(type =>
                {
                    CallSubscribeMessageHandler.CallMethod(_messageSubscriber, handler, type);
                    CallSubscribeEventHandler.CallMethod(_eventSubscriber, handler, type);
                    CallSubscribeCommandHandler.CallMethod(_commandSubscriber, handler, type);
                });
        }

        public void Unsubscribe(object handler)
        {
            if (!(handler is IHandler))
                return;

            HandlerInterfaceTypesFor(handler).ForEach(type =>
                {
                    CallUnsubscribeMessageHandler.CallMethod(_messageSubscriber, handler, type);
                    CallUnsubscribeEventHandler.CallMethod(_eventSubscriber, handler, type);
                    CallUnsubscribeCommandHandler.CallMethod(_commandSubscriber, handler, type);
                });
        }

        private static IEnumerable<Type> HandlerInterfaceTypesFor(object handler)
        {
            return handler
                .GetType()
                .GetInterfaces()
                .Where(x => x.IsGenericType);
        }

        private class CallMethodOnSubscriber
        {
            private readonly Type _handlerGenericType;
            private readonly MethodInfo _callMethod;

            public CallMethodOnSubscriber(
                Type handlerGenericType,
                MethodInfo callMethod)
            {
                _handlerGenericType = handlerGenericType;
                _callMethod = callMethod;
            }

            public void CallMethod(object subscriber, object handler, Type handlerType)
            {

                if (handlerType.GetGenericTypeDefinition() != _handlerGenericType)
                    return;

                _callMethod.MakeGenericMethod(handlerType.GetGenericArguments().Single()).Invoke(subscriber, new[] { handler });
            }
        }



    }
}
