using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using LigalFrontend.Models;
using PagedList;
using LigalFrontend.DAL;
using LigalFrontend.ViewModels;
using System.Collections.Generic;
using LigalFrontend.Helpers;
using LigalFrontend.Models.Buscador;

namespace LigalFrontend.Controllers
{
    public class UsuariosController : Controller
    {
        private UsuariosRepo repo = new UsuariosRepo();
        private int page = Functions.Globals.page;
        private int pageSizeBig = Functions.Globals.pageSizeBig;

        // GET: Usuarios
        public ActionResult Index(int? page)
        {
            List<UsuariosVM> index = repo.getAll();
            var pageNumber = page ?? 1;
            var onePage = index.ToPagedList(pageNumber, pageSizeBig);
            ViewBag.controlador = "Usuarios";

            return View(onePage);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            UsuariosVM uvm = new UsuariosVM();
            uvm.listaTipoUsuario = new GenericRepository<LigalEntities, gen_tipouser>().getTodo().ToList();
            uvm.usuario.USERTYPE = uvm.listaTipoUsuario.ElementAt(0).ETIQUETA;
            return View(uvm);
        }

        // POST: Usuarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UsuariosVM vm)
        {
            if (ModelState.IsValid)
            {
                vm.usuario.IDEMPRESA = 1;
                vm.usuario.IDTIPO = (vm.usuario.USERTYPE == "WADMIN") ? 1 : 2;
                vm.usuario.IDEMPRESA = 1;
                vm.usuario.PWD = Functions.Functions.Base64Encode(vm.usuario.PWD);

                repo.Insert(vm);
                repo.Save();
                return RedirectToAction("Index");
            }

            return View(vm);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UsuariosVM vista = repo.getById(id);
            vista.listaTipoUsuario = new GenericRepository<LigalEntities, gen_tipouser>().getTodo().ToList();
            vista.usuario.PWD = Functions.Functions.Base64Decode(vista.usuario.PWD);

            if (vista == null)
            {
                return HttpNotFound();
            }
            return View(vista);
        }

        // POST: Usuarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UsuariosVM vm)
        {
            if (ModelState.IsValid)
            {
                vm.usuario.PWD = Functions.Functions.Base64Encode(vm.usuario.PWD);

                repo.Update(vm);
                repo.Save();
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public void DeleteConfirmed(int id)
        {
            UsuariosVM vm = repo.getById(id);
            if(vm != null)
            {
                repo.Delete(vm);
                repo.Save();
            }            
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
        public ActionResult Filtro(buscadorUsuarios buscador, int pagina = 0)
        {
            List<UsuariosVM> index = (List<UsuariosVM>)repo.getByParametro(buscador);

            var pageNumber = pagina == 0 ? page : pagina;
            var onePage = index.ToPagedList(pageNumber, pageSizeBig);
            ViewBag.controlador = "Usuarios";

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
        public ActionResult Orden(buscadorUsuarios buscador, objetoOrdenMapa paramOrden, int direccion, int pagina = 0)
        {
            IEnumerable<UsuariosVM> resulFiltro = (List<UsuariosVM>)repo.getSorted(buscador, paramOrden, direccion);

            var pageNumber = pagina == 0 ? page : pagina;
            var onePage = resulFiltro.ToPagedList(pageNumber, pageSizeBig);
            ViewBag.controlador = "Usuarios";

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
