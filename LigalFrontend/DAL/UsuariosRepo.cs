using LigalFrontend.Models;
using LigalFrontend.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System;
using LigalFrontend.Models.Buscador;

namespace LigalFrontend.DAL
{
    public class UsuariosRepo : IViewModelRepository<UsuariosVM, buscadorUsuarios>, IDisposable
    {
        private LigalEntities context;
        private GenericRepository<LigalEntities, gen_usuarios> repo;
        private IEnumerable<UsuariosVM> vm;

        public UsuariosRepo()
        {
            context = new LigalEntities();
            repo = new GenericRepository<LigalEntities, gen_usuarios>();
        }

        public IEnumerable<UsuariosVM> consultaBase()
        {
            vm = from usu in context.gen_usuarios
                 join tu in context.gen_tipouser on usu.IDTIPO equals tu.ID
                 join emp in context.gen_empresa on usu.IDEMPRESA equals emp.ID
                 select new UsuariosVM
                 {
                     usuario = usu,
                     tipoUsuario = tu,
                     empresa = emp
                 };

            return vm;
        }

        public List<UsuariosVM> getAll()
        {
            IQueryable<UsuariosVM> vmq = consultaBase().AsQueryable();
            return vmq.ToList();
        }

        public UsuariosVM getById(int? id)
        {
            IQueryable<UsuariosVM> vmq = consultaBase().AsQueryable();
            UsuariosVM pr = vmq.Where(x => x.usuario.ID == id).SingleOrDefault();
            return pr;
        }

        public IEnumerable<UsuariosVM> getByParametro(buscadorUsuarios param)
        {
            IQueryable<UsuariosVM> vmq = consultaBase().AsQueryable();

            if (param.LOGIN != null)
            {
                string codigo = Functions.Functions.CleanInput(param.LOGIN.ToString());
                vmq = vmq.Where(x => x.usuario.LOGIN.Contains(codigo));
            }

            if (param.NOMBRE != null)
            {
                string nombre = Functions.Functions.CleanInput(param.NOMBRE.ToString());
                vmq = vmq.Where(x => x.usuario.NOMBRE.Contains(nombre));
            }

            if (param.TIPOUSUARIO != null)
            {
                string recogedor = Functions.Functions.CleanInput(param.TIPOUSUARIO.ToString());
                vmq = vmq.Where(x => x.usuario.USERTYPE.Contains(recogedor));
            }

            IEnumerable<UsuariosVM> ievm = vmq.ToList();
            return ievm;
        }

        public IEnumerable<UsuariosVM> getSorted(buscadorUsuarios paramFiltro, objetoOrdenMapa paramOrden, int direccion)
        {
            IEnumerable<UsuariosVM> resuFiltro = getByParametro(paramFiltro);

            if (paramOrden.coleccion == "usuario" && paramOrden.nombreCampo == "CODIGO")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.usuario.CODIGO) : resuFiltro.OrderByDescending(x => x.usuario.CODIGO);
            }

            if (paramOrden.coleccion == "usuario" && paramOrden.nombreCampo == "LOGIN")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.usuario.LOGIN) : resuFiltro.OrderByDescending(x => x.usuario.LOGIN);
            }

            if (paramOrden.coleccion == "usuario" && paramOrden.nombreCampo == "NOMBRE")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.usuario.NOMBRE) : resuFiltro.OrderByDescending(x => x.usuario.NOMBRE);
            }

            if (paramOrden.coleccion == "usuario" && paramOrden.nombreCampo == "USERTYPE")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.usuario.USERTYPE) : resuFiltro.OrderByDescending(x => x.usuario.USERTYPE);
            }
            
            return resuFiltro.ToList();
        }

        public List<gen_usuarios> getListaUsuarios()
        {
            var sql = from usu in context.gen_usuarios
                      select usu;

            return sql.ToList();

        }

        public UsuariosVM validation(UsuarioLoginVM u)
        {
            IQueryable<UsuariosVM> vmq = consultaBase().AsQueryable();
            if (String.IsNullOrEmpty(u.PASSWORD))
                u.PASSWORD = "";

            //string hola = Functions.Functions.Base64Decode("d2Fybmllcg==");

            string pass = Functions.Functions.Base64Encode(u.PASSWORD);

            UsuariosVM ievm = vmq.Where(x => x.usuario.LOGIN.Equals(u.LOGIN) && x.usuario.PWD.Equals(pass)).SingleOrDefault();

            if (ievm != null)
            {
                return ievm;
            }
            return null;
        }

        public void Insert(UsuariosVM vm)
        {
            vm.usuario.ROWID = Guid.NewGuid().ToString();
            repo.Insert(vm.usuario);
        }

        public void Update(UsuariosVM vm)
        {
            repo.Update(vm.usuario);
        }

        public void Delete(UsuariosVM vm)
        {
            repo.Delete(vm.usuario);
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