using System;
using System.Threading.Tasks;
using FluentAssertions;
using JetBrains.Annotations;

namespace Vostok.Commons.Testing
{
    [PublicAPI]
    internal static class TaskAssertions
    {
        public static void ShouldCompleteImmediately(this Task task)
        {
            task.IsCompleted.Should().BeTrue();
        }

        public static void ShouldCompleteIn(this Task task, TimeSpan timeout)
        {
            task.Wait(timeout).Should().BeTrue();
        }

        public static void ShouldCompleteWithErrorIn<TException>(this Task task, TimeSpan timeout)
            where TException : Exception
        {
            Action action = () => task.Wait(timeout);

            action.Should().Throw<AggregateException>().WithInnerException<TException>().Which.ShouldBePrinted();
        }

        public static void ShouldNotCompleteIn(this Task task, TimeSpan timeout)
        {
            task.Wait(timeout).Should().BeFalse();
        }
    }
}