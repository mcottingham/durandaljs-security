using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace DurandalSecurity.Models.SaveValidators.Validators {
    public class LoginValidator : GenericValidator<Login> {
        public LoginValidator(object instance, SecurityDbContext context) : base(instance, context) {
        }

        public override bool Validate(IPrincipal user) {
            return true;
        }
    }
}