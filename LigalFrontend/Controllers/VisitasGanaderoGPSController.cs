using System.Collections.Generic;
using System.Web.Mvc;
using LigalFrontend.DAL;
using LigalFrontend.ViewModels;
using LigalFrontend.Helpers;
using LigalFrontend.Models.Buscador;
using System.Web.Script.Serialization;
using LigalFrontend.Models;
using PagedList;
using System;
using System.Net.Http;
using System.IO;
using System.Net;
using System.Net.Http.Headers;

namespace LigalFrontend.Controllers
{
    public class VisitasGanaderoGPSController : Controller
    {
        private VisitasGanaderosRepo repo = new VisitasGanaderosRepo();

        private int page = Functions.Globals.page;
        private int pageSizeBig = Functions.Globals.pageSizeBig;

        // GET: InspeccionesGPS
        public ActionResult Index()
        {
            VisitasGanaderosVM vm = new VisitasGanaderosVM();
            vm.buscador = new Models.Buscador.buscadorVisitasGanadero();
            vm.listaUsuarios = new UsuariosRepo().getListaUsuarios();
            ViewBag.controlador = "VisitasGanadero";

            return View(vm);
        }


        [HttpPost]
        [ValidateHeaderAntiForgeryToken]
        public string Filtro(buscadorVisitasGanadero buscador)
        {
            ViewBag.controlador = "VisitasGanadero";
            List<VisitasGanaderosVM> index = (List<VisitasGanaderosVM>)repo.getByParametro(buscador);

            JavaScriptSerializer js = new JavaScriptSerializer();
            List<objetoResultadoMapa> lista = new List<objetoResultadoMapa>();
            foreach (VisitasGanaderosVM insp in index)
            {
                string cx = insp.visitasVet.COORDX;
                string cy = insp.visitasVet.COORDY;
                string fecha = insp.visitasVet.FECHA.ToString();
                string seriegan = insp.visitasVet.SERIEGAN.ToString();
                objetoResultadoMapa obj = new objetoResultadoMapa
                {
                    cx = cx,
                    cy = cy,
                    fecha = fecha,
                    seriegan = seriegan
                };

                lista.Add(obj);
            }
            return js.Serialize(lista);
        }

        [HttpPost]
        [ValidateHeaderAntiForgeryToken]
        public ActionResult Orden(buscadorVisitasGanadero buscador, objetoOrdenMapa paramOrden, int direccion, int pagina = 0)
        {
            var pageNumber = pagina == 0 ? page : pagina;

            IEnumerable<VisitasGanaderosVM> index = (List<VisitasGanaderosVM>)repo.getSorted(buscador, paramOrden, direccion);

            var tabla = index.ToPagedList(pageNumber, pageSizeBig);

            return PartialView("Filtro", tabla);
        }

        [HttpPost]
        [ValidateHeaderAntiForgeryToken]
        public ActionResult PintaTabla(buscadorVisitasGanadero buscador, int pagina = 0)
        {
            List<VisitasGanaderosVM> index = (List<VisitasGanaderosVM>)repo.getByParametro(buscador);

            var pageNumber = pagina == 0 ? page : pagina;
            var onePage = index.ToPagedList(pageNumber, pageSizeBig);
            ViewBag.controlador = "VisitasGanadero";
            ViewBag.nombreAction = "PintaTabla";

            if (Request.IsAjaxRequest())
            {
                return PartialView("Filtro", onePage);
            }
            else
            {
                return View(onePage);
            }
        }


        //[HttpPost]
        public void tablaToCSV()
        {
            FormCollection form = new FormCollection();
            string valor = null;
            foreach (var element in Request.QueryString)
            {
                valor = null;
                if (Request.QueryString[element.ToString()] != "")
                {
                    valor = Request.QueryString[element.ToString()];
                }
                form.Add(element.ToString(), valor);
            }

            buscadorVisitasGanadero buscador = new buscadorVisitasGanadero();
            buscador.FechaHoraVisitaI = form.Get("buscador.FechaHoraVisitaI");
            buscador.FechaHoraVisitaF = form.Get("buscador.FechaHoraVisitaF");
            buscador.idUsuario = form.Get("buscador.idUsuario");
            buscador.serieGanadero = form.Get("buscador.serieGanadero");
            buscador.codMuestra = Int32.Parse(form.Get("buscador.codMuestra"));

            List<VisitasGanaderosVM> index = (List<VisitasGanaderosVM>)repo.getByParametro(buscador);

            Response.Clear();
            Response.ContentType = "text/csv";
            string nombreFichero = "Listado_Vistas_Ganaderos_GPS_" + DateTime.Now.ToString("yyyyMMdd");
            Response.AddHeader("Content-Disposition", "attachment;filename="+nombreFichero+".csv");

            Response.Write("Fecha;Inspector;Serie Ganadero;Longitud;Latitud;Observacion\n");

            foreach (VisitasGanaderosVM vm in index)
            {
                string fechaHV = (!String.IsNullOrEmpty(vm.visitasVet.FECHA.ToString())) ? vm.visitasVet.FECHA.ToString() : "";
                string inspec = (!String.IsNullOrEmpty(vm.usuario.NOMBRE)) ? vm.usuario.NOMBRE : "";
                string serieg = (!String.IsNullOrEmpty(vm.visitasVet.SERIEGAN.ToString())) ? vm.visitasVet.SERIEGAN.ToString() : "";
                string cx = (!String.IsNullOrEmpty(vm.visitasVet.COORDX)) ? vm.visitasVet.COORDX.ToString() : "";
                if (cx.Contains("."))
                {
                    cx = cx.Replace(".", ",");
                }
                string cy = (!String.IsNullOrEmpty(vm.visitasVet.COORDY)) ? vm.visitasVet.COORDY.ToString() : "";
                if (cy.Contains("."))
                {
                    cy = cx.Replace(".", ",");
                }
                string obs = (!String.IsNullOrEmpty(vm.visitasVet.OBSERVA)) ? vm.visitasVet.OBSERVA.ToString() : "";

                Response.Write(System.String.Format("{0};{1};{2};{3};{4};{5};\n", fechaHV, inspec, serieg, cx, cy, obs));
            }

            Response.End();
        }
    }
}