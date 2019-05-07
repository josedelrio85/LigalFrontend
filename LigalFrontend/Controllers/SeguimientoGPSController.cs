using LigalFrontend.DAL;
using LigalFrontend.Helpers;
using LigalFrontend.Models;
using LigalFrontend.Models.Buscador;
using LigalFrontend.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace LigalFrontend.Controllers
{
    public class SeguimientoGPSController : Controller
    {
        private SeguimientoGpsRepo repo = new SeguimientoGpsRepo();

        private int page = Functions.Globals.page;
        private int pageSizeBig = Functions.Globals.pageSizeBig;

        // GET: SeguimientoGps
        public ActionResult Index()
        {
            SeguimientoGpsVM vm = new SeguimientoGpsVM();
            vm.buscador = new buscadorSeguimientoGps();
            vm.listaUsuarios = new UsuariosRepo().getListaUsuarios();
            ViewBag.controlador = "SeguimientoGps";

            return View(vm);
        }


        [HttpPost]
        [ValidateHeaderAntiForgeryToken]
        public string Filtro(buscadorSeguimientoGps buscador)
        {
            ViewBag.controlador = "SeguimientoGps";
            List<SeguimientoGpsVM> index = (List<SeguimientoGpsVM>) repo.getByParametro(buscador);

            JavaScriptSerializer js = new JavaScriptSerializer();
            List<objetoResultadoMapa> lista = new List<objetoResultadoMapa>();
            foreach (SeguimientoGpsVM cgps in index)
            {
                string cx = cgps.coordenadasGps.LONGITUDGPS.ToString().Replace(",", ".");
                string cy = cgps.coordenadasGps.LATITUDGPS.ToString().Replace(",",".");
                string fecha = cgps.coordenadasGps.FECHAHORAPDA.ToString();
                string seriegan = cgps.coordenadasGps.NPUNTO != null ? cgps.coordenadasGps.NPUNTO.ToString() : "";
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
        public ActionResult Orden(buscadorSeguimientoGps buscador, objetoOrdenMapa paramOrden, int direccion, int pagina = 0)
        {
            var pageNumber = pagina == 0 ? page : pagina;

            IEnumerable<SeguimientoGpsVM> index = (List<SeguimientoGpsVM>)repo.getSorted(buscador, paramOrden, direccion);

            var tabla = index.ToPagedList(pageNumber, pageSizeBig);

            return PartialView("Filtro", tabla);
        }

        [HttpPost]
        [ValidateHeaderAntiForgeryToken]
        public ActionResult PintaTabla(buscadorSeguimientoGps buscador, int pagina = 0)
        {
            List<SeguimientoGpsVM> index = (List<SeguimientoGpsVM>)repo.getByParametro(buscador);

            var pageNumber = pagina == 0 ? page : pagina;
            var onePage = index.ToPagedList(pageNumber, pageSizeBig);
            ViewBag.controlador = "SeguimientoGps";
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

            buscadorSeguimientoGps buscador = new buscadorSeguimientoGps();
            buscador.FechaHoraVisitaI = form.Get("buscador.FechaHoraVisitaI");
            buscador.FechaHoraVisitaF = form.Get("buscador.FechaHoraVisitaF");
            buscador.idUsuario = form.Get("buscador.idUsuario");

            List<SeguimientoGpsVM> index = (List<SeguimientoGpsVM>)repo.getByParametro(buscador);

            Response.Clear();
            Response.ContentType = "text/csv";
            string nombreFichero = "Listado_Seguimiento_GPS_" + DateTime.Now.ToString("yyyyMMdd");
            Response.AddHeader("Content-Disposition", "attachment;filename=" + nombreFichero + ".csv");

            Response.Write("Inspector;Fecha Visita;Longitud;Latitud;Cod.Punto\n");

            foreach (SeguimientoGpsVM vm in index)
            {
                string fechaHV = (!String.IsNullOrEmpty(vm.coordenadasGps.FECHAHORAPDA.ToString())) ? vm.coordenadasGps.FECHAHORAPDA.ToString() : "";
                string inspec = (!String.IsNullOrEmpty(vm.usuario.NOMBRE)) ? vm.usuario.NOMBRE : "";
                string cx = (!String.IsNullOrEmpty(vm.coordenadasGps.LONGITUDGPS.ToString())) ? vm.coordenadasGps.LONGITUDGPS.ToString() : "";
                string cy = (!String.IsNullOrEmpty(vm.coordenadasGps.LATITUDGPS.ToString())) ? vm.coordenadasGps.LATITUDGPS.ToString() : "";
                string obs = (!String.IsNullOrEmpty(vm.coordenadasGps.NPUNTO)) ? vm.coordenadasGps.NPUNTO.ToString() : "";

                Response.Write(System.String.Format("{0};{1};{2};{3};{4};\n", inspec, fechaHV, cx, cy, obs));
            }

            Response.End();
        }
    }
}