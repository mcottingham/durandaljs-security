using DurandalSecurity.Models;
using DurandalSecurity.Models.SaveValidators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Breeze.WebApi2;
using Breeze.ContextProvider.EF6;
using Breeze.ContextProvider;
using Newtonsoft.Json.Linq;

namespace DurandalSecurity.Controllers {
    [BreezeController]
    public class SecurityController: ApiController {
        private Repository _repository;

        public SecurityController() {
            _repository = new Repository(User);
        }

        [HttpGet]
        public string MetaData() {
            return _repository.Metadata();
        }

        [HttpPost]
        public SaveResult SaveChanges(JObject saveBundle) {
            return _repository.SaveChanges(saveBundle);
        }
    }
}