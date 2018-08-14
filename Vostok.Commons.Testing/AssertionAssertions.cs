using System;
using System.Diagnostics;
using System.Threading;
using JetBrains.Annotations;
using NSubstitute.Exceptions;
using NUnit.Framework;

namespace Vostok.Commons.Testing
{
    [PublicAPI]
    internal static class AssertionAssertions
    {
        public static void ShouldPassIn([NotNull] this Action assertion, TimeSpan wait) =>
            assertion.ShouldPassIn(wait, TimeSpan.FromMilliseconds(10));

        public static void ShouldPassIn([NotNull] this Action assertion, TimeSpan wait, TimeSpan pause)
        {
            var watch = Stopwatch.StartNew();

            while (watch.Elapsed < wait)
            {
                try
                {
                    assertion();
                    return;
                }
                catch (AssertionException)
                {
                }
                catch (ReceivedCallsException)
                {
                }

                Thread.Sleep(pause);
            }

            assertion();
        }

        public static void ShouldNotFailIn([NotNull] this Action assertion, TimeSpan wait) =>
            ShouldNotFailIn(assertion, wait, TimeSpan.FromMilliseconds(10));

        public static void ShouldNotFailIn([NotNull] this Action assertion, TimeSpan wait, TimeSpan pause)
        {
            var watch = Stopwatch.StartNew();

            while (watch.Elapsed < wait)
            {
                assertion();
                Thread.Sleep(pause);
            }
        }
    }
}