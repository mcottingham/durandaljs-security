using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace DurandalSecurity.Models.SaveValidators.Validators {
    public class TestValidator : GenericValidator<Test> {
        public TestValidator(object instance, SecurityDbContext context) : base(instance, context) {
        }

        public override bool Validate(IPrincipal user) {
            if (_entity.Name == "Foo") {
                throw new InvalidOperationException(_entity.Name + " is not a valid name");
            }
            return true;
        }
    }
}