using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace DurandalSecurity.Models.SaveValidators {
    public interface ISaveValidator {
        bool Validate(IPrincipal user);
    }
}