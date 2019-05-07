using System;
using System.Diagnostics;
using System.Web.Mvc;

namespace LigalFrontend.Filters
{
    public class TrackUserIp : FilterAttribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Debug.WriteLine("Inside OnActionExecuting");
            string userIP = filterContext.HttpContext.Request.UserHostAddress;
            LogIP(filterContext.HttpContext.Request.Url.PathAndQuery, userIP, "Attempted");
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Debug.WriteLine("Inside OnActionExecuted");
            string userIP = filterContext.HttpContext.Request.UserHostAddress;
            LogIP(filterContext.HttpContext.Request.Url.PathAndQuery, userIP, "Completed");
        }

        private void LogIP(string url, string ip, string msg)
        {
            Debug.WriteLine(msg + " : " + url + "[" + ip + "] on " + DateTime.Now.ToString());
        }
    }
}