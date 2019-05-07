﻿using LigalFrontend.Filters;
using System.Web.Mvc;

namespace LigalFrontend
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new CustomHandleErrorAttribute());
            filters.Add(new TrackUserIp());
        }
    }
}
