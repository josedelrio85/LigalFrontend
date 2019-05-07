using System.Net;
using System.Web.Mvc;
using LigalFrontend.Models;
using LigalFrontend.DAL;
using LigalFrontend.ViewModels;
using System;

namespace LigalFrontend.Controllers
{
    public class ProductosgrController : Controller
    {
        private LigalEntities db = new LigalEntities();
		private GenericRepository<LigalEntities, gen_productosgr> repo;
		private int page = Functions.Globals.page;
		private int pageSizeBig = Functions.Globals.pageSizeBig;


        // GET: Productos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Productos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CODIGOPRO,PRODUCTO,ROWID")] gen_productosgr gen_productosgr)
        {
            if (ModelState.IsValid)
            {
                using (repo = new GenericRepository<LigalEntities, gen_productosgr>())
                {
                    gen_productosgr.ROWID = System.Guid.NewGuid().ToString();
                    repo.Insert(gen_productosgr);
                    repo.Save();
                }
                return RedirectToAction("Index");
            }

            return View(gen_productosgr);
        }

        // GET: Productos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            gen_productosgr gen_productosgr = db.gen_productosgr.Find(id);
            if (gen_productosgr == null)
            {
                return HttpNotFound();
            }
            return View(gen_productosgr);
        }

        // POST: Productos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CODIGOPRO,PRODUCTO,ROWID")] gen_productosgr gen_productosgr)
        {
            if (ModelState.IsValid)
            {
                using (repo = new GenericRepository<LigalEntities, gen_productosgr>())
                {
                    repo.Update(gen_productosgr);
                    repo.Save();
                }
                return RedirectToAction("Index");
            }
            return View(gen_productosgr);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public void DeleteConfirmed(int id)
        {
            gen_productosgr gen_productosgr = db.gen_productosgr.Find(id);
            using (repo = new GenericRepository<LigalEntities, gen_productosgr>())
            {
                repo.Delete(gen_productosgr);
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
            ProductosgrVM vista = new ProductosgrVM();
            vista.productogr.ID = 0;
            vista.productogr.TEMPERATURA = 0;

            vista.listaProductos = new GenericRepository<LigalEntities, gen_productos>().getTodo();

            return View(vista);
        }

    }
}
