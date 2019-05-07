using LigalFrontend.Models;
using LigalFrontend.Models.Buscador;
using LigalFrontend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LigalFrontend.DAL
{
    public class InsercionRecogidasRepo : IViewModelRepository<InsercionRecogidasVM, buscadorInsercionRecogidas>
    {
        private LigalEntities context;
        private GenericRepository<LigalEntities, gen_puntorecogidadia> repo;
        private IEnumerable<InsercionRecogidasVM> vm;

        private int maxRecords = 500;

        public InsercionRecogidasRepo()
        {
            context = new LigalEntities();
            repo = new GenericRepository<LigalEntities, gen_puntorecogidadia>();
        }

        public IEnumerable<InsercionRecogidasVM> consultaBase()
        {
            var a = new GEN_PUNTOSRECOGIDA() { ID = 0, CODIGOP = null, NOMBRE = null };
            vm = from prd in context.gen_puntorecogidadia
                 join pr in context.GEN_PUNTOSRECOGIDA on prd.IDPUNTO equals pr.ID
                 join usu in context.gen_usuarios on prd.IDUSUARIO equals usu.ID
                 select new InsercionRecogidasVM
                 {
                     puntoRecogidaDia = prd,
                     pRecogida = pr,
                     usuario = usu
                 };
            return vm;
        }

        public IEnumerable<InsercionRecogidasVM> consultaBaseAlt()
        {
            var a = new GEN_PUNTOSRECOGIDA() { ID = 0, CODIGOP = null, NOMBRE = null };
            vm = from prd in context.gen_puntorecogidadia
                 join pr in context.GEN_PUNTOSRECOGIDA on prd.IDPUNTO equals pr.ID into JoinedPrPrd
                 join usu in context.gen_usuarios on prd.IDUSUARIO equals usu.ID
                 from PrPrd in JoinedPrPrd.DefaultIfEmpty(a)
                 select new InsercionRecogidasVM
                 {
                     puntoRecogidaDia = prd,
                     pRecogida = PrPrd,
                     usuario = usu
                 };
            return vm;
        }

        public List<InsercionRecogidasVM> getAll()
        {
            IQueryable<InsercionRecogidasVM> vmq = consultaBase().AsQueryable();
            vmq = vmq.OrderByDescending(x => x.puntoRecogidaDia.FECHA).Take(maxRecords);
            return vmq.ToList();
        }

        public InsercionRecogidasVM getById(int? id)
        {
            IEnumerable<InsercionRecogidasVM> vmq = consultaBase().AsQueryable();
            return vmq.Where(x => x.puntoRecogidaDia.ID == id).SingleOrDefault();
        }

        public IEnumerable<InsercionRecogidasVM> getByParametro(buscadorInsercionRecogidas param)
        {
            IQueryable<InsercionRecogidasVM> vmq = consultaBase().AsQueryable();

            vmq = aplicaParametros(vmq, param);

            IEnumerable<InsercionRecogidasVM> ievm = vmq.OrderBy(x => x.puntoRecogidaDia.FECHA).Take(maxRecords).ToList(); ;
            return ievm;
        }

        protected IQueryable<InsercionRecogidasVM> aplicaParametros(IQueryable<InsercionRecogidasVM> vmq, buscadorInsercionRecogidas param)
        {
            if (param.idUsuario != null)
            {
                int idUsuario = Int32.Parse(param.idUsuario);
                vmq = vmq.Where(x => x.puntoRecogidaDia.IDUSUARIO == idUsuario);
            }

            if (param.codpunto != null)
            {
                string resu = Functions.Functions.CleanInput(param.codpunto.ToString());
                vmq = vmq.Where(x => x.pRecogida.CODIGOP.Contains(resu));
            }

            if (param.fechaInicio != null)
            {
                System.DateTime dIni = Functions.Functions.textoToFecha(param.fechaInicio);
                vmq = vmq.Where(x => x.puntoRecogidaDia.FECHA > dIni);
            }

            if (param.fechaFin != null)
            {
                System.DateTime dFin = Functions.Functions.textoToFecha(param.fechaFin, true);
                vmq = vmq.Where(x => x.puntoRecogidaDia.FECHA <= dFin);
            }

            if (param.programado != null)
            {
                int programado = Int32.Parse(param.programado);
                vmq = vmq.Where(x => x.puntoRecogidaDia.PROGRAMADO == programado);
            }

            if (param.nompunto != null)
            {
                string resu = Functions.Functions.CleanInput(param.nompunto.ToString());
                vmq = vmq.Where(x => x.pRecogida.NOMBRE.Contains(resu));
            }

            return vmq;
        }

        public IEnumerable<InsercionRecogidasVM> getSorted(buscadorInsercionRecogidas paramFiltro, objetoOrdenMapa paramOrden, int direccion)
        {
            IEnumerable<InsercionRecogidasVM> resuFiltro = getByParametro(paramFiltro);

            if (paramOrden.coleccion == "pRecogida" && paramOrden.nombreCampo == "CODIGOP")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.pRecogida.CODIGOP) : resuFiltro.OrderByDescending(x => x.pRecogida.CODIGOP);
            }

            if (paramOrden.coleccion == "pRecogida" && paramOrden.nombreCampo == "NOMBRE")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.pRecogida.NOMBRE) : resuFiltro.OrderByDescending(x => x.pRecogida.NOMBRE);
            }

            if (paramOrden.coleccion == "puntoRecogidaDia" && paramOrden.nombreCampo == "FECHA")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.puntoRecogidaDia.FECHA) : resuFiltro.OrderByDescending(x => x.puntoRecogidaDia.FECHA);
            }

            if (paramOrden.coleccion == "puntoRecogidaDia" && paramOrden.nombreCampo == "HORA")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.puntoRecogidaDia.HORA) : resuFiltro.OrderByDescending(x => x.puntoRecogidaDia.HORA);
            }

            if (paramOrden.coleccion == "puntoRecogidaDia" && paramOrden.nombreCampo == "PROGRAMADO")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.puntoRecogidaDia.PROGRAMADO) : resuFiltro.OrderByDescending(x => x.puntoRecogidaDia.PROGRAMADO);
            }

            if (paramOrden.coleccion == "puntoRecogidaDia" && paramOrden.nombreCampo == "VISITADO")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.puntoRecogidaDia.VISITADO) : resuFiltro.OrderByDescending(x => x.puntoRecogidaDia.VISITADO);
            }

            IEnumerable<InsercionRecogidasVM> aa = resuFiltro.ToList();
            return aa;
        }

        public void Insert(InsercionRecogidasVM vm)
        {
            vm.puntoRecogidaDia.ROWID = Guid.NewGuid().ToString();
            repo.Insert(vm.puntoRecogidaDia);
        }

        public void Update(InsercionRecogidasVM vm)
        {
            repo.Update(vm.puntoRecogidaDia);
        }

        public void Delete(InsercionRecogidasVM vm)
        {
            repo.Delete(vm.puntoRecogidaDia);
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