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

        public static IEnumerable<T> ShouldNotContain<T>(this IEnumerable<T> list, T expectedItem)
        {
            if (list.Any(item => Equals(item, expectedItem)))
                Assert.Fail("Expected list not to contain '{0}', but it did: {1}", expectedItem, Format(list));
            return list;
        }

        private static string Format<T>(IEnumerable<T> list)
        {
            if(list == null) return "List is null";
            if (!list.Any()) return "List is empty";

            return string.Join("", list.Select(item => string.Format("\n\r- {0}", item)));
        }
    }
}