using System.Net;
using System.Web.Mvc;
using LigalFrontend.Models;
using PagedList;
using LigalFrontend.DAL;
using LigalFrontend.Helpers;
using LigalFrontend.Models.Buscador;
using System.Collections.Generic;
using LigalFrontend.ViewModels;

namespace LigalFrontend.Controllers
{
    public class ClientesController : Controller
    {
        private ClientesRepo repo = new ClientesRepo();

        private int page = Functions.Globals.page;
        private int pageSizeBig = Functions.Globals.pageSizeBig;

        // GET: gen_clientes
        public ActionResult Index(int? page)
        {
            List<ClienteVM> index = (List<ClienteVM>) repo.getAll();
            var pageNumber = page ?? 1;
            var onePage = index.ToPagedList(pageNumber, pageSizeBig);
            ViewBag.controlador = "Clientes";

            return View(onePage);
        }

        // GET: gen_clientes/Create
        public ActionResult Create()
        {
            return View(new ClienteVM());
        }

        // POST: gen_clientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClienteVM vm)
        {
            if (ModelState.IsValid)
            {
                repo.Insert(vm);
                repo.Save();
                return RedirectToAction("Index");
            }

            return View(vm);
        }

        // GET: gen_clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ClienteVM vista = repo.getById(id);

            if (vista == null)
            {
                return HttpNotFound();
            }
            return View(vista);
        }

        // POST: gen_clientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ClienteVM vm)
        {
            if (ModelState.IsValid)
            {
                repo.Update(vm);
                repo.Save();
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // POST: gen_clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public void DeleteConfirmed(int id)
        {
            ClienteVM vm = repo.getById(id);
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
        public ActionResult Filtro(buscadorClientes buscador, int pagina = 0)
        {
            IEnumerable<ClienteVM> index = repo.getByParametro(buscador);

            var pageNumber = pagina == 0 ? page : pagina;
            var onePage = index.ToPagedList(pageNumber, pageSizeBig);
            ViewBag.controlador = "Clientes";

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
        public ActionResult Orden(buscadorClientes buscador, objetoOrdenMapa paramOrden, int direccion, int pagina = 0)
        { 
            IEnumerable<ClienteVM> resulFiltro = repo.getSorted(buscador, paramOrden, direccion);

            var pageNumber = pagina == 0 ? page : pagina;
            var onePage = resulFiltro.ToPagedList(pageNumber, pageSizeBig);
            ViewBag.controlador = "Clientes";

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
