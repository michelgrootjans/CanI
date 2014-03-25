using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace CanI.Tests.TestUtilities
{
    public static class EnumerableTestExtensions
    {
        public static IEnumerable<T> ShouldContain<T>(this IEnumerable<T> list, T expectedItem)
        {
            if (!list.Any(item => Equals(item, expectedItem)))
                Assert.Fail("Expected list to contain '{0}': {1}", expectedItem, Format(list));
            return list;
        }

        private static string Format<T>(IEnumerable<T> list)
        {
            if(list == null) return "List is null";
            if (!list.Any()) return "List is empty";

            return string.Join("\n\r", list.Select(item => string.Format("- {0}", item)));
        }
    }
}