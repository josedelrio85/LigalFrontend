﻿using LigalFrontend.DAL;
using LigalFrontend.Helpers;
using LigalFrontend.Models;
using LigalFrontend.Models.Buscador;
using LigalFrontend.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using System.Web.Mvc;

namespace LigalFrontend.Controllers
{
    public class InspeccionesAleatoriasController : Controller
    {
        private InspeccionesRepo repo = new InspeccionesRepo();

        private int page = Functions.Globals.page;
        private int pageSizeBig = Functions.Globals.pageSizeBig;
        private string nAction;

        // GET: InspeccionesAleatorias
        public ActionResult Index(int? page)
        {
            var pageNumber = page ?? 1;
            InspBuscadorVM inspBusca = new InspBuscadorVM();
            List<InspeccionesVM> index = (List<InspeccionesVM>)repo.getInspeccionesAleatorias();

            inspBusca.inspecvm = index.ToPagedList(pageNumber, pageSizeBig);

            ViewBag.nombreAction = "Index";
            ViewBag.Title = "Inspecciones Aleatorias";

            return View("~/Views/Inspecciones/Index.cshtml", inspBusca);
        }

        // GET: InspeccionesAleatorias/IndexCompleto
        public ActionResult IndexCompleto(int? page)
        {
            var pageNumber = page ?? 1;
            InspBuscadorVM inspBusca = new InspBuscadorVM();
            List<InspeccionesVM> index = (List<InspeccionesVM>)repo.getTodasInspeccionesAleatorias();

            inspBusca.inspecvm = index.ToPagedList(pageNumber, pageSizeBig);

            ViewBag.nombreAction = "IndexCompleto";
            ViewBag.Title = "Inspecciones Aleatorias";

            return View("~/Views/Inspecciones/Index.cshtml", inspBusca);
        }

        // GET: InspeccionesAleatorias/Create
        public ActionResult Create()
        {
            InspeccionesVM vista = new InspeccionesVM();

            vista.listaResultados = new GenericRepository<LigalEntities, gen_resultados>().getTodo().ToList();
            vista.listaResultadosQuinolonas = new GenericRepository<LigalEntities, GEN_RESULTADOQUINOLONAS>().getTodo().ToList();
            vista.listaObservaciones = new GenericRepository<LigalEntities, gen_ObsInsp>().getTodo().ToList();
            vista.listaUsuarios = new UsuariosRepo().getListaUsuarios();

            vista.map_IdResultadoCharm = getIdResultadoSelect(vista, 1);
            vista.map_IdResultadoQuino = getIdResultadoSelect(vista, 2);

            vista.listaTratamientos = new TratamientoRepo().getByIdInspeccion(vista.inspeccion.ID);

            vista.inspeccion.TipoInspeccion = "1";

            return View("~/Views/Inspecciones/Create.cshtml", vista);
        }

        // POST: InspeccionesAleatorias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public string Create(InspeccionesVM vm)
        {
            if (ModelState.IsValid)
            {
                repo.Insert(vm);
                repo.Save();

                almacenaTratamientoContent(vm.inspeccion.ID);

                return this.Url.Action("Index");
            }

            return this.Url.Action("Create");
        }

        // GET: InspeccionesAleatorias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InspeccionesVM vista = repo.getById(id);
            if (vista == null)
            {
                return HttpNotFound();
            }

            vista.listaResultados = new GenericRepository<LigalEntities, gen_resultados>().getTodo().ToList();
            vista.listaResultadosQuinolonas = new GenericRepository<LigalEntities, GEN_RESULTADOQUINOLONAS>().getTodo().ToList();
            vista.listaObservaciones = new GenericRepository<LigalEntities, gen_ObsInsp>().getTodo().ToList();
            vista.listaUsuarios = new UsuariosRepo().getListaUsuarios();

            vista.map_IdResultadoCharm = getIdResultadoSelect(vista, 1);
            vista.map_IdResultadoQuino = getIdResultadoSelect(vista, 2);

            vista.listaTratamientos = new TratamientoRepo().getByIdInspeccion(vista.inspeccion.ID);

            return View("~/Views/Inspecciones/Edit.cshtml", vista);
        }

