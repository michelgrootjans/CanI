namespace CanI.Core.Configuration
{
    internal interface IInternalAbilityConfiguration
    {
        IPermissionConfiguration AllowTo(string action, string subject);
    }
}