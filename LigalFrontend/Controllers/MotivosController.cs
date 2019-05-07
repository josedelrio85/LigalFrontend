using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using LigalFrontend.Models;
using PagedList;
using LigalFrontend.DAL;


namespace LigalFrontend.Controllers
{
    public class MotivosController : Controller
    {
        private LigalEntities db = new LigalEntities();
		private GenericRepository<LigalEntities, gen_motivos> repo;
		private int page = Functions.Globals.page;
		private int pageSizeBig = Functions.Globals.pageSizeBig;

        // GET: motivos
        public ActionResult Index(int? page)
        {
			
			repo = new GenericRepository<LigalEntities, gen_motivos>();
            var pageNumber = page ?? 1;
            var onePage = repo.getTodo().OrderBy(x => x.ID).ToPagedList(pageNumber, pageSizeBig);
            ViewBag.controlador = "motivos";

            return View(onePage);
        }

        // GET: motivos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: motivos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DESCRIPCION,ROWID")] gen_motivos gen_motivos)
        {
            if (ModelState.IsValid)
            {
				using (repo = new GenericRepository<LigalEntities, gen_motivos>())
                {
                    gen_motivos.ROWID = System.Guid.NewGuid().ToString();
                    repo.Insert(gen_motivos);
                    repo.Save();
                }
                return RedirectToAction("Index");
            }

            return View(gen_motivos);
        }

        // GET: motivos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            gen_motivos gen_motivos = db.gen_motivos.Find(id);
            if (gen_motivos == null)
            {
                return HttpNotFound();
            }
            return View(gen_motivos);
        }

        // POST: motivos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DESCRIPCION,ROWID")] gen_motivos gen_motivos)
        {
            if (ModelState.IsValid)
            {
				using (repo = new GenericRepository<LigalEntities, gen_motivos>())
                {
                    repo.Update(gen_motivos);
                    repo.Save();
                }
                return RedirectToAction("Index");
            }
            return View(gen_motivos);
        }

        // GET: motivos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            gen_motivos gen_motivos = db.gen_motivos.Find(id);
            if (gen_motivos == null)
            {
                return HttpNotFound();
            }
            return View(gen_motivos);
        }

        // POST: motivos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            gen_motivos gen_motivos = db.gen_motivos.Find(id);
            using (repo = new GenericRepository<LigalEntities, gen_motivos>())
            {
                repo.Delete(gen_motivos);
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
