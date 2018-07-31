using System;
using System.Threading;
using NSubstitute.Exceptions;
using NUnit.Framework;
using Vostok.Commons.Conversions;
using Vostok.Commons.Time;

namespace Vostok.Commons.Testing
{
    public static class AssertionAssertions
    {
        public static void ShouldPassIn(this Action assertion, TimeSpan wait) =>
            assertion.ShouldPassIn(wait, 10.Milliseconds());

        public static void ShouldPassIn(this Action assertion, TimeSpan wait, TimeSpan pause)
        {
            var budget = TimeBudget.StartNew(wait);

            while (!budget.HasExpired())
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

        public static void ShouldNotFailIn(this Action assertion, TimeSpan wait) =>
            ShouldNotFailIn(assertion, wait, 10.Milliseconds());

        public static void ShouldNotFailIn(this Action assertion, TimeSpan wait, TimeSpan pause)
        {
            var budget = TimeBudget.StartNew(wait);

            while (!budget.HasExpired())
            {
                assertion();
                Thread.Sleep(pause);
            }
        }
    }
}