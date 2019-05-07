using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using LigalFrontend.Models;
using PagedList;
using LigalFrontend.DAL;
using LigalFrontend.ViewModels;
using LigalFrontend.Helpers;
using LigalFrontend.Models.Buscador;

namespace LigalFrontend.Controllers
{
    public class TratamientoController : Controller
    {
        private TratamientoRepo repo = new TratamientoRepo();

        private int page = Functions.Globals.page;
        private int pageSizeBig = Functions.Globals.pageSizeBig;

        // GET: Tratamiento
        public ActionResult Index(int? page)
        {
            List<TratamientoVM> index = repo.getAll();
            var pageNumber = page ?? 1;
            var onePage = index.ToPagedList(pageNumber, pageSizeBig);
            ViewBag.controlador = "Tratamiento";

            return View(onePage);
        }

        // GET: Tratamiento/Create
        public ActionResult Create()
        {
            TratamientoVM vista = new TratamientoVM();
            vista.listaAntibioticos = (List<gen_antibioticos>) new GenericRepository<LigalEntities, gen_antibioticos>().getTodo();

            return View(vista);
        }

        // POST: Tratamiento/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TratamientoVM vm)
        {
            if (ModelState.IsValid)
            {
                repo.Insert(vm);
                repo.Save();
                return RedirectToAction("Index");
            }

            return View(vm);
        }

        // GET: Tratamiento/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TratamientoVM vista = repo.getById(id);
            vista.listaAntibioticos = (List<gen_antibioticos>)new GenericRepository<LigalEntities, gen_antibioticos>().getTodo();

            if (vista == null)
            {
                return HttpNotFound();
            }
            return View(vista);
        }

        // POST: Tratamiento/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TratamientoVM vm)
        {
            if (ModelState.IsValid)
            {
                repo.Update(vm);
                repo.Save();
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // POST: Tratamiento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public void DeleteConfirmed(int id)
        {
            TratamientoVM vm = repo.getById(id);
            repo.Delete(vm);
            repo.Save();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult getElement()
        {
            TratamientoVM vista = new TratamientoVM();
            vista.tratamiento.ID = 0;
            vista.tratamiento.FECHAT = System.DateTime.Today;

            vista.listaAntibioticos = new GenericRepository<LigalEntities, gen_antibioticos>().getTodo();

            return View(vista);
        }
    }
}
