using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace DurandalSecurity.Models.SaveValidators.Validators {
    public class GenericValidator<T> : ISaveValidator where T : class {
        protected T _entity { get; set; }
        protected SecurityDbContext _context { get; set; }

        public GenericValidator(object entity, SecurityDbContext context) {
            if (entity.GetType() != typeof(T)) {
                throw new TypeInitializationException(typeof(T).Name, new ArgumentException("entity"));
            }
            _entity = (T)entity;
            _context = context;
        }

        public virtual bool Validate(IPrincipal user) {
            return true;
        }
    }
}