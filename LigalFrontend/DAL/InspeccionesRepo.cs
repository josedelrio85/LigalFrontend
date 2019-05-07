using LigalFrontend.Models;
using LigalFrontend.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System;
using LigalFrontend.Models.Buscador;

namespace LigalFrontend.DAL
{
    public class InspeccionesRepo : IViewModelRepository<InspeccionesVM, buscadorInspecciones>
    {
        private LigalEntities context;
        private GenericRepository<LigalEntities, gen_inspecciones> repo;
        private IEnumerable<InspeccionesVM> vm;
        private int maxRecords = 500;

        
        public InspeccionesRepo()
        {
            context = new LigalEntities();
            repo = new GenericRepository<LigalEntities, gen_inspecciones>();
        }

        public IEnumerable<InspeccionesVM> consultaBase()
        {
            vm = from ins in context.gen_inspecciones
                 join obs in context.gen_ObsInsp on ins.IDOBSERVA equals obs.ID into JoinedObsIns
                 from obs in JoinedObsIns.DefaultIfEmpty()
                 select new InspeccionesVM
                 {
                     inspeccion = ins,
                     observacionInsp = obs
                 };

            return vm;
        }

        public IEnumerable<InspeccionesVM> consultaBaseAlt()
        {
            vm = from ins in context.gen_inspecciones
                 select new InspeccionesVM
                 {
                     inspeccion = ins
                 };

            return vm;
        }

        public InspeccionesVM getById(int? id)
        {
            IQueryable<InspeccionesVM> vmq = consultaBase().AsQueryable();
            InspeccionesVM pr = vmq.Where(x => x.inspeccion.ID == id).SingleOrDefault();
            return pr;
        }



        protected IQueryable<InspeccionesVM> getPreInspeccionesAutocontrol()
        {
            IQueryable<InspeccionesVM> vmq = consultaBase().AsQueryable();
            //t1.Actualizado = 0 AND t1.TipoInspeccion = 1
            IQueryable<InspeccionesVM> pr = vmq.Where(x => x.inspeccion.Actualizado == 0 && x.inspeccion.TipoInspeccion == "1");
            pr = pr.OrderByDescending(x => x.inspeccion.ID);
            return pr;
        }

        public IEnumerable<InspeccionesVM> getInspeccionesAutocontrol()
        {
            IQueryable<InspeccionesVM> aa = getPreInspeccionesAutocontrol();
            return aa.ToList();
        }



        protected IQueryable<InspeccionesVM> getPreTodasInspeccionesAutocontrol()
        {
            IQueryable<InspeccionesVM> vmq = consultaBase().AsQueryable();
            //t1.TipoInspeccion = 1
            IQueryable<InspeccionesVM> pr = vmq.Where(x => x.inspeccion.TipoInspeccion == "1");
            pr = pr.OrderByDescending(x => x.inspeccion.ID).Take(maxRecords);
            return pr;
        }

        public IEnumerable<InspeccionesVM> getTodasInspeccionesAutocontrol()
        {
            return getPreTodasInspeccionesAutocontrol().ToList();
        }



        protected IQueryable<InspeccionesVM> getPreInspeccionesAleatorias()
        {
            IQueryable<InspeccionesVM> vmq = consultaBaseAlt().AsQueryable();
            //i.ACTUALIZADO = 0 AND i.TIPOINSPECCION = 0
            IQueryable<InspeccionesVM> pr = vmq.Where(x => x.inspeccion.Actualizado == 1 && x.inspeccion.TipoInspeccion == "0");
            pr = pr.OrderByDescending(x => x.inspeccion.FechaHoraVisita);
            return pr;
        }

        public IEnumerable<InspeccionesVM> getInspeccionesAleatorias()
        {
            return getPreInspeccionesAleatorias().ToList();
        }



