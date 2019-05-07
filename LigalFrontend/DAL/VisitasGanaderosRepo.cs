using LigalFrontend.Models;
using LigalFrontend.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System;
using LigalFrontend.Models.Buscador;

namespace LigalFrontend.DAL
{
    public class VisitasGanaderosRepo : IViewModelRepository<VisitasGanaderosVM, buscadorVisitasGanadero>
    {
        private LigalEntities context;
        private GenericRepository<LigalEntities, gen_visitavet> repo;
        private IEnumerable<VisitasGanaderosVM> vm;

        public VisitasGanaderosRepo()
        {
            context = new LigalEntities();
            repo = new GenericRepository<LigalEntities, gen_visitavet>();
        }

        public IEnumerable<VisitasGanaderosVM> consultaBase()
        {
            vm = from vv in context.gen_visitavet
                 join usu in context.gen_usuarios on vv.IDUSUARIO equals usu.ID
                 select new VisitasGanaderosVM
                 {
                     visitasVet = vv,
                     usuario = usu,
                     buscador = new buscadorVisitasGanadero()
                 };

            return vm;
        }

        public VisitasGanaderosVM getById(int? id)
        {
            IQueryable<VisitasGanaderosVM> vmq = consultaBase().AsQueryable();
            VisitasGanaderosVM pr = vmq.Where(x => x.visitasVet.ID == id).SingleOrDefault();
            return pr;
        }

        public IEnumerable<VisitasGanaderosVM> getByParametro(buscadorVisitasGanadero param)
        {
            IQueryable<VisitasGanaderosVM> vmq = consultaBase().AsQueryable();

            if (param.idUsuario != null)
            {
                int idUsuario = Int32.Parse(param.idUsuario);
                vmq = vmq.Where(x => x.visitasVet.IDUSUARIO == idUsuario);
            }

            if (param.serieGanadero != null)
            {
                int resu = Int32.Parse(param.serieGanadero);
                vmq = vmq.Where(x => x.visitasVet.SERIEGAN == resu);
            }

            if (param.FechaHoraVisitaI != null)
            {
                System.DateTime dIni = Functions.Functions.textoToFecha(param.FechaHoraVisitaI);
                vmq = vmq.Where(x => x.visitasVet.FECHA > dIni);
            }

            if (param.FechaHoraVisitaF != null)
            {
                System.DateTime dFin = Functions.Functions.textoToFecha(param.FechaHoraVisitaF);
                vmq = vmq.Where(x => x.visitasVet.FECHA <= dFin);
            }

            if (param.codMuestra == 1)
            {
                vmq = vmq.Where(x => x.visitasVet.OBSERVA.StartsWith("69"));
            }

            IEnumerable<VisitasGanaderosVM> ievm = vmq.ToList();
            return ievm;
        }

        public IEnumerable<VisitasGanaderosVM> getSorted(buscadorVisitasGanadero paramFiltro, objetoOrdenMapa paramOrden, int direccion)
        {
            IEnumerable<VisitasGanaderosVM> resuFiltro = getByParametro(paramFiltro);

            if (paramOrden.coleccion == "visitasVet" && paramOrden.nombreCampo == "FECHA")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.visitasVet.FECHA) : resuFiltro.OrderByDescending(x => x.visitasVet.FECHA);
            }

            if (paramOrden.coleccion == "usuario" && paramOrden.nombreCampo == "NOMBRE")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.usuario.NOMBRE) : resuFiltro.OrderByDescending(x => x.usuario.NOMBRE);
            }

            if (paramOrden.coleccion == "visitasVet" && paramOrden.nombreCampo == "SERIEGAN")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.visitasVet.SERIEGAN) : resuFiltro.OrderByDescending(x => x.visitasVet.SERIEGAN);
            }

            if (paramOrden.coleccion == "visitasVet" && paramOrden.nombreCampo == "COORDX")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.visitasVet.COORDX) : resuFiltro.OrderByDescending(x => x.visitasVet.COORDX);
            }

            if (paramOrden.coleccion == "visitasVet" && paramOrden.nombreCampo == "COORDY")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.visitasVet.COORDY) : resuFiltro.OrderByDescending(x => x.visitasVet.COORDY);
            }

            if (paramOrden.coleccion == "visitasVet" && paramOrden.nombreCampo == "OBSERVA")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.visitasVet.OBSERVA) : resuFiltro.OrderByDescending(x => x.visitasVet.OBSERVA);
            }

            return resuFiltro.ToList();
        }

        public void Insert(VisitasGanaderosVM vm)
        {
            vm.visitasVet.ROWID = Guid.NewGuid().ToString();
            repo.Insert(vm.visitasVet);
        }

        public void Update(VisitasGanaderosVM vm)
        {
            repo.Update(vm.visitasVet);
        }

        public void Delete(VisitasGanaderosVM vm)
        {
            repo.Delete(vm.visitasVet);
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