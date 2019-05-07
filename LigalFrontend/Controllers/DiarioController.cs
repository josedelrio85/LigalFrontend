using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using LigalFrontend.ViewModels;
using PagedList;
using LigalFrontend.DAL;
using LigalFrontend.Models.Buscador;
using LigalFrontend.Models;
using LigalFrontend.Helpers;

namespace LigalFrontend.Controllers
{
    public class DiarioController : Controller
    {
        private DiarioRepo repo = new DiarioRepo();

        private int page = Functions.Globals.page;
        private int pageSizeBig = Functions.Globals.pageSizeBig;

        // GET: Diario
        public ActionResult Index(int? page)
        {
            DiarioBuscadorVM diarioBusca = new DiarioBuscadorVM();
            List<DiarioMatriculaVM> index;

            if (Functions.Functions.getUserType() == "WADMIN")
            {
                index = repo.getAll();
            }else
            {
                buscadorDiario bd = new buscadorDiario();
                bd.idUsuario = Functions.Functions.getUserId().ToString();
                index = (List<DiarioMatriculaVM>) repo.getByParametro(bd);
            }

            var pageNumber = page ?? 1;
            diarioBusca.diariovm = index.ToPagedList(pageNumber, pageSizeBig);

            ViewBag.nombreAction = "Index";

            return View( diarioBusca);
        }

        // GET: Diario/Create
        public ActionResult Create()
        {
            DiarioMatriculaVM vista = new DiarioMatriculaVM();

            UsuariosVehiculoRepo uvRepo = new UsuariosVehiculoRepo();
            ViewBag.Rol = Functions.Functions.getUserType();

            if(ViewBag.Rol == "WADMIN")
            {
                //obtener lista de usuarios que estén vinculados a algún vehiculo
                vista.listaUsuarios = (List<gen_usuarios>)uvRepo.getListaUsuariosConVehiculoAsignado();
                vista.listaVehiculos = new List<GEN_VEHICULO>();
            }else
            {
                //modo usuario
                int userid = Functions.Functions.getUserId();
                vista.listaUsuarios = new List<gen_usuarios>();
                vista.listaVehiculos = uvRepo.getVehiculosUsuario(userid);
                vista.map_idUsuario = userid;
            }
            
            vista.listaTareas = new GenericRepository<LigalEntities, GEN_TAREASDIARIO>().getTodo();

            return View(vista);
        }

        // POST: Diario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DiarioMatriculaVM vm)
        {
            if(vm.diario.IDUV == null)
            {
                UsuariosVehiculoRepo uvRepo = new UsuariosVehiculoRepo();
                GEN_USUARIOSVEHICULO uv = uvRepo.getByIdUsuarioAndIdMatricula(vm.map_idUsuario, vm.map_idMatricula);
                vm.diario.IDUV = uv.ID;
            }

            if (ModelState.IsValid)
            {
                repo.Insert(vm);
                repo.Save();
                return RedirectToAction("Index");
            }

            return View(vm);
        }

        // GET: Diario/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DiarioMatriculaVM vista = repo.getById(id);

            vista.listaTareas = new GenericRepository<LigalEntities, GEN_TAREASDIARIO>().getTodo();

            if (vista == null)
            {
                return HttpNotFound();
            }
            return View(vista);
        }

        // POST: Diario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DiarioMatriculaVM vm)
        {
            if (ModelState.IsValid)
            {
                repo.Update(vm);
                repo.Save();
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // POST: Diario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public void DeleteConfirmed(int id)
        {
            DiarioMatriculaVM vm = repo.getById(id);
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
        public ActionResult Filtro(buscadorDiario buscador, int pagina = 0)
        {
            var pageNumber = pagina == 0 ? page : pagina;
            DiarioBuscadorVM diarioBusca = new DiarioBuscadorVM();
            List<DiarioMatriculaVM> index = (List<DiarioMatriculaVM>)repo.getByParametro(buscador);
            diarioBusca.diariovm = index.ToPagedList(pageNumber, pageSizeBig);

            ViewBag.controlador = "Diario";

            if (Request.IsAjaxRequest())
            {
                return PartialView("Filtro", diarioBusca);
            }
            else
            {
                return View(diarioBusca);
            }
        }


        [HttpPost]
        [ValidateHeaderAntiForgeryToken]
        public ActionResult Orden(buscadorDiario buscador, objetoOrdenMapa paramOrden, int direccion, int pagina = 0)
        {
            DiarioBuscadorVM diarioBusca = new DiarioBuscadorVM();
            IEnumerable<DiarioMatriculaVM> resulFiltro = (List<DiarioMatriculaVM>)repo.getSorted(buscador, paramOrden, direccion);

            var pageNumber = pagina == 0 ? page : pagina;
            diarioBusca.diariovm = resulFiltro.ToPagedList(pageNumber, pageSizeBig);
            ViewBag.controlador = "Diario";
            
            if (Request.IsAjaxRequest())
            {
                return PartialView("Filtro", diarioBusca);
            }
            else
            {
                return View(diarioBusca);
            }
        }


        [HttpPost]
        public string getSelectVehiculosUsuario(int? idUsuario)
        {
            List<GEN_VEHICULO> lista = new UsuariosVehiculoRepo().getVehiculosUsuario(idUsuario);
            List<itemJson> l = new List<itemJson>();

            foreach(GEN_VEHICULO uv in lista)
            {
                itemJson item = new itemJson(uv.ID, uv.MATRICULA);
                l.Add(item);
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(l);
        }

        [HttpPost]
        public string getIDUV(int idUsuario, int idMatricula)
        {
            if (idUsuario > 0 && idMatricula > 0)
            {
                UsuariosVehiculoRepo uvRepo = new UsuariosVehiculoRepo();
                GEN_USUARIOSVEHICULO uv = uvRepo.getByIdUsuarioAndIdMatricula(idUsuario, idMatricula);

                DiarioRepo dRepo = new DiarioRepo();
                GEN_DIARIOMATRICULA dm = dRepo.getByIdUV(uv.ID);
                
                return Newtonsoft.Json.JsonConvert.SerializeObject(dm);
            }
            return null;
        }

        protected class itemJson
        {
            public int id;
            public string mat;

            public itemJson(int i, string m)
            {
                id = i;
                mat = m;
            }
        }
    }
}