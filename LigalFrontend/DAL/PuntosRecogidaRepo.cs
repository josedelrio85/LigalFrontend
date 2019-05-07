using LigalFrontend.Models;
using LigalFrontend.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System;
using LigalFrontend.Models.Buscador;

namespace LigalFrontend.DAL
{
    public class PuntosRecogidaRepo : IViewModelRepository<PuntosRecogidaVM, buscadorPuntosRecogida>, IDisposable
    {
        private LigalEntities context;
        private GenericRepository<LigalEntities, GEN_PUNTOSRECOGIDA> repo;
        private IEnumerable<PuntosRecogidaVM> vm;

        public PuntosRecogidaRepo()
        {
            context = new LigalEntities();
            repo = new GenericRepository<LigalEntities, GEN_PUNTOSRECOGIDA>();
        }

        public IEnumerable<PuntosRecogidaVM> consultaBase()
        {
            vm = from pr in context.GEN_PUNTOSRECOGIDA
                join usu in context.gen_usuarios on pr.IDUSUARIO equals usu.ID
                select new PuntosRecogidaVM
                {
                    puntosRecogida = pr,
                    usuario = usu,
                    buscador = new Models.Buscador.buscadorPuntosRecogida()
                };
            
            return vm;
        }

        public List<PuntosRecogidaVM> getAll()
        {
            IQueryable<PuntosRecogidaVM> vmq = consultaBase().AsQueryable();
            return vmq.ToList();
        }

        public PuntosRecogidaVM getById(int? id)
        {
            IQueryable<PuntosRecogidaVM> vmq = consultaBase().AsQueryable();
            PuntosRecogidaVM pr = vmq.Where(x => x.puntosRecogida.ID == id).SingleOrDefault();
            return pr;
        }

        public IEnumerable<PuntosRecogidaVM> getByCodigoP(string codigoP)
        {
            IQueryable<PuntosRecogidaVM> vmq = consultaBase().AsQueryable();
            IEnumerable<PuntosRecogidaVM> pr = vmq.Where(x => x.puntosRecogida.CODIGOP.Contains(codigoP)).ToList();
            return pr;
        }

        public IEnumerable<PuntosRecogidaVM> getByNombreP(string nombreP)
        {
            IQueryable<PuntosRecogidaVM> vmq = consultaBase().AsQueryable();
            IEnumerable<PuntosRecogidaVM> pr = vmq.Where(x => x.puntosRecogida.NOMBRE.Contains(nombreP)).ToList();
            return pr;
        }

        public IEnumerable<PuntosRecogidaVM> getByNombreR(string nombreR)
        {
            IQueryable<PuntosRecogidaVM> vmq = consultaBase().AsQueryable();
            IEnumerable<PuntosRecogidaVM> pr = vmq.Where(x => x.usuario.NOMBRE.Contains(nombreR)).ToList();
            return pr;
        }

        public IEnumerable<PuntosRecogidaVM> getByParametro(buscadorPuntosRecogida param)
        {
            IQueryable<PuntosRecogidaVM> vmq = consultaBase().AsQueryable();

            if (param.CODIGOP != null)
            {
                string codigo = Functions.Functions.CleanInput(param.CODIGOP.ToString());
                vmq = vmq.Where(x => x.puntosRecogida.CODIGOP.Contains(codigo));
            }

            if (param.NOMBRE != null)
            {
                string nombre = Functions.Functions.CleanInput(param.NOMBRE.ToString());
                vmq = vmq.Where(x => x.puntosRecogida.NOMBRE.Contains(nombre));
            }

            if (param.RECOGEDOR != null)
            {
                string recogedor = Functions.Functions.CleanInput(param.RECOGEDOR.ToString());
                vmq = vmq.Where(x => x.usuario.NOMBRE.Contains(recogedor));
            }

            IEnumerable<PuntosRecogidaVM> ievm = vmq.ToList();
            return ievm;
        }

        public IEnumerable<PuntosRecogidaVM> getSorted(buscadorPuntosRecogida paramFiltro, objetoOrdenMapa paramOrden, int direccion)
        {
            IEnumerable<PuntosRecogidaVM> resuFiltro = getByParametro(paramFiltro);

            if (paramOrden.coleccion == "puntosRecogida" && paramOrden.nombreCampo == "CODIGOP")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.puntosRecogida.CODIGOP) : resuFiltro.OrderByDescending(x => x.puntosRecogida.CODIGOP);
            }

            if (paramOrden.coleccion == "puntosRecogida" && paramOrden.nombreCampo == "NOMBRE")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.puntosRecogida.NOMBRE) : resuFiltro.OrderByDescending(x => x.puntosRecogida.NOMBRE);
            }

            if (paramOrden.coleccion == "puntosRecogida" && paramOrden.nombreCampo == "COORDX")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.puntosRecogida.COORDX) : resuFiltro.OrderByDescending(x => x.puntosRecogida.COORDX);
            }

            if (paramOrden.coleccion == "puntosRecogida" && paramOrden.nombreCampo == "COORDY")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.puntosRecogida.COORDY) : resuFiltro.OrderByDescending(x => x.puntosRecogida.COORDY);
            }

            if (paramOrden.coleccion == "usuario" && paramOrden.nombreCampo == "NOMBRE")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.usuario.NOMBRE) : resuFiltro.OrderByDescending(x => x.usuario.NOMBRE);
            }

            return resuFiltro.ToList();                       
        }

        public List<GEN_PUNTOSRECOGIDA> getListaPuntosRecogida()
        {
            var sql = from pr in context.GEN_PUNTOSRECOGIDA
                      select pr;

            return sql.ToList();
        }

        public void Insert(PuntosRecogidaVM vm)
        {
            vm.puntosRecogida.ROWID = Guid.NewGuid().ToString();
            repo.Insert(vm.puntosRecogida);
        }

        public void Update(PuntosRecogidaVM vm)
        {
            repo.Update(vm.puntosRecogida);
        }

        public void Delete(PuntosRecogidaVM vm)
        {
            repo.Delete(vm.puntosRecogida);
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