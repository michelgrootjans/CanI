using System;
using CanI.Core.Authorization;

namespace CanI.Tests.TestUtilities
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

        public static void IShouldBeAbleToExecute(object command)
        {
            var permission = I.CanExecute(command);
            if (!permission)
                throw new Exception(string.Format("Expected to be able to execute [{0}], but I wasn't.", command));
        }

        public static void IShouldNotBeAbleToExecute(object command)
        {
            var permission = I.CanExecute(command);
            if (permission)
                throw new Exception(string.Format("Expected not to be able to execute [{0}], but I was.", command));
        }
    }
}