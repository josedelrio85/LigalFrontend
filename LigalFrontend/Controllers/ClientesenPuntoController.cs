using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using LigalFrontend.ViewModels;
using PagedList;
using LigalFrontend.DAL;
using LigalFrontend.Models.Buscador;
using LigalFrontend.Models;
using LigalFrontend.Helpers;

namespace LigalFrontend.Controllers
{
    public class ClientesenPuntoController : Controller
    {
        private ClientesEnPuntoRepo repo = new ClientesEnPuntoRepo();

        private int page = Functions.Globals.page;
        private int pageSizeBig = Functions.Globals.pageSizeBig;

        // GET: PuntosRecogida
        public ActionResult Index(int? page)
        {
            List<ClientesEnPuntoVM> index = repo.getAll();
            if(index.Count == 0)
            {
                ClientesEnPuntoVM aa = new ClientesEnPuntoVM();
                aa.clientesEnPunto.ID = 0;
                index.Add(aa);
            }
            var pageNumber = page ?? 1;
            var onePage = index.ToPagedList(pageNumber, pageSizeBig);
            ViewBag.controlador = "ClientesenPunto";

            return View(onePage);
        }

        // GET: PuntosRecogida/Create
        public ActionResult Create()
        {
            ClientesEnPuntoVM vista = new ClientesEnPuntoVM();

            vista.listaClientes = new GenericRepository<LigalEntities, gen_clientes>().getTodo();
            vista.listaPuntosRecogida = new GenericRepository<LigalEntities, GEN_PUNTOSRECOGIDA>().getTodo();

            return View(vista);
        }

        // POST: PuntosRecogida/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClientesEnPuntoVM vm)
        {
            if (ModelState.IsValid)
            {
                repo.Insert(vm);
                repo.Save();
                return RedirectToAction("Index");
            }

            return View(vm);
        }

        // GET: PuntosRecogida/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ClientesEnPuntoVM vista = repo.getById(id);
            vista.listaClientes = new GenericRepository<LigalEntities, gen_clientes>().getTodo(); ;
            vista.listaPuntosRecogida = new GenericRepository<LigalEntities, GEN_PUNTOSRECOGIDA>().getTodo();

            if (vista == null)
            {
                return HttpNotFound();
            }
            return View(vista);
        }

        // POST: PuntosRecogida/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ClientesEnPuntoVM vm)
        {
            if (ModelState.IsValid)
            {
                repo.Update(vm);
                repo.Save();
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // POST: PuntosRecogida/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public void DeleteConfirmed(int id)
        {
            ClientesEnPuntoVM vm = repo.getById(id);
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
        public ActionResult Filtro(buscadorClientesenPunto buscador, int pagina = 0)
        {
            List<ClientesEnPuntoVM> index = (List<ClientesEnPuntoVM>)repo.getByParametro(buscador);

            var pageNumber = pagina == 0 ? page : pagina;
            var onePage = index.ToPagedList(pageNumber, pageSizeBig);
            ViewBag.controlador = "ClientesenPunto";

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
        public ActionResult Orden(buscadorClientesenPunto buscador, objetoOrdenMapa paramOrden, int direccion, int pagina = 0)
        {
            IEnumerable<ClientesEnPuntoVM> resulFiltro = (List<ClientesEnPuntoVM>)repo.getSorted(buscador, paramOrden, direccion);

            var pageNumber = pagina == 0 ? page : pagina;
            var onePage = resulFiltro.ToPagedList(pageNumber, pageSizeBig);
            ViewBag.controlador = "ClientesenPunto";

            if (Request.IsAjaxRequest())
            {
                return PartialView("Filtro", onePage);
            }
            else
            {
                return View(onePage);
            }
        }
    }
}
