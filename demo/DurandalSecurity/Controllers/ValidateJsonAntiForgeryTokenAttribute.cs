using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace DurandalSecurity.Attributes {
    public class ValidateJsonAntiForgeryTokenAttribute : FilterAttribute, IAuthorizationFilter {
        public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation) {
            if (actionContext == null) {
                throw new ArgumentNullException("HttpActionContext");
            }

            if (actionContext.Request.Method != HttpMethod.Get) {
                return ValidateAntiForgeryToken(actionContext, cancellationToken, continuation);
            }

            return continuation();
        }

        private Task<HttpResponseMessage> FromResult(HttpResponseMessage result) {
            var source = new TaskCompletionSource<HttpResponseMessage>();
            source.SetResult(result);
            return source.Task;
        }

        private Task<HttpResponseMessage> ValidateAntiForgeryToken(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation) {
            try {
                var cookieToken = string.Empty;
                var formToken = string.Empty;
                IEnumerable<string> tokenHeaders;

                if(actionContext.Request.Headers.TryGetValues("__RequestVerificationToken", out tokenHeaders)) {
                    formToken = tokenHeaders.First();
                }

                var cookies = actionContext.Request.Headers.GetCookies("__RequestVerificationToken");
                var tokenCookie = cookies.First();
                if (tokenCookie != null) {
                    cookieToken = tokenCookie.Cookies.First().Value;
                }

                AntiForgery.Validate(cookieToken, formToken);
                //throw new System.Web.Mvc.HttpAntiForgeryException("BAM!");
            }
            catch (System.Web.Mvc.HttpAntiForgeryException ex) {
                actionContext.Response = new HttpResponseMessage {
                    StatusCode = HttpStatusCode.Forbidden,
                    RequestMessage = actionContext.ControllerContext.Request
                };

                return FromResult(actionContext.Response);
            }
            return continuation();
        }

    }
}