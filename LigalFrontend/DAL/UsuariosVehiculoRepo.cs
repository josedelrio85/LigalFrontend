using LigalFrontend.Models;
using LigalFrontend.Models.Buscador;
using LigalFrontend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LigalFrontend.DAL
{
    public class UsuariosVehiculoRepo : IViewModelRepository<GEN_USUARIOSVEHICULO, ClaseDummy>, IDisposable
    {
        private LigalEntities context;
        private GenericRepository<LigalEntities, GEN_USUARIOSVEHICULO> repo;
        private IEnumerable<GEN_USUARIOSVEHICULO> vm;

        public UsuariosVehiculoRepo()
        {
            context = new LigalEntities();
            repo = new GenericRepository<LigalEntities, GEN_USUARIOSVEHICULO>();
        }

        public IEnumerable<GEN_USUARIOSVEHICULO> consultaBase()
        {
            var sql = from uv in context.GEN_USUARIOSVEHICULO
                      select uv;
            return sql;
        }
        
        public GEN_USUARIOSVEHICULO getById(int? id)
        {
            IQueryable< GEN_USUARIOSVEHICULO> vm = consultaBase().AsQueryable();
            GEN_USUARIOSVEHICULO uv = vm.Where(x => x.ID == id).SingleOrDefault();
            return uv;
        }

        public GEN_USUARIOSVEHICULO getByIdUsuario(int? idUsuario)
        {
            IQueryable< GEN_USUARIOSVEHICULO> vm = consultaBase().AsQueryable();
            GEN_USUARIOSVEHICULO uv = vm.Where(x => x.IDUSUARIO == idUsuario).OrderByDescending(x => x.ID).SingleOrDefault();
            return uv;
        }

        public GEN_USUARIOSVEHICULO getByIdMatricula(int? idMatricula)
        {
            IQueryable< GEN_USUARIOSVEHICULO> vm = consultaBase().AsQueryable();
            GEN_USUARIOSVEHICULO uv = vm.Where(x => x.IDMATRICULA == idMatricula).OrderByDescending(x => x.ID).SingleOrDefault();
            return uv;
        }

        public GEN_USUARIOSVEHICULO getByIdUsuarioAndIdMatricula(int idUsuario, int idMatricula)
        {
            var vm = consultaBase().AsQueryable();
            return vm.Where(x => x.IDUSUARIO == idUsuario)
                .Where(x => x.IDMATRICULA == idMatricula).SingleOrDefault();
        }

        public IEnumerable<GEN_USUARIOSVEHICULO> getByParametro(ClaseDummy buscador)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<gen_usuarios> getListaUsuariosConVehiculoAsignado()
        {
            //select usuarios.*
            //from gen_usuarios usuarios
            //where usuarios.ID IN(SELECT uv.idusuario from gen_usuariosvehiculo uv)

            //int[] productList = new int[] { 1, 2, 3, 4 };
            //var myProducts = from p in db.Products
            //                 where productList.Contains(p.ProductID)
            //                 select p;

            var sql = from uv in context.GEN_USUARIOSVEHICULO
                      select uv.IDUSUARIO;

            var sql2 = from usu in context.gen_usuarios
                       where sql.ToList().Contains(usu.ID)
                       select usu;

            return sql2.ToList();
        }

        public List<GEN_VEHICULO> getVehiculosUsuario(int? idUsuario)
        {
            var sql = from usuveh in context.GEN_USUARIOSVEHICULO
                      join veh in context.GEN_VEHICULO on usuveh.IDMATRICULA equals veh.ID into JoinedVehUsuveh
                      from veh in JoinedVehUsuveh.DefaultIfEmpty()
                      where usuveh.IDUSUARIO == idUsuario
                      select veh;

            return sql.OrderBy(x => x.ID).ToList();
        }

        public void Insert(GEN_USUARIOSVEHICULO vm)
        {
            vm.ROWID = Guid.NewGuid().ToString();
            repo.Insert(vm);
        }

        public void Update(GEN_USUARIOSVEHICULO vm)
        {
            repo.Update(vm);
        }

        public void Delete(GEN_USUARIOSVEHICULO vm)
        {
            repo.Delete(vm);
        }

        public void Save()
        {
            repo.Save();
        }

        public void Dispose()
        {
            repo.Dispose();
        }
    }
}