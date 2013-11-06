using System.Web.Mvc;
using DurandalSecurity.Attributes;
using System.Web.Http.Filters;

namespace DurandalSecurity {
    public class FilterConfig {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());           
        }

        public static void RegisterHttpFilters(HttpFilterCollection filters) {
            filters.Add(new ValidateJsonAntiForgeryTokenAttribute());
        }
    }
}