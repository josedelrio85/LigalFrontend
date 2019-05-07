using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using LigalFrontend.ViewModels;
using PagedList;
using LigalFrontend.DAL;
using LigalFrontend.Models.Buscador;
using LigalFrontend.Models;
using LigalFrontend.Helpers;
using System;
using System.Linq;

namespace LigalFrontend.Controllers
{
    public class RecogClientesController : Controller
    {
        private RecogClientesRepo repo = new RecogClientesRepo();

        private int page = Functions.Globals.page;
        private int pageSizeBig = Functions.Globals.pageSizeBig;


        // GET: PuntosRecogida/Create
        public ActionResult Create(int id)
        {
            RecogClientesVM vista = new RecogClientesVM();
            vista.recogidasR = new gen_recogidasR();
            vista.recogidasR.IDRECOGIDA = id;
            vista.listaClientes = new GenericRepository<LigalEntities, gen_clientes>().getTodo();
            vista.listaObservaciones = new GenericRepository<LigalEntities, gen_observa>().getTodo();
            vista.listaProductosgr = new List<ProductosgrVM>();

            return View(vista);
        }

        // POST: PuntosRecogida/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RecogClientesVM vm)
        {
            if (ModelState.IsValid)
            {
                repo.Insert(vm);
                repo.Save();
                almacenaProductos(vm.recogidasR.ID);
                return RedirectToAction("Edit", "InsercionRecogidasN", new { id = vm.recogidasR.IDRECOGIDA });
            }

            return View(vm);
        }

        // GET: PuntosRecogida/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            RecogClientesVM vista = repo.getById(id);
            vista.listaClientes = new GenericRepository<LigalEntities, gen_clientes>().getTodo();
            vista.listaObservaciones = new GenericRepository<LigalEntities, gen_observa>().getTodo();
            vista.listaProductosgr = new ProductosgrRepo().getByIdRecogida(vista.recogidasR.ID);

            if (vista == null)
            {
                return HttpNotFound();
            }

            vista.listaImagenes = new ImageSliderVM();

            vista.listaImagenes.lista.Add(vista.recogidasR.FOTO1);
            vista.listaImagenes.lista.Add(vista.recogidasR.FOTO2);
            vista.listaImagenes.lista.Add(vista.recogidasR.FOTO3);
            vista.listaImagenes.lista.Add(vista.recogidasR.VIDEO);

            return View(vista);
        }

        // POST: PuntosRecogida/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RecogClientesVM vm)
        {
            if (ModelState.IsValid)
            {
                almacenaProductos(vm.recogidasR.ID);
                repo.Update(vm);
                repo.Save();
                return RedirectToAction("Index", "InsercionRecogidasN");
            }
            return View(vm);
        }

        // POST: PuntosRecogida/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public void DeleteConfirmed(int id)
        {
            RecogClientesVM vm = repo.getById(id);
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

        protected void almacenaProductos(int idRecogida)
        {
            if (!String.IsNullOrEmpty(Request.Form["productogr.IDPRODUCTO"]) || !String.IsNullOrEmpty(Request.Form["productogr.CANTIDAD"]) || !String.IsNullOrEmpty(Request.Form["productogr.TEMPERATURA"]))
            {
                var idProducto = Request.Form["productogr.IDPRODUCTO"];
                var cantidad = Request.Form["productogr.CANTIDAD"];
                var temperatura = Request.Form["productogr.TEMPERATURA"];
                string[] separatingChars = { "," };
                List<string> cantidades = new List<string>();
                List<string> temperaturas = new List<string>();
                List<string> ids = new List<string>();

                if (idProducto.Contains(","))
                {
                    ids = idProducto.Split(separatingChars, System.StringSplitOptions.None).ToList();
                }
                else
                {
                    ids.Add(idProducto);
                }

                if (cantidad.Contains(","))
                {
                    cantidades = cantidad.Split(separatingChars, System.StringSplitOptions.None).ToList();
                }
                else
                {
                    cantidades.Add(cantidad);
                }

                if (temperatura.Contains(","))
                {
                    temperaturas = temperatura.Split(separatingChars, System.StringSplitOptions.None).ToList();
                }
                else
                {
                    temperaturas.Add(temperatura);
                }

                int tamMax = ids.Count;

                ProductosgrRepo prodgrRepo = new ProductosgrRepo();
                for (int i = 0; i < tamMax; i++)
                {
                    ProductosgrVM pgr = new ProductosgrVM();
                    pgr.productogr.IDPRODUCTO = Int32.Parse(ids[i]);
                    pgr.productogr.IDRECOGIDAN = idRecogida;
                    pgr.productogr.CANTIDAD = Int32.Parse(cantidades[i]);
                    pgr.productogr.TEMPERATURA = Int32.Parse(temperaturas[i]);
                    prodgrRepo.Insert(pgr);
                }
                prodgrRepo.Save();
            }
        }

        public ActionResult getElement()
        {
            RecogClientesVM vista = new RecogClientesVM();

            vista.recogidasR = new gen_recogidasR();

            vista.listaClientes = new GenericRepository<LigalEntities, gen_clientes>().getTodo();
            vista.listaObservaciones = new GenericRepository<LigalEntities, gen_observa>().getTodo();
            vista.listaProductosgr = new List<ProductosgrVM>();

            return View(vista);
        }
    }
}
