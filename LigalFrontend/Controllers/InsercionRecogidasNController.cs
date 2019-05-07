using LigalFrontend.DAL;
using LigalFrontend.Helpers;
using LigalFrontend.Models;
using LigalFrontend.Models.Buscador;
using LigalFrontend.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace LigalFrontend.Controllers
{
    public class InsercionRecogidasNController : Controller
    {
        private InsercionRecogidasRepo repo = new InsercionRecogidasRepo();

        private int page = Functions.Globals.page;
        private int pageSizeBig = Functions.Globals.pageSizeBig;

        // GET: InsercionRecogidasN
        public ActionResult Index(int? page)
        {
            List<InsercionRecogidasVM> index = repo.getAll();
            var pageNumber = page ?? 1;
            var onePage = index.ToPagedList(pageNumber, pageSizeBig);
            ViewBag.controlador = "InsercionRecogidasN";

            ViewBag.listaUsuarios = new UsuariosRepo().getListaUsuarios();

            return View(onePage);
        }

        // GET: InsercionRecogidasN/Create
        public ActionResult Create()
        {
            InsercionRecogidasVM vista = new InsercionRecogidasVM();

            UsuariosRepo repUsu = new UsuariosRepo();
            vista.listaUsuarios = repUsu.getListaUsuarios();

            PuntosRecogidaRepo repPr = new PuntosRecogidaRepo();
            vista.listaPuntoRecogidaDia = repPr.getListaPuntosRecogida();

            vista.listaRecogCliente = new List<RecogClientesVM>();

            return View(vista);
        }

        // POST: InsercionRecogidasN/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InsercionRecogidasVM vm)
        {
            if (ModelState.IsValid)
            {
                vm.puntoRecogidaDia.HORA = vm.puntoRecogidaDia.FECHA.Value.ToString("HH:mm");
                repo.Insert(vm);                
                repo.Save();

                almacenaRecogClientes(vm.puntoRecogidaDia.ID);

                return RedirectToAction("Index");
            }

            return View(vm);
        }

        // GET: InsercionRecogidasN/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            InsercionRecogidasVM vista = repo.getById(id);
            if (vista == null)
            {
                return HttpNotFound();
            }
            UsuariosRepo repUsu = new UsuariosRepo();
            vista.listaUsuarios = repUsu.getListaUsuarios();
            PuntosRecogidaRepo repPr = new PuntosRecogidaRepo();
            vista.listaPuntoRecogidaDia = repPr.getListaPuntosRecogida();

            vista.listaRecogCliente = new RecogClientesRepo().getByIdRecogida(vista.puntoRecogidaDia.ID);

            vista.listaImagenes = new ImageSliderVM();
            
            vista.listaImagenes.lista.Add(vista.puntoRecogidaDia.FOTO1);
            vista.listaImagenes.lista.Add(vista.puntoRecogidaDia.FOTO2);
            vista.listaImagenes.lista.Add(vista.puntoRecogidaDia.FOTO3);
            vista.listaImagenes.lista.Add(vista.puntoRecogidaDia.VIDEO);

            return View(vista);
        }

        // POST: InsercionRecogidasN/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(InsercionRecogidasVM vm)
        {
            if (ModelState.IsValid)
            {
                vm.puntoRecogidaDia.HORA = vm.puntoRecogidaDia.FECHA.Value.ToString("HH:mm");
                repo.Update(vm);
                repo.Save();
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // POST: InsercionRecogidasN/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public void DeleteConfirmed(int id)
        {
            InsercionRecogidasVM vm = repo.getById(id);
            
            RecogClientesRepo recogrRepo = new RecogClientesRepo();
            ProductosgrRepo prRepo = new ProductosgrRepo();

            List<RecogClientesVM> listaRecog = recogrRepo.getByIdRecogida(vm.puntoRecogidaDia.ID);
            foreach(RecogClientesVM rc in listaRecog)
            {
                List<ProductosgrVM> listaPgr = prRepo.getByIdRecogida(rc.recogidasR.ID);
                foreach(ProductosgrVM pgr in listaPgr)
                {
                    prRepo.Delete(pgr);
                }
                prRepo.Save();
                recogrRepo.Delete(rc);
            }
            recogrRepo.Save();

            repo.Delete(vm);
            repo.Save();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        [ValidateHeaderAntiForgeryToken]
        public ActionResult Filtro(buscadorInsercionRecogidas buscador, int pagina = 0)
        {
            List<InsercionRecogidasVM> index = (List<InsercionRecogidasVM>)repo.getByParametro(buscador);

            var pageNumber = pagina == 0 ? page : pagina;
            var onePage = index.ToPagedList(pageNumber, pageSizeBig);
            ViewBag.controlador = "InsercionRecogidasN";

            if (Request.IsAjaxRequest())
            {
                return PartialView("Filtro", onePage);
            }
            else
            {
                return View(onePage);
            }
        }


        [HttpPost]
        [ValidateHeaderAntiForgeryToken]
        public ActionResult Orden(buscadorInsercionRecogidas buscador, objetoOrdenMapa paramOrden, int direccion, int pagina = 0)
        {
            IEnumerable<InsercionRecogidasVM> resulFiltro = (List<InsercionRecogidasVM>)repo.getSorted(buscador, paramOrden, direccion);

            var pageNumber = pagina == 0 ? page : pagina;
            var onePage = resulFiltro.ToPagedList(pageNumber, pageSizeBig);
            ViewBag.controlador = "InsercionRecogidasN";

            if (Request.IsAjaxRequest())
            {
                return PartialView("Filtro", onePage);
            }
            else
            {
                return View(onePage);
            }
        }

        protected void almacenaRecogClientes(int idRecogida)
        {
            if(!String.IsNullOrEmpty(Request.Form["recogidasR.IDCLIENTE"])){

                var idCliente = Request.Form["recogidasR.IDCLIENTE"];
                var graddeja = Request.Form["recogidasR.GRADDEJA"];
                var gradrecoge = Request.Form["recogidasR.GRADRECOGE"];
                var idObserva = Request.Form["recogidasR.IDOBSERVA"];
                var observacion = Request.Form["recogidasR.OBSERVACIONES"];
                string[] separatingChars = { "," };
                List<string> gradrecoges = new List<string>();
                List<string> graddejas = new List<string>();
                List<string> idsCliente = new List<string>();
                List<string> idsObserva = new List<string>();
                List<string> observaciones = new List<string>();

                if (idCliente.Contains(","))
                {
                    idsCliente = idCliente.Split(separatingChars, System.StringSplitOptions.None).ToList();
                }
                else
                {
                    idsCliente.Add(idCliente);
                }

                if (graddeja.Contains(","))
                {
                    graddejas = graddeja.Split(separatingChars, System.StringSplitOptions.None).ToList();
                }
                else
                {
                    graddejas.Add(graddeja);
                }

                if (gradrecoge.Contains(","))
                {
                    gradrecoges = gradrecoge.Split(separatingChars, System.StringSplitOptions.None).ToList();
                }
                else
                {
                    gradrecoges.Add(gradrecoge);
                }

                if (idObserva.Contains(","))
                {
                    idsObserva = idObserva.Split(separatingChars, System.StringSplitOptions.None).ToList();
                }
                else
                {
                    idsObserva.Add(idObserva);
                }

                if (observacion.Contains(","))
                {
                    observaciones = observacion.Split(separatingChars, System.StringSplitOptions.None).ToList();
                }
                else
                {
                    observaciones.Add(observacion);
                }

                RecogClientesRepo recogRepo = new RecogClientesRepo();
                for(int i = 0; i < idsCliente.Count; i++)
                {
                    RecogClientesVM r = new RecogClientesVM();
                    r.recogidasR = new gen_recogidasR();
                    r.recogidasR.IDCLIENTE = Int32.Parse(idsCliente[i]);
                    r.recogidasR.GRADDEJA = Int32.Parse(graddejas[i]);
                    r.recogidasR.GRADRECOGE = Int32.Parse(gradrecoges[i]);
                    r.recogidasR.IDOBSERVA = Int32.Parse(idsObserva[i]);
                    r.recogidasR.OBSERVACIONES = observaciones[i];

                    r.recogidasR.IDRECOGIDA = idRecogida;

                    recogRepo.Insert(r);
                }
                recogRepo.Save();
            }
        }
    }
}