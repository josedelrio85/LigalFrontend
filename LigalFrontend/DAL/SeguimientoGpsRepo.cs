using LigalFrontend.Models;
using LigalFrontend.Models.Buscador;
using LigalFrontend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LigalFrontend.DAL
{
    public class SeguimientoGpsRepo : IViewModelRepository<SeguimientoGpsVM, buscadorSeguimientoGps>
    {
        private LigalEntities context;
        private GenericRepository<LigalEntities, GEN_COORDENADASGPS> repo;
        private IEnumerable<SeguimientoGpsVM> vm;

        public SeguimientoGpsRepo()
        {
            context = new LigalEntities();
            repo = new GenericRepository<LigalEntities, GEN_COORDENADASGPS>();
        }

        public IEnumerable<SeguimientoGpsVM> consultaBase()
        {
            vm = from sg in context.GEN_COORDENADASGPS
                 join usu in context.gen_usuarios on sg.IDUSUARIO equals usu.ID
                 select new SeguimientoGpsVM
                 {
                     coordenadasGps = sg,
                     usuario = usu,
                     buscador = new buscadorSeguimientoGps()
                 };

            return vm;
        }

        public SeguimientoGpsVM getById(int? id)
        {
            IQueryable<SeguimientoGpsVM> vmq = consultaBase().AsQueryable();
            SeguimientoGpsVM vm = vmq.Where(x => x.coordenadasGps.ID == id).SingleOrDefault();
            return vm;
        }

        public IEnumerable<SeguimientoGpsVM> getByParametro(buscadorSeguimientoGps param)
        {
            IQueryable<SeguimientoGpsVM> vmq = consultaBase().AsQueryable();

            if (param.idUsuario != null)
            {
                int idUsuario = Int32.Parse(param.idUsuario);
                vmq = vmq.Where(x => x.coordenadasGps.IDUSUARIO == idUsuario);
            }

            if (param.FechaHoraVisitaI != null)
            {
                System.DateTime dIni = Functions.Functions.textoToFecha(param.FechaHoraVisitaI);
                vmq = vmq.Where(x => x.coordenadasGps.FECHAHORAPDA > dIni);
            }

            if (param.FechaHoraVisitaF != null)
            {
                System.DateTime dFin = Functions.Functions.textoToFecha(param.FechaHoraVisitaF);
                vmq = vmq.Where(x => x.coordenadasGps.FECHAHORAPDA <= dFin);
            }

            IEnumerable<SeguimientoGpsVM> ievm = vmq.ToList();
            return ievm;
        }

        public IEnumerable<SeguimientoGpsVM> getSorted(buscadorSeguimientoGps paramFiltro, objetoOrdenMapa paramOrden, int direccion)
        {
            IEnumerable<SeguimientoGpsVM> resuFiltro = getByParametro(paramFiltro);

            if (paramOrden.coleccion == "usuario" && paramOrden.nombreCampo == "NOMBRE")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.usuario.NOMBRE) : resuFiltro.OrderByDescending(x => x.usuario.NOMBRE);
            }

            if (paramOrden.coleccion == "coordenadasGps" && paramOrden.nombreCampo == "FECHAHORAPDA")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.coordenadasGps.FECHAHORAPDA) : resuFiltro.OrderByDescending(x => x.coordenadasGps.FECHAHORAPDA);
            }

            if (paramOrden.coleccion == "coordenadasGps" && paramOrden.nombreCampo == "LONGITUDGPS")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.coordenadasGps.LONGITUDGPS) : resuFiltro.OrderByDescending(x => x.coordenadasGps.LONGITUDGPS);
            }

            if (paramOrden.coleccion == "coordenadasGps" && paramOrden.nombreCampo == "LATITUDGPS")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.coordenadasGps.LATITUDGPS) : resuFiltro.OrderByDescending(x => x.coordenadasGps.LATITUDGPS);
            }

            if (paramOrden.coleccion == "coordenadasGps" && paramOrden.nombreCampo == "NPUNTO")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.coordenadasGps.NPUNTO) : resuFiltro.OrderByDescending(x => x.coordenadasGps.NPUNTO);
            }
            
            return resuFiltro.ToList();
        }

        public void Insert(SeguimientoGpsVM vm)
        {
            vm.coordenadasGps.ROWID = Guid.NewGuid().ToString();
            repo.Insert(vm.coordenadasGps);
        }

        public void Update(SeguimientoGpsVM vm)
        {
            repo.Update(vm.coordenadasGps);
        }

        public void Delete(SeguimientoGpsVM vm)
        {
            repo.Delete(vm.coordenadasGps);
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