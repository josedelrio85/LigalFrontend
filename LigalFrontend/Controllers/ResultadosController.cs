using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using LigalFrontend.Models;
using PagedList;
using LigalFrontend.DAL;


namespace LigalFrontend.Controllers
{
    public class ResultadosController : Controller
    {
        private LigalEntities db = new LigalEntities();
		private GenericRepository<LigalEntities, gen_resultados> repo;
		private int page = Functions.Globals.page;
		private int pageSizeBig = Functions.Globals.pageSizeBig;

        // GET: Resultados
        public ActionResult Index(int? page)
        {
			
			repo = new GenericRepository<LigalEntities, gen_resultados>();
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
        public ActionResult Create([Bind(Include = "ID,DESCRIPCION,TIPO,ROWID")] gen_resultados gen_resultados)
        {
            if (ModelState.IsValid)
            {
				using (repo = new GenericRepository<LigalEntities, gen_resultados>())
                {
                    gen_resultados.ROWID = System.Guid.NewGuid().ToString();
                    gen_resultados.TIPO = 1;
                    repo.Insert(gen_resultados);
                    repo.Save();
                }
                return RedirectToAction("Index");
            }

            return View(gen_resultados);
        }

        // GET: Resultados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            gen_resultados gen_resultados = db.gen_resultados.Find(id);
            if (gen_resultados == null)
            {
                return HttpNotFound();
            }
            return View(gen_resultados);
        }

        // POST: Resultados/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DESCRIPCION,TIPO,ROWID")] gen_resultados gen_resultados)
        {
            if (ModelState.IsValid)
            {
				using (repo = new GenericRepository<LigalEntities, gen_resultados>())
                {
                    repo.Update(gen_resultados);
                    repo.Save();
                }
                return RedirectToAction("Index");
            }
            return View(gen_resultados);
        }

        // GET: Resultados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            gen_resultados gen_resultados = db.gen_resultados.Find(id);
            if (gen_resultados == null)
            {
                return HttpNotFound();
            }
            return View(gen_resultados);
        }

        // POST: Resultados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            gen_resultados gen_resultados = db.gen_resultados.Find(id);
            using (repo = new GenericRepository<LigalEntities, gen_resultados>())
            {
                repo.Delete(gen_resultados);
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
