using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using LigalFrontend.Models;
using PagedList;
using LigalFrontend.DAL;


namespace LigalFrontend.Controllers
{
    public class ProductosController : Controller
    {
        private LigalEntities db = new LigalEntities();
		private GenericRepository<LigalEntities, gen_productos> repo;
		private int page = Functions.Globals.page;
		private int pageSizeBig = Functions.Globals.pageSizeBig;

        // GET: Productos
        public ActionResult Index(int? page)
        {
			repo = new GenericRepository<LigalEntities, gen_productos>();
            var pageNumber = page ?? 1;
            var onePage = repo.getTodo().OrderBy(x => x.ID).ToPagedList(pageNumber, pageSizeBig);
            ViewBag.controlador = "Productos";

            return View(onePage);
        }

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
        public ActionResult Create([Bind(Include = "ID,CODIGOPRO,PRODUCTO,ROWID")] gen_productos gen_productos)
        {
            if (ModelState.IsValid)
            {
                using (repo = new GenericRepository<LigalEntities, gen_productos>())
                {
                    gen_productos.ROWID = System.Guid.NewGuid().ToString();
                    repo.Insert(gen_productos);
                    repo.Save();
                }
                return RedirectToAction("Index");
            }

            return View(gen_productos);
        }

        // GET: Productos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            gen_productos gen_productos = db.gen_productos.Find(id);
            if (gen_productos == null)
            {
                return HttpNotFound();
            }
            return View(gen_productos);
        }

        // POST: Productos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CODIGOPRO,PRODUCTO,ROWID")] gen_productos gen_productos)
        {
            if (ModelState.IsValid)
            {
                using (repo = new GenericRepository<LigalEntities, gen_productos>())
                {
                    repo.Update(gen_productos);
                    repo.Save();
                }
                return RedirectToAction("Index");
            }
            return View(gen_productos);
        }

        // GET: Productos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            gen_productos gen_productos = db.gen_productos.Find(id);
            if (gen_productos == null)
            {
                return HttpNotFound();
            }
            return View(gen_productos);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            gen_productos gen_productos = db.gen_productos.Find(id);
            using (repo = new GenericRepository<LigalEntities, gen_productos>())
            {
                repo.Delete(gen_productos);
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
