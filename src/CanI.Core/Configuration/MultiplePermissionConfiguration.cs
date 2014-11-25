using System;
using System.Collections.Generic;

namespace CanI.Core.Configuration
{
    internal class MultiplePermissionConfiguration : IPermissionConfiguration
    {
        private readonly List<IPermissionConfiguration> permissionConfigurations;

        public MultiplePermissionConfiguration()
        {
            this.permissionConfigurations = new List<IPermissionConfiguration>();
        }

        public IPermissionConfiguration If<T>(Func<T, bool> predicate)
        {
            foreach (var permissionConfiguration in permissionConfigurations)
                permissionConfiguration.If(predicate);
            return this;
        }

        public void Add(IPermissionConfiguration permissionConfiguration)
        {
            permissionConfigurations.Add(permissionConfiguration);
        }
    }
}