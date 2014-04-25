using System.Linq;
using System.Security.Principal;

namespace CanI.Demo
{
    public class DummyUser : IPrincipal, IIdentity
    {
        private readonly string name;

        public DummyUser(string name, params string[] roles)
        {
            this.name = name;
            this.Roles = roles;
        }

        public bool IsInRole(string role)
        {
            return Roles.Contains(role);
        }

        public IIdentity Identity { get { return this; } }
        public string Name { get { return name; } }
        public string AuthenticationType { get { return "dummy"; } }
        public bool IsAuthenticated { get { return true; } }
        internal string[] Roles { get; private set; }
    }
}