        // POST: InspeccionesAleatorias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public string Edit(InspeccionesVM vm)
        {
            if (ModelState.IsValid)
            {
                almacenaTratamientoContent(vm.inspeccion.ID);

                repo.Update(vm);
                repo.Save();
                return this.Url.Action("Index");
            }
            return this.Url.Action("Edit");
        }

        // POST: InspeccionesAleatorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public void DeleteConfirmed(int id)
        {
            InspeccionesVM vm = repo.getById(id);
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

        [HttpPost]
        [ValidateHeaderAntiForgeryToken]
        public ActionResult Filtro(buscadorInspecciones buscador, int pagina = 0, string nombreAction = "")
        {
            var pageNumber = pagina == 0 ? page : pagina;
            nAction = nombreAction;
            InspBuscadorVM inspBusca = new InspBuscadorVM();
            List<InspeccionesVM> index = (List<InspeccionesVM>)repo.getByParametro(buscador, getTipoConsulta());

            inspBusca.inspecvm = index.ToPagedList(pageNumber, pageSizeBig);

            return PartialView("~/Views/Inspecciones/Filtro.cshtml", inspBusca);
        }


        [HttpPost]
        [ValidateHeaderAntiForgeryToken]
        public ActionResult Orden(buscadorInspecciones buscador, objetoOrdenMapa paramOrden, int direccion, int pagina = 0, string nombreAction = "")
        {
            var pageNumber = pagina == 0 ? page : pagina;
            nAction = nombreAction;
            InspBuscadorVM inspBusca = new InspBuscadorVM();

            IEnumerable<InspeccionesVM> index = (List<InspeccionesVM>)repo.getSorted(buscador, paramOrden, direccion, getTipoConsulta());

            inspBusca.inspecvm = index.ToPagedList(pageNumber, pageSizeBig);

            return PartialView("~/Views/Inspecciones/Filtro.cshtml", inspBusca);
        }


        protected void almacenaTratamientoContent(int idInsp)
        {
            if (!String.IsNullOrEmpty(Request.Form["tratamiento.FECHAT"]) || !String.IsNullOrEmpty(Request.Form["tratamiento.IDTRATA"]))
            {
                var fTrata = Request.Form["tratamiento.FECHAT"];
                var idTrata = Request.Form["tratamiento.IDTRATA"];
                string[] separatingChars = { "," };
                List<string> fechas = new List<string>();
                List<string> ids = new List<string>();

                if (fTrata.Contains(","))
                {
                    fechas = fTrata.Split(separatingChars, System.StringSplitOptions.None).ToList();
                }
                else
                {
                    fechas.Add(fTrata);
                }

                if (idTrata.Contains(","))
                {
                    ids = idTrata.Split(separatingChars, System.StringSplitOptions.None).ToList();
                }
                else
                {
                    ids.Add(idTrata);
                }

                int tamMax = fechas.Count > ids.Count ? fechas.Count : ids.Count;

                TratamientoRepo trataRepo = new TratamientoRepo();
                for (int i = 0; i < tamMax; i++)
                {
                    TratamientoVM trat = new TratamientoVM();
                    trat.tratamiento.FECHAT = Functions.Functions.textoToFecha(fechas[i]);
                    trat.tratamiento.IDTRATA = Int32.Parse(ids[i]);
                    trat.tratamiento.IDINSP = idInsp;
                    trataRepo.Insert(trat);
                }
                trataRepo.Save();
            }
        }

        private int getTipoConsulta()
        {
            int tipoConsulta = 3;
            if (nAction.Equals("IndexCompleto", StringComparison.InvariantCultureIgnoreCase))
            {
                tipoConsulta = 4;
            }
            return tipoConsulta;
        }

        protected int getIdResultadoSelect(InspeccionesVM insp, int tipoResul)
        {
            if (tipoResul == 1)
            {
                foreach (gen_resultados r in insp.listaResultados)
                {
                    if (r.DESCRIPCION.Equals(insp.inspeccion.ResultadoCHARM, StringComparison.OrdinalIgnoreCase))
                    {
                        return r.ID;
                    }
                }
            }
            else if (tipoResul == 2)
            {
                foreach (GEN_RESULTADOQUINOLONAS r in insp.listaResultadosQuinolonas)
                {
                    if (r.DESCRIPCION.Equals(insp.inspeccion.RESULTADOQUINO, StringComparison.OrdinalIgnoreCase))
                    {
                        return r.ID;
                    }
                }
            }
            return 0;
        }

    }
}