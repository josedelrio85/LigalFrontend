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

namespace LigalFrontend.Controllers
{
    public class InspeccionesQuinoGPSController : Controller
    {
        private InspeccionesGpsRepo repo = new InspeccionesGpsRepo();

        private int page = Functions.Globals.page;
        private int pageSizeBig = Functions.Globals.pageSizeBig;

        // GET: InspeccionesQuinoGPS
        public ActionResult Index()
        {
            InspeccionesGpsVM vm = new InspeccionesGpsVM();
            vm.buscador = new Models.Buscador.buscadorInspeccionesGps();
            vm.listaUsuarios = new UsuariosRepo().getListaUsuarios();
            ViewBag.controlador = "InspeccionesQuinoGPS";

            return View("~/Views/InspeccionesGPS/Index.cshtml", vm);
        }

        [HttpPost]
        [ValidateHeaderAntiForgeryToken]
        public string Filtro(buscadorInspeccionesGps buscador)
        {
            ViewBag.controlador = "InspeccionesQuinoGPS";
            List<InspeccionesGpsVM> index = (List<InspeccionesGpsVM>)repo.getByParametroQuino(buscador);

            JavaScriptSerializer js = new JavaScriptSerializer();
            List<objetoResultadoMapa> lista = new List<objetoResultadoMapa>();
            foreach (InspeccionesGpsVM insp in index)
            {
                string cx = insp.inspeccion.COORDX;
                string cy = insp.inspeccion.COORDY;
                string fecha = insp.inspeccion.FechaHoraVisita.ToString();
                string seriegan = insp.inspeccion.SERIEGANADERO.ToString();
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
        public ActionResult Orden(buscadorInspeccionesGps buscador, objetoOrdenMapa paramOrden, int direccion, int pagina = 0)
        {
            var pageNumber = pagina == 0 ? page : pagina;

            IEnumerable<InspeccionesGpsVM> index = (List<InspeccionesGpsVM>)repo.getSorted(buscador, paramOrden, direccion, true);

            var tabla = index.ToPagedList(pageNumber, pageSizeBig);

            return PartialView("~/Views/InspeccionesGPS/Filtro.cshtml", tabla);
        }

        [HttpPost]
        [ValidateHeaderAntiForgeryToken]
        public ActionResult PintaTabla(buscadorInspeccionesGps buscador, int pagina = 0)
        {
            List<InspeccionesGpsVM> index = (List<InspeccionesGpsVM>)repo.getByParametroQuino(buscador);

            var pageNumber = pagina == 0 ? page : pagina;
            var onePage = index.ToPagedList(pageNumber, pageSizeBig);
            ViewBag.controlador = "InspeccionesQuinoGPS";
            ViewBag.nombreAction = "PintaTabla";

            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Views/InspeccionesGPS/Filtro.cshtml", onePage);
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
            buscadorInspeccionesGps buscador = new buscadorInspeccionesGps();
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

            buscador.FechaHoraVisitaI = form.Get("buscador.FechaHoraVisitaI");
            buscador.FechaHoraVisitaF = form.Get("buscador.FechaHoraVisitaF");
            buscador.idUsuario = form.Get("buscador.idUsuario");
            buscador.Industria = form.Get("buscador.Industria");

            List<InspeccionesGpsVM> index = (List<InspeccionesGpsVM>)repo.getByParametroQuino(buscador);

            Response.Clear();
            Response.ContentType = "text/csv";
            string nombreFichero = "Listado_Inspecciones_Quinolona_GPS_" + DateTime.Now.ToString("yyyyMMdd");
            Response.AddHeader("Content-Disposition", "attachment;filename=" + nombreFichero + ".csv");

            Response.Write("Fecha Visita;Inspector,Industria,Serie Ganadero,Nombre Ganadero, Población, Resultado Quinolona, Latitud, Longitud\n");

            foreach (InspeccionesGpsVM vm in index)
            {
                string fechaHV = (!String.IsNullOrEmpty(vm.inspeccion.FechaHoraVisita.ToString())) ? vm.inspeccion.FechaHoraVisita.ToString() : "";
                string inspec = (!String.IsNullOrEmpty(vm.usuario.NOMBRE)) ? vm.usuario.NOMBRE : "";
                string indust = (!String.IsNullOrEmpty(vm.inspeccion.IDIndustria)) ? vm.inspeccion.IDIndustria : "";
                string serieg = (!String.IsNullOrEmpty(vm.inspeccion.SERIEGANADERO.ToString())) ? vm.inspeccion.SERIEGANADERO.ToString() : "";
                string nombreg = (!String.IsNullOrEmpty(vm.inspeccion.Nombre)) ? vm.inspeccion.Nombre.ToString() : "";
                string pob = (!String.IsNullOrEmpty(vm.inspeccion.Poblacion)) ? vm.inspeccion.Poblacion.ToString() : "";
                string rquino = (!String.IsNullOrEmpty(vm.inspeccion.RESULTADOQUINO)) ? vm.inspeccion.RESULTADOQUINO.ToString() : "";
                string cx = (!String.IsNullOrEmpty(vm.inspeccion.COORDX)) ? vm.inspeccion.COORDX.ToString() : "";
                if (cx.Contains("."))
                {
                    cx = cx.Replace(".", ",");
                }
                string cy = (!String.IsNullOrEmpty(vm.inspeccion.COORDY)) ? vm.inspeccion.COORDY.ToString() : "";
                if (cy.Contains("."))
                {
                    cy = cx.Replace(".", ",");
                }
                Response.Write(System.String.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};\n", fechaHV, inspec, indust, serieg, nombreg, pob, rquino, cx, cy));
            }

            Response.End();

        }
    }
}
