using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using JetBrains.Annotations;

namespace Vostok.Commons.Testing.Observable
{
    [PublicAPI]
    internal class TestObserver<T> : IObserver<T>
    {
        private readonly object lockObject = new object();
        private readonly List<Notification<T>> messages = new List<Notification<T>>();
        public IList<Notification<T>> Messages
        {
            get
            {
                lock (lockObject)
                    return messages.ToList();
            }
        }

        public IList<T> Values => Messages.Select(message => message.Value).ToList();

        public void OnNext(T value)
        {
            Add(Notification.CreateOnNext(value));
        }

        public void OnError(Exception exception)
        {
            Add(Notification.CreateOnError<T>(exception));
        }

        public void OnCompleted()
        {
            Add(Notification.CreateOnCompleted<T>());
        }

        private void Add(Notification<T> message)
        {
            lock (lockObject)
                messages.Add(message);
        }
    }
}