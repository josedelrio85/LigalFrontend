using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace LigalFrontend.Helpers
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class ValidateHeaderAntiForgeryTokenAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            //if (filterContext == null)
            //{
            //    throw new ArgumentNullException("filterContext");
            //}

            //var httpContext = filterContext.HttpContext;
            //var cookie = httpContext.Request.Cookies[AntiForgeryConfig.CookieName];
            //AntiForgery.Validate(cookie != null ? cookie.Value : null, httpContext.Request.Headers["__RequestVerificationToken"]);

            var request = filterContext.HttpContext.Request;
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            var httpContext = new JsonAntiForgeryHttpContextWrapper(HttpContext.Current);

            try
            {
                var antiForgeryCookie = request.Cookies[AntiForgeryConfig.CookieName];
                var cookieValue = antiForgeryCookie != null
                       ? antiForgeryCookie.Value
                       : null;
                AntiForgery.Validate(cookieValue, request.Headers["__RequestVerificationToken"]);
                //AntiForgery.Validate(cookieValue, request.Form["__RequestVerificationToken"]);
            }
            catch (HttpAntiForgeryException ex)
            {
                //Append where the request url.
                var msg = string.Format("{0} from {1}", ex.Message, httpContext.Request.Url);
                throw new HttpAntiForgeryException(msg);
            }
        }



        private class JsonAntiForgeryHttpContextWrapper : HttpContextWrapper
        {
            readonly HttpRequestBase _request;
            public JsonAntiForgeryHttpContextWrapper(HttpContext httpContext)
                : base(httpContext)
            {
                _request = new JsonAntiForgeryHttpRequestWrapper(httpContext.Request);
            }

            public override HttpRequestBase Request
            {
                get
                {
                    return _request;
                }
            }
        }

        private class JsonAntiForgeryHttpRequestWrapper : HttpRequestWrapper
        {
            readonly NameValueCollection _form;

            public JsonAntiForgeryHttpRequestWrapper(HttpRequest request)
                : base(request)
            {
                _form = new NameValueCollection(request.Form);
                if (request.Headers["__RequestVerificationToken"] != null)
                {
                    _form["__RequestVerificationToken"] = request.Headers["__RequestVerificationToken"];
                }
            }

            public override NameValueCollection Form
            {
                get
                {
                    return _form;
                }
            }
        }
    }
}