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

namespace LigalFrontend.Controllers
{
    public class observaController : Controller
    {
        private LigalEntities db = new LigalEntities();
        private GenericRepository<LigalEntities, gen_observa> repo;
        private int page = Functions.Globals.page;
        private int pageSizeBig = Functions.Globals.pageSizeBig;

        // GET: observa
        public ActionResult Index(int? page)
        {
            repo = new GenericRepository<LigalEntities, gen_observa>();
            var pageNumber = page ?? 1;
            var onePage = repo.getTodo().OrderBy(x => x.ID).ToPagedList(pageNumber, pageSizeBig);
            ViewBag.controlador = "PuntosRecogida";

            return View(onePage);
        }
        
        // GET: observa/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: observa/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,OBSERVA,ROWID")] gen_observa gen_observa)
        {

            if (ModelState.IsValid)
            {
                using (repo = new GenericRepository<LigalEntities, gen_observa>())
                {
                    gen_observa.ROWID = System.Guid.NewGuid().ToString();
                    repo.Insert(gen_observa);
                    repo.Save();
                }
                return RedirectToAction("Index");
            }

            return View(gen_observa);
        }

        // GET: observa/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            gen_observa gen_observa = db.gen_observa.Find(id);
            if (gen_observa == null)
            {
                return HttpNotFound();
            }
            return View(gen_observa);
        }

        // POST: observa/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,OBSERVA,ROWID")] gen_observa gen_observa)
        {
            if (ModelState.IsValid)
            {
                using (repo = new GenericRepository<LigalEntities, gen_observa>())
                {
                    repo.Update(gen_observa);
                    repo.Save();
                }
                return RedirectToAction("Index");
            }
            return View(gen_observa);
        }

        // POST: observa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            gen_observa gen_observa = db.gen_observa.Find(id);
            using (repo = new GenericRepository<LigalEntities, gen_observa>())
            {
                repo.Delete(gen_observa);
                repo.Save();
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
