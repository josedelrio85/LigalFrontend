using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using LigalFrontend.Models;
using PagedList;
using LigalFrontend.DAL;
using LigalFrontend.ViewModels;
using LigalFrontend.Helpers;
using LigalFrontend.Models.Buscador;
using System;
using System.Linq;

namespace LigalFrontend.Controllers
{
    public class VehiculoController : Controller
    {
        private VehiculoRepo repo = new VehiculoRepo();

        private int page = Functions.Globals.page;
        private int pageSizeBig = Functions.Globals.pageSizeBig;

        // GET: Vehiculo
        public ActionResult Index(int? page)
        {
            List<VehiculoVM> index = repo.getAll();
            var pageNumber = page ?? 1;
            var onePage = index.ToPagedList(pageNumber, pageSizeBig);
            ViewBag.controlador = "Vehiculo";

            return View(onePage);
        }

        // GET: Vehiculo/Create
        public ActionResult Create()
        {
            VehiculoVM vista = new VehiculoVM();
            
            ViewBag.Rol = Functions.Functions.getUserType();
            if (ViewBag.Rol != "WADMIN")
            {
                vista.listaUsuariosVehiculo = new List<UsuariosVehiculoVM>();
                UsuariosVehiculoVM uv = new UsuariosVehiculoVM();
                var userid = Functions.Functions.getUserId();
                uv.usuario = new GenericRepository<LigalEntities, gen_usuarios>().getById(x => x.ID == userid).SingleOrDefault();
                vista.listaUsuariosVehiculo.Add(uv);
            }

            return View(vista);
        }

        // POST: Vehiculo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VehiculoVM vm)
        {
            if (ModelState.IsValid)
            {
                repo.Insert(vm);
                repo.Save();

                almacenaUsuarioVehiculoContent(vm.vehiculo.ID);

                return RedirectToAction("Index");
            }

            return View(vm);
        }

        // GET: Vehiculo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehiculoVM vista = repo.getById(id);
            vista.listaUsuariosVehiculo = (List<UsuariosVehiculoVM>) repo.getUsuariosVehiculo(vista.vehiculo.ID);
            ViewBag.Rol = Functions.Functions.getUserType();

            if (vista == null)
            {
                return HttpNotFound();
            }
            return View(vista);
        }

        // POST: Vehiculo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VehiculoVM vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.InvariantCulture;
                    DateTime newDate = System.Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd h:mm tt"));
                    DateTime result = vm.vehiculo.ANHOCOMPRA ?? DateTime.Now;

                }
                catch (FormatException)
                {
                    Console.WriteLine("{0} is not in the correct format.", vm.vehiculo.ANHOCOMPRA.ToString());
                }

                repo.Update(vm);
                repo.Save();

                almacenaUsuarioVehiculoContent(vm.vehiculo.ID);

                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // POST: Vehiculo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public void DeleteConfirmed(int id)
        {
            VehiculoVM vm = repo.getById(id);
            repo.Delete(vm);

            repo.DeleteUsuariosVehiculoVinculados(id);

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
        public ActionResult Filtro(buscadorVehiculo buscador, int pagina = 0)
        {
            List<VehiculoVM> index = (List<VehiculoVM>)repo.getByParametro(buscador);

            var pageNumber = pagina == 0 ? page : pagina;
            var onePage = index.ToPagedList(pageNumber, pageSizeBig);
            ViewBag.controlador = "Vehiculo";

            if (Request.IsAjaxRequest())
            {
                return PartialView("Filtro", onePage);
            }
            else
            {
                return View(onePage);
            }
        }


        [HttpPost]
        [ValidateHeaderAntiForgeryToken]
        public ActionResult Orden(buscadorVehiculo buscador, objetoOrdenMapa paramOrden, int direccion, int pagina = 0)
        {
            IEnumerable<VehiculoVM> resulFiltro = (List<VehiculoVM>)repo.getSorted(buscador, paramOrden, direccion);

            var pageNumber = pagina == 0 ? page : pagina;
            var onePage = resulFiltro.ToPagedList(pageNumber, pageSizeBig);
            ViewBag.controlador = "Vehiculo";

            if (Request.IsAjaxRequest())
            {
                return PartialView("Filtro", onePage);
            }
            else
            {
                return View(onePage);
            }
        }


        protected void almacenaUsuarioVehiculoContent(int idMatricula)
        {
            string idsUsuario = (String.IsNullOrEmpty(Request.Form["usuVehiculo.IDUSUARIO"])) ? Request.Form["usuario.ID"] : Request.Form["usuVehiculo.IDUSUARIO"];
            

            if (!String.IsNullOrEmpty(idsUsuario))
            {
                string[] separatingChars = { "," };
                List<string> ids = new List<string>();

                if (idsUsuario.Contains(","))
                {
                    ids = idsUsuario.Split(separatingChars, System.StringSplitOptions.None).ToList();
                }
                else
                {
                    ids.Add(idsUsuario);
                }

                GenericRepository<LigalEntities, GEN_USUARIOSVEHICULO> repoUV = new GenericRepository<LigalEntities, GEN_USUARIOSVEHICULO>();
                for (int i = 0; i < ids.Count; i++)
                {
                    UsuariosVehiculoVM uvvm = new UsuariosVehiculoVM();
                    uvvm.usuVehiculo.IDMATRICULA = idMatricula;
                    uvvm.usuVehiculo.IDUSUARIO = Int32.Parse(ids[i]);
                    uvvm.usuVehiculo.ROWID = Guid.NewGuid().ToString();
                    repoUV.Insert(uvvm.usuVehiculo);
                }
                repoUV.Save();
            }
        }
    }
}
