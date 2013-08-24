using System;
using CanI.Core;

namespace CanI.Tests
{
    public static class Then
    {
        public static void IShouldBeAbleTo(string action, object subject)
        {
            var permission = I.Can(action, subject);
            if (!permission)
                throw new Exception(string.Format("Expected to be able to [{0}] [{1}], but I wasn't.", action, subject));
        }

        public static void IShouldNotBeAbleTo(string action, object subject)
        {
            var permission = I.Can(action, subject);
            if (permission)
                throw new Exception(string.Format("Expected not to be able to [{0}] [{1}], but I was.", action, subject));
        }
    }
}