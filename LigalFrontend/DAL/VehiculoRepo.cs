using LigalFrontend.Models;
using LigalFrontend.Models.Buscador;
using LigalFrontend.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LigalFrontend.DAL
{
    public class VehiculoRepo : IViewModelRepository<VehiculoVM, buscadorVehiculo>, IDisposable
    {
        private LigalEntities context;
        private GenericRepository<LigalEntities, GEN_VEHICULO> repo;
        private IEnumerable<VehiculoVM> vm;

        public VehiculoRepo()
        {
            context = new LigalEntities();
            repo = new GenericRepository<LigalEntities, GEN_VEHICULO>();
        }

        public IEnumerable<VehiculoVM> consultaBase()
        {
            vm = from veh in context.GEN_VEHICULO
                 //join usuveh in context.GEN_USUARIOSVEHICULO on veh.ID equals usuveh.IDMATRICULA
                 //join usu in context.gen_usuarios on usuveh.IDUSUARIO equals usu.ID
                 select new VehiculoVM
                 {
                     vehiculo = veh
                 };

            return vm;
        }

        public List<VehiculoVM> getAll()
        {
            IQueryable<VehiculoVM> vmq = consultaBase().AsQueryable();
            return vmq.ToList();
        }

        public VehiculoVM getById(int? id)
        {
            IQueryable<VehiculoVM> vmq = consultaBase().AsQueryable();
            VehiculoVM pr = vmq.Where(x => x.vehiculo.ID == id).SingleOrDefault();
            return pr;
        }

        public IEnumerable<VehiculoVM> getByParametro(buscadorVehiculo param)
        {
            IQueryable<VehiculoVM> vmq = consultaBase().AsQueryable();

            if (param.MATRICULA != null)
            {
                string codigo = Functions.Functions.CleanInput(param.MATRICULA.ToString());
                vmq = vmq.Where(x => x.vehiculo.MATRICULA.Contains(codigo));
            }

            if (param.MODELO != null)
            {
                string mode = Functions.Functions.CleanInput(param.MODELO.ToString());
                vmq = vmq.Where(x => x.vehiculo.MODELO.Contains(mode));
            }

            if (param.ANHOCOMPRA != null)
            {
                System.DateTime dIni = Functions.Functions.textoToFecha(param.ANHOCOMPRA);
                vmq = vmq.Where(x => DbFunctions.DiffYears(x.vehiculo.ANHOCOMPRA, dIni) == 0);
            }

            if (param.COMBUSTIBLE != null)
            {
                string combus = Functions.Functions.CleanInput(param.COMBUSTIBLE.ToString());
                vmq = vmq.Where(x => x.vehiculo.TIPOCOMBUSTIBLE.Contains(combus));
            }

            IEnumerable<VehiculoVM> ievm = vmq.ToList();
            return ievm;
        }

        public IEnumerable<VehiculoVM> getSorted(buscadorVehiculo paramFiltro, objetoOrdenMapa paramOrden, int direccion)
        {
            IEnumerable<VehiculoVM> resuFiltro = getByParametro(paramFiltro);

            if (paramOrden.coleccion == "vehiculo" && paramOrden.nombreCampo == "MATRICULA")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.vehiculo.MATRICULA) : resuFiltro.OrderByDescending(x => x.vehiculo.MATRICULA);
            }

            if (paramOrden.coleccion == "vehiculo" && paramOrden.nombreCampo == "MODELO")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.vehiculo.MODELO) : resuFiltro.OrderByDescending(x => x.vehiculo.MODELO);
            }

            if (paramOrden.coleccion == "vehiculo" && paramOrden.nombreCampo == "ANHOCOMPRA")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.vehiculo.ANHOCOMPRA) : resuFiltro.OrderByDescending(x => x.vehiculo.ANHOCOMPRA);
            }

            if (paramOrden.coleccion == "vehiculo" && paramOrden.nombreCampo == "TIPOCOMBUSTIBLE")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.vehiculo.TIPOCOMBUSTIBLE) : resuFiltro.OrderByDescending(x => x.vehiculo.TIPOCOMBUSTIBLE);
            }

            return resuFiltro.ToList();
        }

        public IEnumerable<UsuariosVehiculoVM> getUsuariosVehiculo(int idMatricula)
        {
            var vm = from usveh in context.GEN_USUARIOSVEHICULO
                     join usu in context.gen_usuarios on usveh.IDUSUARIO equals usu.ID
                     where usveh.IDMATRICULA == idMatricula
                     select new UsuariosVehiculoVM
                     {
                         usuVehiculo = usveh,
                         usuario = usu
                     };

            return vm.ToList();
        }

        public void Insert(VehiculoVM vm)
        {
            vm.vehiculo.ROWID = Guid.NewGuid().ToString();
            repo.Insert(vm.vehiculo);
        }

        public void Update(VehiculoVM vm)
        {
            repo.Update(vm.vehiculo);
        }

        public void Delete(VehiculoVM vm)
        {
            repo.Delete(vm.vehiculo);
        }

        public void Save()
        {
            repo.Save();
        }

        public void Dispose()
        {
            repo.Dispose();
        }

        public void DeleteUsuariosVehiculoVinculados(int idMatricula)
        {
            var sql = from usveh in context.GEN_USUARIOSVEHICULO
                     where usveh.IDMATRICULA == idMatricula
                     select usveh;

            IEnumerable<GEN_USUARIOSVEHICULO> lista = sql.ToList();
            GenericRepository<LigalEntities, GEN_USUARIOSVEHICULO> repoUV = new GenericRepository<LigalEntities, GEN_USUARIOSVEHICULO>();
            foreach(GEN_USUARIOSVEHICULO uv in lista)
            {
                repoUV.Delete(uv);
            }
            repoUV.Save();
        }
    }
}