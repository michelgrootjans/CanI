using System;
using CanI.Core.Configuration;

namespace CanI.Core.Authorization
{
    public abstract class Permission : IPermissionConfiguration
    {
        public abstract IPermissionConfiguration If<T>(Func<T, bool> predicate);
        public abstract bool Authorizes(string requestedAction, object requestedSubject);
        public abstract bool AllowsExecutionOf(object command);
    }
}