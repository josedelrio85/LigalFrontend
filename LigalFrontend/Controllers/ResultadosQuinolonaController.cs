using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using LigalFrontend.Models;
using PagedList;
using LigalFrontend.DAL;


namespace LigalFrontend.Controllers
{
    public class ResultadosQuinolonaController : Controller
    {
        private LigalEntities db = new LigalEntities();
		private GenericRepository<LigalEntities, GEN_RESULTADOQUINOLONAS> repo;
		private int page = Functions.Globals.page;
		private int pageSizeBig = Functions.Globals.pageSizeBig;

        // GET: Resultados
        public ActionResult Index(int? page)
        {
			
			repo = new GenericRepository<LigalEntities, GEN_RESULTADOQUINOLONAS>();
            var pageNumber = page ?? 1;
            var onePage = repo.getTodo().OrderBy(x => x.ID).ToPagedList(pageNumber, pageSizeBig);
            ViewBag.controlador = "Resultados";

            return View(onePage);
        }

        // GET: Resultados/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Resultados/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DESCRIPCION,TIPO,ROWID")] GEN_RESULTADOQUINOLONAS GEN_RESULTADOQUINOLONAS)
        {
            if (ModelState.IsValid)
            {
				using (repo = new GenericRepository<LigalEntities, GEN_RESULTADOQUINOLONAS>())
                {
                    GEN_RESULTADOQUINOLONAS.ROWID = System.Guid.NewGuid().ToString();
                    repo.Insert(GEN_RESULTADOQUINOLONAS);
                    repo.Save();
                }
                return RedirectToAction("Index");
            }

            return View(GEN_RESULTADOQUINOLONAS);
        }

        // GET: Resultados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GEN_RESULTADOQUINOLONAS GEN_RESULTADOQUINOLONAS = db.GEN_RESULTADOQUINOLONAS.Find(id);
            if (GEN_RESULTADOQUINOLONAS == null)
            {
                return HttpNotFound();
            }
            return View(GEN_RESULTADOQUINOLONAS);
        }

        // POST: Resultados/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DESCRIPCION,TIPO,ROWID")] GEN_RESULTADOQUINOLONAS GEN_RESULTADOQUINOLONAS)
        {
            if (ModelState.IsValid)
            {
				using (repo = new GenericRepository<LigalEntities, GEN_RESULTADOQUINOLONAS>())
                {
                    repo.Update(GEN_RESULTADOQUINOLONAS);
                    repo.Save();
                }
                return RedirectToAction("Index");
            }
            return View(GEN_RESULTADOQUINOLONAS);
        }

        // GET: Resultados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GEN_RESULTADOQUINOLONAS GEN_RESULTADOQUINOLONAS = db.GEN_RESULTADOQUINOLONAS.Find(id);
            if (GEN_RESULTADOQUINOLONAS == null)
            {
                return HttpNotFound();
            }
            return View(GEN_RESULTADOQUINOLONAS);
        }

        // POST: Resultados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GEN_RESULTADOQUINOLONAS GEN_RESULTADOQUINOLONAS = db.GEN_RESULTADOQUINOLONAS.Find(id);
            using (repo = new GenericRepository<LigalEntities, GEN_RESULTADOQUINOLONAS>())
            {
                repo.Delete(GEN_RESULTADOQUINOLONAS);
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
