using System;
using System.Linq;
using System.Reactive.Linq;
using JetBrains.Annotations;

namespace Vostok.Commons.Testing.Observable
{
    [PublicAPI]
    internal static class ObservableHelpers
    {
        public static T WaitFirstValue<T>(this IObservable<T> observable, TimeSpan timeout)
        {
            return observable.Buffer(timeout, 1)
                .ToEnumerable()
                .First()
                .First();
        }
    }
}