using System.Security.Principal;

namespace CanI.Demo
{
    public static class PrincipalExtensions
    {
        public static string PrintRoles(this IPrincipal principal)
        {
            var user = principal as DummyUser;
            return user == null ? "N/A" : string.Join(", ", user.Roles);
        }
    }
}