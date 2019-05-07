using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LigalFrontend.Models;
using PagedList;
using LigalFrontend.DAL;
using LigalFrontend.ViewModels;

namespace LigalFrontend.Controllers
{
    public class UsuariosVehiculoController : Controller
    {
        private LigalEntities db = new LigalEntities();
		private GenericRepository<LigalEntities, GEN_USUARIOSVEHICULO> repo;
		private int page = Functions.Globals.page;
		private int pageSizeBig = Functions.Globals.pageSizeBig;

        // GET: UsuariosVehiculo
        private ActionResult Index(int? page)
        {
			
			repo = new GenericRepository<LigalEntities, GEN_USUARIOSVEHICULO>();
            var pageNumber = page ?? 1;
            var onePage = repo.getTodo().OrderBy(x => x.ID).ToPagedList(pageNumber, pageSizeBig);
            ViewBag.controlador = "UsuariosVehiculo";

            return View(onePage);
        }

        // GET: UsuariosVehiculo/Create
        private ActionResult Create()
        {
            return View();
        }

        // POST: UsuariosVehiculo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        private ActionResult Create([Bind(Include = "ID,IDUSUARIO,IDMATRICULA,ROWID")] GEN_USUARIOSVEHICULO gEN_USUARIOSVEHICULO)
        {
            if (ModelState.IsValid)
            {
				using (repo = new GenericRepository<LigalEntities, GEN_USUARIOSVEHICULO>())
                {
                    gEN_USUARIOSVEHICULO.ROWID = System.Guid.NewGuid().ToString();
                    repo.Insert(gEN_USUARIOSVEHICULO);
                    repo.Save();
                }
                return RedirectToAction("Index");
            }

            return View(gEN_USUARIOSVEHICULO);
        }

        // GET: UsuariosVehiculo/Edit/5
        private ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GEN_USUARIOSVEHICULO gEN_USUARIOSVEHICULO = db.GEN_USUARIOSVEHICULO.Find(id);
            if (gEN_USUARIOSVEHICULO == null)
            {
                return HttpNotFound();
            }
            return View(gEN_USUARIOSVEHICULO);
        }

        // POST: UsuariosVehiculo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        private ActionResult Edit([Bind(Include = "ID,IDUSUARIO,IDMATRICULA,ROWID")] GEN_USUARIOSVEHICULO gEN_USUARIOSVEHICULO)
        {
            if (ModelState.IsValid)
            {
				using (repo = new GenericRepository<LigalEntities, GEN_USUARIOSVEHICULO>())
                {
                    repo.Update(gEN_USUARIOSVEHICULO);
                    repo.Save();
                }
                return RedirectToAction("Index");
            }
            return View(gEN_USUARIOSVEHICULO);
        }

        // POST: UsuariosVehiculo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public void DeleteConfirmed(int id)
        {
            GEN_USUARIOSVEHICULO gEN_USUARIOSVEHICULO = db.GEN_USUARIOSVEHICULO.Find(id);
            using (repo = new GenericRepository<LigalEntities, GEN_USUARIOSVEHICULO>())
            {
                repo.Delete(gEN_USUARIOSVEHICULO);
                repo.Save();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult getElement()
        {
            UsuariosVehiculoVM vista = new UsuariosVehiculoVM();
            
            vista.listaUsuarios = new GenericRepository<LigalEntities, gen_usuarios>().getTodo();

            return View(vista);
        }
    }
}
