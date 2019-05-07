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
    public class PuntosRecogidaController : Controller
    {
        private PuntosRecogidaRepo repo = new PuntosRecogidaRepo();

        private int page = Functions.Globals.page;
        private int pageSizeBig = Functions.Globals.pageSizeBig;

        // GET: PuntosRecogida
        public ActionResult Index(int? page)
        {
            List<PuntosRecogidaVM> index = repo.getAll();
            var pageNumber = page ?? 1;
            var onePage = index.ToPagedList(pageNumber, pageSizeBig);
            ViewBag.controlador = "PuntosRecogida";

            return View(onePage);
        }

        // GET: PuntosRecogida/Create
        public ActionResult Create()
        {
            PuntosRecogidaVM vista = new PuntosRecogidaVM();

            UsuariosRepo repUsu = new UsuariosRepo();
            vista.listaUsuarios = repUsu.getListaUsuarios();

            return View(vista);
        }

        // POST: PuntosRecogida/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PuntosRecogidaVM vm)
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

            PuntosRecogidaVM vista = repo.getById(id);
            UsuariosRepo repUsu = new UsuariosRepo();
            vista.listaUsuarios = repUsu.getListaUsuarios();

            if (vista == null)
            {
                return HttpNotFound();
            }
            return View(vista);
        }

        // POST: PuntosRecogida/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PuntosRecogidaVM vm)
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
            PuntosRecogidaVM vm = repo.getById(id);
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
        public ActionResult Filtro(buscadorPuntosRecogida buscador, int pagina = 0)
        {
            List<PuntosRecogidaVM> index = (List<PuntosRecogidaVM>) repo.getByParametro(buscador);

            var pageNumber = pagina == 0 ? page : pagina; 
            var onePage = index.ToPagedList(pageNumber, pageSizeBig);
            ViewBag.controlador = "PuntosRecogida";

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
        public ActionResult Orden(buscadorPuntosRecogida buscador,objetoOrdenMapa paramOrden, int direccion, int pagina = 0)
        {
            IEnumerable<PuntosRecogidaVM> resulFiltro = (List<PuntosRecogidaVM>)repo.getSorted(buscador, paramOrden, direccion);

            var pageNumber = pagina == 0 ? page : pagina;
            var onePage = resulFiltro.ToPagedList(pageNumber, pageSizeBig);
            ViewBag.controlador = "PuntosRecogida";

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
