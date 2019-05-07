﻿using System.Web;
using System.Web.Optimization;

namespace LigalFrontend
{
    public class BundleConfig
    {
        // Para obtener más información sobre Bundles, visite http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // preparado para la producción y podrá utilizar la herramienta de compilación disponible en http://modernizr.com para seleccionar solo las pruebas que necesite.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js",
            //          "~/Scripts/bootstrap-*",
            //          "~/Scripts/respond.js",
            //          "~/Scripts/Funciones.js"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/moment*",
                      "~/Scripts/bootstrap-datetimepicker.js",
                      "~/Scripts/Funciones.js",
                      "~/Scripts/auxiliares.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-*",
                      //"~/Content/Site.css",
                      "~/Content/PagedList.css",
                      "~/Content/estilos.css"));

            bundles.Add(new ScriptBundle("~/bundles/funcionesInspeccion").Include(
                        "~/Scripts/FuncionesInspeccion.js"));

            bundles.Add(new ScriptBundle("~/bundles/funcionesDiario").Include(
            "~/Scripts/funcionesDiario.js"));

            bundles.Add(new StyleBundle("~/Content/mediaquery").Include(
                      "~/Content/mediaquery/min-320px.css",
                      "~/Content/mediaquery/min-768px.css",
                      "~/Content/mediaquery/min-1280px.css",
                      "~/Content/mediaquery/min-1920px.css"));
        }
    }
}