        public IQueryable<InspeccionesVM> getPreTodasInspeccionesAleatorias()
        {
            IQueryable<InspeccionesVM> vmq = consultaBaseAlt().AsQueryable();
            //t1.TipoInspeccion = 0
            IQueryable<InspeccionesVM> pr = vmq.Where(x => x.inspeccion.TipoInspeccion == "0");
            pr = pr.OrderByDescending(x => x.inspeccion.FechaHoraVisita).Take(maxRecords);
            return pr;
        }

        public IEnumerable<InspeccionesVM> getTodasInspeccionesAleatorias()
        {
            return getPreTodasInspeccionesAleatorias().ToList();
        }



        public IQueryable<InspeccionesVM> getInspeccionesAflatoxina()
        {
            IQueryable<InspeccionesVM> vmq = consultaBase().AsQueryable();
            //i.ACTUALIZADO = 0 AND i.TIPOINSPECCION = 3
            IQueryable<InspeccionesVM> pr = vmq.Where(x => x.inspeccion.Actualizado == 0 && x.inspeccion.TipoInspeccion == "3");
            return pr;
        }

        public IQueryable<InspeccionesVM> getTodasInspeccionesAflatoxina()
        {
            IQueryable<InspeccionesVM> vmq = consultaBase().AsQueryable();
            //i.TIPOINSPECCION = 3
            IQueryable<InspeccionesVM> pr = vmq.Where(x => x.inspeccion.TipoInspeccion == "3");
            pr = pr.OrderByDescending(x => x.inspeccion.ID).Take(maxRecords);

            return pr;
        }

        public IQueryable<InspeccionesVM> getModificablesInspeccionesAutocontrol()
        {
            IQueryable<InspeccionesVM> vmq = consultaBase().AsQueryable();
            //t1.TipoInspeccion = 1 and Situacion is not null and (now - fechaanalisisorigen < 5)
            IQueryable<InspeccionesVM> pr = vmq.Where(x => x.inspeccion.TipoInspeccion == "1")
                .Where(x => !x.inspeccion.Situacion.Equals(string.Empty))
                .Where(x => System.Data.Entity.DbFunctions.DiffDays(DateTime.Now, x.inspeccion.FechaAnalisisOrigen) < 5);
            return pr;
        }



        public IEnumerable<InspeccionesVM> getByParametro(buscadorInspecciones param, int tipoConsulta)
        {
            IQueryable<InspeccionesVM> vmq = null;

            switch (tipoConsulta)
            {
                case 1:
                    vmq = getPreInspeccionesAutocontrol();
                    break;
                case 2:
                    vmq = getPreTodasInspeccionesAutocontrol();
                    break;
                case 3:
                    vmq = getPreInspeccionesAleatorias();
                    break;
                case 4:
                    vmq = getPreTodasInspeccionesAleatorias();
                    break;
                default:
                    vmq = getPreInspeccionesAutocontrol();
                    break;
            }

            if (!String.IsNullOrEmpty(param.idUsuario))
            {
                int idUsuario = Int32.Parse(param.idUsuario);
                vmq = vmq.Where(x => x.inspeccion.IDUsuario == idUsuario);
            }

            if (!String.IsNullOrEmpty(param.IDExplotacion))
            {
                string resu = Functions.Functions.CleanInput(param.IDExplotacion);
                vmq = vmq.Where(x => x.inspeccion.IDExplotacion.Contains(resu));
            }

            if (!String.IsNullOrEmpty(param.FechaHoraVisitaI))
            {
                System.DateTime dIni = Functions.Functions.textoToFecha(param.FechaHoraVisitaI);
                vmq = vmq.Where(x => x.inspeccion.FechaHoraVisita >= dIni);
            }

            if (!String.IsNullOrEmpty(param.FechaHoraVisitaF))
            {
                System.DateTime dFin = Functions.Functions.textoToFecha(param.FechaHoraVisitaF);
                vmq = vmq.Where(x => x.inspeccion.FechaHoraVisita < dFin);
            }

            if (!String.IsNullOrEmpty(param.INDUSTRIAN))
            {
                string industria = Functions.Functions.CleanInput(param.INDUSTRIAN);
                vmq = vmq.Where(x => x.inspeccion.INDUSTRIAN.Contains(industria));
            }

            if (!String.IsNullOrEmpty(param.Resultado))
            {
                string resu = Functions.Functions.CleanInput(param.Resultado);
                vmq = vmq.Where(x => x.inspeccion.Resultado.Contains(resu));
            }

            if (!String.IsNullOrEmpty(param.ResultadoCHARM))
            {
                string resucharm = Functions.Functions.CleanInput(param.ResultadoCHARM);
                vmq = vmq.Where(x => x.inspeccion.ResultadoCHARM.Contains(resucharm));
            }

            if (!String.IsNullOrEmpty(param.RESULTADOQUINO))
            {
                string resuquino = Functions.Functions.CleanInput(param.RESULTADOQUINO);
                vmq = vmq.Where(x => x.inspeccion.RESULTADOQUINO.Contains(resuquino));
            }

            vmq = vmq.OrderByDescending(x => x.inspeccion.ID);
            IEnumerable<InspeccionesVM> ievm = vmq.ToList();
            return ievm;
        }

