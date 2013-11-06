using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Breeze.WebApi2;
using Breeze.ContextProvider.EF6;
using DurandalSecurity.Models.SaveValidators;
using System.Security.Principal;

namespace DurandalSecurity.Models {
    public class Repository : EFContextProvider<SecurityDbContext> {
        private IPrincipal _user;
        
        public Repository(IPrincipal user) {
            _user = user;
        }

        protected override bool BeforeSaveEntity(Breeze.ContextProvider.EntityInfo entityInfo) {
            var validatorFactory = new SaveValidatorFactory<ISaveValidator>();
            var validator = validatorFactory.CreateInstance(entityInfo.Entity.GetType().Name, entityInfo.Entity, Context);
            return validator.Validate(_user);
        }
    }
}