using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace DurandalSecurity.Controllers {
    public interface ICustomPrincipal : IPrincipal {
        int Id { get; set; }
        string Email { get; set; }
    }

    public class CustomPrincipal : ICustomPrincipal {
        public IIdentity Identity { get; private set; }

        public CustomPrincipal(string username) {
            Identity = new GenericIdentity(username);
        }

        public bool IsInRole(string role) {
            return Identity != null && Identity.IsAuthenticated &&
           !string.IsNullOrWhiteSpace(role) && Roles.IsUserInRole(Identity.Name, role);
        }

        public int Id { get; set; }
        public string Email { get; set; }
    }

    public class CustomPrincipalSerializedModel {
        public int Id { get; set; }
        public string Email { get; set; }
    }
}