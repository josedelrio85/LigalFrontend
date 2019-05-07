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
    public class InspeccionesGPSController : Controller
    {
        private InspeccionesGpsRepo repo = new InspeccionesGpsRepo();

        private int page = Functions.Globals.page;
        private int pageSizeBig = Functions.Globals.pageSizeBig;

        // GET: InspeccionesGPS
        public ActionResult Index()
        {
            InspeccionesGpsVM vm = new InspeccionesGpsVM();
            vm.buscador = new Models.Buscador.buscadorInspeccionesGps();
            vm.listaUsuarios = new UsuariosRepo().getListaUsuarios();
            ViewBag.controlador = "InspeccionesGPS";

            return View(vm);
        }


        [HttpPost]
        [ValidateHeaderAntiForgeryToken]
        public string Filtro(buscadorInspeccionesGps buscador)
        {
            ViewBag.controlador = "InspeccionesGPS";
            List<InspeccionesGpsVM> index = (List<InspeccionesGpsVM>)repo.getByParametro(buscador);

            JavaScriptSerializer js = new JavaScriptSerializer();
            List<objetoResultadoMapa> lista = new List<objetoResultadoMapa>();
            foreach (InspeccionesGpsVM insp in index)
            {
                string cx = insp.inspeccion.COORDX;
                string cy = insp.inspeccion.COORDY;
                string fecha = insp.inspeccion.FechaHoraVisita.ToString();
                string seriegan = insp.inspeccion.SERIEGANADERO.ToString();

                string nombreGan = insp.inspeccion.Nombre;
                string nombreInspec = insp.usuario.NOMBRE + " " + insp.usuario.APELLIDO1;
                string idIndustria = insp.inspeccion.IDIndustria;
                string poblacion = insp.inspeccion.Poblacion;
                string resultadoCharm = insp.inspeccion.ResultadoCHARM;
                string resultadoQuino = insp.inspeccion.RESULTADOQUINO;

                objetoResultadoMapa obj = new objetoResultadoMapa
                {
                    cx = cx,
                    cy = cy,
                    fecha = fecha,
                    seriegan = seriegan,
                    nombreGan = nombreGan,
                    nombreInspec = nombreInspec,
                    idIndustria = idIndustria,
                    poblacion = poblacion,
                    resultadoCharm = resultadoCharm,
                    resultadoQuino = resultadoQuino
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

            IEnumerable<InspeccionesGpsVM> index = (List<InspeccionesGpsVM>)repo.getSorted(buscador, paramOrden, direccion);

            var tabla = index.ToPagedList(pageNumber, pageSizeBig);

            return PartialView("Filtro", tabla);
        }

        [HttpPost]
        [ValidateHeaderAntiForgeryToken]
        public ActionResult PintaTabla(buscadorInspeccionesGps buscador, int pagina = 0)
        {
            List<InspeccionesGpsVM> index = (List<InspeccionesGpsVM>)repo.getByParametro(buscador);

            var pageNumber = pagina == 0 ? page : pagina;
            var onePage = index.ToPagedList(pageNumber, pageSizeBig);
            ViewBag.controlador = "InspeccionesGps";
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
            buscadorInspeccionesGps buscador = new buscadorInspeccionesGps();
            string valor = null;
            foreach (var element in Request.QueryString)
            {
                valor = null;
                if(Request.QueryString[element.ToString()] != "")
                {
                   valor = Request.QueryString[element.ToString()];
                }
                form.Add(element.ToString(), valor);
            }

            buscador.FechaHoraVisitaI = form.Get("buscador.FechaHoraVisitaI");
            buscador.FechaHoraVisitaF = form.Get("buscador.FechaHoraVisitaF");
            buscador.idUsuario = form.Get("buscador.idUsuario");
            buscador.Industria = form.Get("buscador.Industria");
            buscador.TipoInspeccion = form.Get("buscador.TipoInspeccion");

            List<InspeccionesGpsVM> index = (List<InspeccionesGpsVM>)repo.getByParametro(buscador);

            Response.Clear();
            Response.ContentType = "text/csv";
            string nombreFichero = "Listado_Inspecciones_GPS_" + DateTime.Now.ToString("yyyyMMdd");
            Response.AddHeader("Content-Disposition", "attachment;filename=" + nombreFichero + ".csv");

            Response.Write("Fecha Visita;Inspector;Industria;Serie Ganadero;Nombre Ganadero;Población;Resultado Charm;Longitud;Latitud\n");

            foreach (InspeccionesGpsVM vm in index)
            {
                string fechaHV = (!String.IsNullOrEmpty(vm.inspeccion.FechaHoraVisita.ToString())) ? vm.inspeccion.FechaHoraVisita.ToString() : "";
                string inspec = (!String.IsNullOrEmpty(vm.usuario.NOMBRE)) ? vm.usuario.NOMBRE : "";
                string indust = (!String.IsNullOrEmpty(vm.inspeccion.IDIndustria)) ? vm.inspeccion.IDIndustria : "";
                string serieg = (!String.IsNullOrEmpty(vm.inspeccion.SERIEGANADERO.ToString())) ? vm.inspeccion.SERIEGANADERO.ToString() : "";
                string nombreg = (!String.IsNullOrEmpty(vm.inspeccion.Nombre)) ? vm.inspeccion.Nombre.ToString() : "";
                string pob = (!String.IsNullOrEmpty(vm.inspeccion.Poblacion)) ? vm.inspeccion.Poblacion.ToString() : "";
                string rcharm = (!String.IsNullOrEmpty(vm.inspeccion.ResultadoCHARM)) ? vm.inspeccion.ResultadoCHARM.ToString() : "";
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

                Response.Write(System.String.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};\n", fechaHV, inspec, indust, serieg, nombreg, pob, rcharm, rquino, cx, cy));
            }

            Response.End();
        }
    }
}
