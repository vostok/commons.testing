using System;
using System.Diagnostics;
using System.Threading;
using FluentAssertions;
using FluentAssertions.Extensions;
using JetBrains.Annotations;

namespace Vostok.Commons.Testing
{
    [PublicAPI]
    internal static class ConditionAssertions
    {
        public static void ShouldBecomeTrueIn(this Func<bool> condition, TimeSpan wait)
        {
            condition.ShouldBecomeTrueIn(wait, 10.Milliseconds());
        }

        public static void ShouldBecomeTrueIn(this Func<bool> condition, TimeSpan wait, TimeSpan pause)
        {
            var watch = Stopwatch.StartNew();

            while (watch.Elapsed < wait)
            {
                if (condition())
                    return;

                Thread.Sleep(pause);
            }

            condition().Should().BeTrue("because given condition should have become true in {0}", wait);
        }

        public static void ShouldNotBecomeTrueIn(this Func<bool> condition, TimeSpan wait)
        {
            ShouldNotBecomeTrueIn(condition, wait, 10.Milliseconds());
        }

        public static void ShouldNotBecomeTrueIn(this Func<bool> condition, TimeSpan wait, TimeSpan pause)
        {
            var watch = Stopwatch.StartNew();

            while (watch.Elapsed < wait)
            {
                condition().Should().BeFalse("because given condition shouldn't have become true in {0}", wait);

                Thread.Sleep(pause);
            }
        }
    }
}