        public IEnumerable<InspeccionesVM> getSorted(buscadorInspecciones paramFiltro, objetoOrdenMapa paramOrden, int direccion, int tipoConsulta)
        {
            IEnumerable<InspeccionesVM> resuFiltro = getByParametro(paramFiltro, tipoConsulta);

            if (paramOrden.coleccion == "inspeccion" && paramOrden.nombreCampo == "IDUsuario")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.inspeccion.IDUsuario) : resuFiltro.OrderByDescending(x => x.inspeccion.IDUsuario);
            }

            if (paramOrden.coleccion == "inspeccion" && paramOrden.nombreCampo == "Situacion")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.inspeccion.Situacion) : resuFiltro.OrderByDescending(x => x.inspeccion.Situacion);

            }
            if (paramOrden.coleccion == "inspeccion" && paramOrden.nombreCampo == "Resultado")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.inspeccion.Resultado) : resuFiltro.OrderByDescending(x => x.inspeccion.Resultado);
            }

            if (paramOrden.coleccion == "inspeccion" && paramOrden.nombreCampo == "Nombre")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.inspeccion.Nombre) : resuFiltro.OrderByDescending(x => x.inspeccion.Nombre);
            }

            if (paramOrden.coleccion == "inspeccion" && paramOrden.nombreCampo == "IDExplotacion")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.inspeccion.IDExplotacion) : resuFiltro.OrderByDescending(x => x.inspeccion.IDExplotacion);
            }

            if (paramOrden.coleccion == "inspeccion" && paramOrden.nombreCampo == "INDUSTRIAN")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.inspeccion.INDUSTRIAN) : resuFiltro.OrderByDescending(x => x.inspeccion.INDUSTRIAN);
            }

            if (paramOrden.coleccion == "inspeccion" && paramOrden.nombreCampo == "FechaHoraVisita")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.inspeccion.FechaHoraVisita) : resuFiltro.OrderByDescending(x => x.inspeccion.FechaHoraVisita);
            }

            return resuFiltro.ToList();
        }

        public void Insert(InspeccionesVM vm)
        {
            vm.inspeccion.ROWID = Guid.NewGuid().ToString();
            repo.Insert(vm.inspeccion);
        }

        public void Update(InspeccionesVM vm)
        {
            repo.Update(vm.inspeccion);
        }

        public void Delete(InspeccionesVM vm)
        {
            repo.Delete(vm.inspeccion);
        }

        public void Save()
        {
            repo.Save();
        }

        public void Dispose()
        {
            repo.Dispose();
        }

        public IEnumerable<InspeccionesVM> getByParametro(buscadorInspecciones buscador)
        {
            throw new NotImplementedException();
        }
    }
}