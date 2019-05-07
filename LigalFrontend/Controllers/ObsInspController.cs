using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using LigalFrontend.Models;
using PagedList;
using LigalFrontend.DAL;


namespace LigalFrontend.Controllers
{
    public class ObsInspController : Controller
    {
        private LigalEntities db = new LigalEntities();
		private GenericRepository<LigalEntities, gen_ObsInsp> repo;
		private int page = Functions.Globals.page;
		private int pageSizeBig = Functions.Globals.pageSizeBig;

        // GET: ObsInsp
        public ActionResult Index(int? page)
        {
			
			repo = new GenericRepository<LigalEntities, gen_ObsInsp>();
            var pageNumber = page ?? 1;
            var onePage = repo.getTodo().OrderBy(x => x.ID).ToPagedList(pageNumber, pageSizeBig);
            ViewBag.controlador = "ObsInsp";

            return View(onePage);
        }

        // GET: ObsInsp/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ObsInsp/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,OBSERVA,ROWID")] gen_ObsInsp gen_ObsInsp)
        {
            if (ModelState.IsValid)
            {
				using (repo = new GenericRepository<LigalEntities, gen_ObsInsp>())
                {
                    gen_ObsInsp.ROWID = System.Guid.NewGuid().ToString();
                    repo.Insert(gen_ObsInsp);
                    repo.Save();
                }
                return RedirectToAction("Index");
            }

            return View(gen_ObsInsp);
        }

        // GET: ObsInsp/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            gen_ObsInsp gen_ObsInsp = db.gen_ObsInsp.Find(id);
            if (gen_ObsInsp == null)
            {
                return HttpNotFound();
            }
            return View(gen_ObsInsp);
        }

        // POST: ObsInsp/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,OBSERVA,ROWID")] gen_ObsInsp gen_ObsInsp)
        {
            if (ModelState.IsValid)
            {
				using (repo = new GenericRepository<LigalEntities, gen_ObsInsp>())
                {
                    repo.Update(gen_ObsInsp);
                    repo.Save();
                }
                return RedirectToAction("Index");
            }
            return View(gen_ObsInsp);
        }

        // GET: ObsInsp/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            gen_ObsInsp gen_ObsInsp = db.gen_ObsInsp.Find(id);
            if (gen_ObsInsp == null)
            {
                return HttpNotFound();
            }
            return View(gen_ObsInsp);
        }

        // POST: ObsInsp/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            gen_ObsInsp gen_ObsInsp = db.gen_ObsInsp.Find(id);
            using (repo = new GenericRepository<LigalEntities, gen_ObsInsp>())
            {
                repo.Delete(gen_ObsInsp);
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
