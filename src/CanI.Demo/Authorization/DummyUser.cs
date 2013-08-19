using System.Linq;
using System.Security.Principal;

namespace CanI.Demo.Authorization
{
    public class DummyUser : IPrincipal, IIdentity
    {
        private readonly string[] roles;

        public DummyUser(params string[] roles)
        {
            this.roles = roles;
        }

        public bool IsInRole(string role)
        {
            return roles.Contains(role);
        }

        public IIdentity Identity { get { return this; }}
        public string Name { get { return "Dummy User";} }
        public string AuthenticationType { get { return "dummy";} }
        public bool IsAuthenticated { get { return true;} }
    }
}