using System;

namespace CanI.Core.Authorization
{
    internal class AuthorizeIfICanAttribute : Attribute
    {
        public string Action { get; private set; }
        public string Subject { get; private set; }

        public AuthorizeIfICanAttribute(string action, string subject)
        {
            Action = action;
            Subject = subject;
        }
    }
}