using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using FluentAssertions;
using JetBrains.Annotations;

namespace Vostok.Commons.Testing.Observable
{
    [PublicAPI]
    internal static class ObservableAssertions
    {
        public static void ShouldStartWithIn<T>(this IObservable<T> observable, TimeSpan timeout, params T[] values)
        {
            observable.Buffer(timeout, values.Length)
                .ToEnumerable()
                .First()
                .Should()
                .Equal(values);
        }
        
        public static void ShouldCompleteWithError<T>(this IObservable<T> observable, Exception error)
        {
            var observer = new TestObserver<T>();
            using (observable.Subscribe(observer))
            {
                observer.Messages.Should().Equal(Notification.CreateOnError<(object, Exception)>(error));
            }
        }
    }
}