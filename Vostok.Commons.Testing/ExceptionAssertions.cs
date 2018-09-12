using System;
using JetBrains.Annotations;

namespace Vostok.Commons.Testing
{
    [PublicAPI]
    internal static class ExceptionAssertions
    {
        public static void ShouldBePrinted<T>(this T exception)
            where T : Exception
        {
            Console.Out.WriteLine(exception);
        }
    }
}