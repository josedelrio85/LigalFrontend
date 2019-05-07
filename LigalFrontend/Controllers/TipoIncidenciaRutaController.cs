using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using LigalFrontend.Models;
using PagedList;
using LigalFrontend.DAL;

namespace LigalFrontend.Controllers
{
    public class TipoIncidenciaRutaController : Controller
    {
        private LigalEntities db = new LigalEntities();
		private GenericRepository<LigalEntities, GEN_TIPOINCIDENCIARUTA> repo;
		private int page = Functions.Globals.page;
		private int pageSizeBig = Functions.Globals.pageSizeBig;

        // GET: TipoIncidenciaRuta
        public ActionResult Index(int? page)
        {
			
			repo = new GenericRepository<LigalEntities, GEN_TIPOINCIDENCIARUTA>();
            var pageNumber = page ?? 1;
            var onePage = repo.getTodo().OrderBy(x => x.ID).ToPagedList(pageNumber, pageSizeBig);
            ViewBag.controlador = "TipoIncidenciaRuta";

            return View(onePage);
        }

        // GET: TipoIncidenciaRuta/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoIncidenciaRuta/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,INCIDENCIA,ROWID")] GEN_TIPOINCIDENCIARUTA gEN_TIPOINCIDENCIARUTA)
        {
            if (ModelState.IsValid)
            {
				using (repo = new GenericRepository<LigalEntities, GEN_TIPOINCIDENCIARUTA>())
                {
                    gEN_TIPOINCIDENCIARUTA.ROWID = System.Guid.NewGuid().ToString();
                    repo.Insert(gEN_TIPOINCIDENCIARUTA);
                    repo.Save();
                }
                return RedirectToAction("Index");
            }

            return View(gEN_TIPOINCIDENCIARUTA);
        }

        // GET: TipoIncidenciaRuta/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GEN_TIPOINCIDENCIARUTA gEN_TIPOINCIDENCIARUTA = db.GEN_TIPOINCIDENCIARUTA.Find(id);
            if (gEN_TIPOINCIDENCIARUTA == null)
            {
                return HttpNotFound();
            }
            return View(gEN_TIPOINCIDENCIARUTA);
        }

        // POST: TipoIncidenciaRuta/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,INCIDENCIA,ROWID")] GEN_TIPOINCIDENCIARUTA gEN_TIPOINCIDENCIARUTA)
        {
            if (ModelState.IsValid)
            {
				using (repo = new GenericRepository<LigalEntities, GEN_TIPOINCIDENCIARUTA>())
                {
                    repo.Update(gEN_TIPOINCIDENCIARUTA);
                    repo.Save();
                }
                return RedirectToAction("Index");
            }
            return View(gEN_TIPOINCIDENCIARUTA);
        }

        // GET: TipoIncidenciaRuta/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GEN_TIPOINCIDENCIARUTA gEN_TIPOINCIDENCIARUTA = db.GEN_TIPOINCIDENCIARUTA.Find(id);
            if (gEN_TIPOINCIDENCIARUTA == null)
            {
                return HttpNotFound();
            }
            return View(gEN_TIPOINCIDENCIARUTA);
        }

        // POST: TipoIncidenciaRuta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GEN_TIPOINCIDENCIARUTA gEN_TIPOINCIDENCIARUTA = db.GEN_TIPOINCIDENCIARUTA.Find(id);
            using (repo = new GenericRepository<LigalEntities, GEN_TIPOINCIDENCIARUTA>())
            {
                repo.Delete(gEN_TIPOINCIDENCIARUTA);
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
